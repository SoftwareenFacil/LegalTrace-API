using Microsoft.EntityFrameworkCore;
using LegalTrace.DAL.Context;

namespace LegalTrace.DAL.Controllers.ClientControllers
{
    public class ClientDelete
    {
        private AppDbContext _context;
        public ClientDelete(AppDbContext _dbContext)
        {
            _context = _dbContext;
        }

        public async Task<bool> DeleteClient(int id)
        {
            var client = await _context.Clients.Where(clientAux => clientAux.Id.Equals(id)).FirstOrDefaultAsync();
            _context.Clients.Remove(client);
            if (await _context.SaveChangesAsync() > 0) { 
                return true;
            }
            return false;
        }
    }
}
