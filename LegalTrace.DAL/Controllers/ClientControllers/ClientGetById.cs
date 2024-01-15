using Microsoft.EntityFrameworkCore;
using LegalTrace.DAL.Context;
using LegalTrace.DAL.Models;

namespace LegalTrace.DAL.Controllers.ClientControllers
{
    public class ClientGetById
    {
        private AppDbContext _context;
        public ClientGetById(AppDbContext _dbContext)
        {
            _context = _dbContext;
        }

        public async Task<Client> GetClientById(int id)
        {
            var response = await _context.Clients.Where(clientAux => clientAux.Id.Equals(id)).FirstOrDefaultAsync();
            return response;
        }
    }
}
