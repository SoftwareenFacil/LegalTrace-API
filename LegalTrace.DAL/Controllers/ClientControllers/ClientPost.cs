using LegalTrace.DAL.Context;
using LegalTrace.DAL.Models;

namespace LegalTrace.DAL.Controllers.ClientControllers
{
    public class ClientPost
    {
        private AppDbContext _context;
        public ClientPost(AppDbContext _dbContext)
        {
            _context = _dbContext;
        }

        public async Task<int> InsertClient(Client client)
        {
            await _context.Clients.AddAsync(client);
            return await _context.SaveChangesAsync();
        }
    }
}
