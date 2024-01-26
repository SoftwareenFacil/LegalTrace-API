using Microsoft.EntityFrameworkCore;
using LegalTrace.DAL.Context;
using LegalTrace.DAL.Models;

namespace LegalTrace.DAL.Controllers.ClientControllers
{
    public class ClientGetByEmail
    {
        private AppDbContext _context;
        public ClientGetByEmail(AppDbContext _dbContext)
        {
            _context = _dbContext;
        }

        public async Task<Client?> GetClientByEmail(string email)
        {
            var client = await _context.Clients.Where(u => u.Email == email).FirstOrDefaultAsync();
            return client;
        }
    }
}
