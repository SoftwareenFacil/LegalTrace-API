using LegalTrace.BLL.Models.ClientDTO;
using LegalTrace.DAL.Context;
using LegalTrace.DAL.Models;
using LegalTrace.DAL.Repository;
using LegalTrace.PDF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegalTrace.BLL.Controllers.ClientControllers
{
    public class ClientService
    {
        private readonly ClientRepository _repository;
        public ClientService(AppDbContext _dbContext)
        {
            _repository = new ClientRepository(_dbContext);
        }

        public async Task<int> AddClient(ClientInsertDTO client)
        {
            if (await _repository.GetClientByEmail(client.Email) != null)
                return -1;

            DateTime utcNow = DateTime.UtcNow;
            var clientCreate = new Client()
            {
                Name = client.Name,
                Email = client.Email,
                Phone = client.Phone,
                TaxId = client.TaxId,
                Address = client.Address,
                Created = DateTime.SpecifyKind(utcNow, DateTimeKind.Utc),
                Updated = DateTime.SpecifyKind(utcNow, DateTimeKind.Utc),
                Vigency = true
            };
            return await _repository.InsertClient(clientCreate);
        }
        public async Task<bool> DeleteClientById(int id)
        {
            var exist = await _repository.GetClientById(id);
            if (exist == null)
            {
                return false;
            }

            return await _repository.DeleteClient(id);
        }
        public async Task<List<ClientDTO>> GetClientsWithNoMovements(DateTime from, DateTime to)
        {
            var clients = await _repository.GetClientsWithNoMovements(from, to);
            if (clients.Count() > 0)
            {
                List<ClientDTO> result = new List<ClientDTO>();
                clients.ForEach(row => result.Add(new ClientDTO()
                {
                    Id = row.Id,
                    Name = row.Name,
                    Email = row.Email,
                    TaxId = row.TaxId,
                    Address = row.Address,
                    Created = row.Created,
                    Vigency = row.Vigency
                }));
                return result;
            }

            return new List<ClientDTO>();
        }
        public async Task<List<ClientDTO>> GetClientBy(int? id, string? name, string? email, string? taxId, DateTime? createdFrom, DateTime? createdTo, bool? vigency)
        {
            var clients = await _repository.GetClientBy(id, name, email, taxId, createdFrom, createdTo, vigency);
            if (clients.Count() > 0)
            {
                List<ClientDTO> result = new List<ClientDTO>();
                clients.ForEach(row => result.Add(new ClientDTO()
                {
                    Id = row.Id,
                    Name = row.Name,
                    Email = row.Email,
                    TaxId = row.TaxId,
                    Address = row.Address,
                    Created = row.Created,
                    Vigency = row.Vigency
                }));
                return result;
            }

            return new List<ClientDTO>();
        }

        public async Task<ClientDTO?> GetClientById(int id)
        {
            var client = await _repository.GetClientById(id);
            if (client != null)
            {
                return new ClientDTO()
                {
                    Id = client.Id,
                    Name = client.Name,
                    Email = client.Email,
                    TaxId = client.TaxId,
                    Address = client.Address,
                    Created = client.Created,
                    Vigency = client.Vigency
                };
            }
            return null;
        }
        public async Task<int> UpdateClient(ClientEditDTO clientEdited)
        {
            if (string.IsNullOrWhiteSpace(clientEdited.Name) && string.IsNullOrWhiteSpace(clientEdited.Email) && clientEdited.Phone == 0 && string.IsNullOrWhiteSpace(clientEdited.Address) && string.IsNullOrWhiteSpace(clientEdited.TaxId))
                return 400;

            var client = await _repository.GetClientById(clientEdited.Id);
            if (client != null)
            {
                client.Name = !string.IsNullOrEmpty(clientEdited.Name) ? clientEdited.Name : client.Name;
                client.Email = !string.IsNullOrEmpty(clientEdited.Email) ? clientEdited.Email : client.Email;
                client.Phone = clientEdited.Phone > 0 ? clientEdited.Phone : client.Phone;
                client.Address = !string.IsNullOrEmpty(clientEdited.Address) ? clientEdited.Address : client.Address;
                client.TaxId = !string.IsNullOrEmpty(clientEdited.TaxId) ? clientEdited.TaxId : client.TaxId;

                DateTime utcNow = DateTime.UtcNow;
                client.Updated = DateTime.SpecifyKind(utcNow, DateTimeKind.Utc);

                var isUpdated = await _repository.UpdateClient(client);
                if (!isUpdated)
                    return 400;
                return 200;
            }
            return 404;
        }

        public async Task<int> UpdateClientVigency(int id)
        {
            var client = await _repository.GetClientById(id);
            if (client != null)
            {
                if (client.Vigency)
                {
                    client.Vigency = false;
                }
                else { client.Vigency = true; }

                var isUpdated = await _repository.UpdateClient(client);
                if (isUpdated)
                    return 200;
                return 500;
            }
            return 404;
        }
    }
}
