using LegalTrace.DAL.Context;
using LegalTrace.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegalTrace.DAL.Controllers.ClientControllers
{
    public class ClientController : IClientController
    {
        private AppDbContext _context;
        public ClientController(AppDbContext _dbContext)
        {
            _context = _dbContext;
        }

        public async Task<Client> GetClientById(int id)
        {
            var response = await _context.Clients.Where(clientAux => clientAux.Id.Equals(id)).FirstOrDefaultAsync();
            return response;
        }

        public async Task<Client?> GetClientByEmail(string email)
        {
            var client = await _context.Clients.Where(u => u.Email == email).FirstOrDefaultAsync();
            return client;
        }

        public async Task<List<Client>> GetClientBy(int? id, string? name, string? email, string? taxId, DateTime? created, bool? vigency)
        {
            if (id.HasValue)
            {
                if (id.Value == 0)
                {
                    return await _context.Clients.Take(100).ToListAsync();
                }
                else
                {
                    var client = await _context.Clients.FirstOrDefaultAsync(u => u.Id == id.Value);
                    return client == null ? new List<Client>() : new List<Client> { client };
                }
            }

            var query = _context.Clients.AsQueryable();

            if (!string.IsNullOrWhiteSpace(name))
            {
                query = query.Where(u => EF.Functions.Like(u.Name, $"%{name}%"));
            }

            if (!string.IsNullOrWhiteSpace(email))
            {
                query = query.Where(u => EF.Functions.Like(u.Email, $"%{email}%"));
            }

            if (!string.IsNullOrWhiteSpace(taxId))
            {
                query = query.Where(u => EF.Functions.Like(u.TaxId, $"%{taxId}%"));
            }

            if (created.HasValue)
            {
                query = query.Where(u => u.Created > created.Value)
                             .OrderBy(u => u.Created);
            }

            if (vigency.HasValue)
            {
                query = query.Where(u => u.Vigency == vigency.Value);
            }

            return await query.Take(100).ToListAsync();
        }

        public async Task<bool> DeleteClient(int id)
        {
            var client = await _context.Clients.Where(clientAux => clientAux.Id.Equals(id)).FirstOrDefaultAsync();
            if (client != null)
            {
                _context.Clients.Remove(client);
                if (await _context.SaveChangesAsync() > 0)
                {
                    return true;
                }
            }

            return false;
        }

        public async Task<int> InsertClient(Client client)
        {
            await _context.Clients.AddAsync(client);
            return await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateClient(Client client)
        {
            var response = await _context.Clients.Where(clientAux => clientAux.Id.Equals(client.Id)).FirstOrDefaultAsync();
            if (response != null)
            {
                response.Name = client.Name;
                response.Email = client.Email;
                response.Phone = client.Phone;
                response.TaxId = client.TaxId;
                response.Updated = client.Updated;

                if (await _context.SaveChangesAsync() > 0)
                    return true;

            }
            return false;
        }
    }
}
