using Microsoft.EntityFrameworkCore;
using LegalTrace.DAL.Context;
using LegalTrace.DAL.Models;

namespace LegalTrace.DAL.Controllers.ClientControllers
{
    public class ClientUpdate
    {
        private AppDbContext _context;
        public ClientUpdate(AppDbContext _dbContext)
        {
            _context = _dbContext;
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
