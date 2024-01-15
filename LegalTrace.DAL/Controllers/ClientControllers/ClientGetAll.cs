using Microsoft.EntityFrameworkCore;
using LegalTrace.DAL.Context;
using LegalTrace.DAL.Models;

namespace LegalTrace.DAL.Controllers.ClientControllers
{
    public class ClientGetAll
    {
        private AppDbContext _context;
        public ClientGetAll(AppDbContext _dbContext)
        {
            _context = _dbContext;
        }

        public async Task<List<Client>> GetAllClients()
        {
            var clients = await _context.Clients.Take(100).ToListAsync();
            return clients;
        }
    }
}
