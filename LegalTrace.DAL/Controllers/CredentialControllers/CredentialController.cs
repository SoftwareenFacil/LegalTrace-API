using LegalTrace.DAL.Context;
using LegalTrace.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegalTrace.DAL.Controllers.CredentialControllers
{
    public class CredentialController : ICredentialController
    {
        private AppDbContext _context;
        public CredentialController(AppDbContext _dbContext)
        {
            _context = _dbContext;
        }

        public async Task<Credential?> GetCredentialById(int id)
        {
            return await _context.Credentials.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<List<Credential>> GetCredentialsBy(int id)
        {
            if (id == 0)
            {
                return await _context.Credentials.Take(100).ToListAsync();
            }
            else
            {
                var credential = await _context.Credentials.FirstOrDefaultAsync(u => u.Id == id);
                return credential == null ? new List<Credential>() : new List<Credential> { credential };
            }
        }

        public async Task<int> InsertCredential(Credential credential)
        {
            await _context.Credentials.AddAsync(credential);
            return await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateCredential(Credential updatedCredential)
        {
            var credential = await _context.Credentials.FirstOrDefaultAsync(c => c.Id == updatedCredential.Id);
            if (credential != null)
            {
                credential.Vigency = updatedCredential.Vigency;
                credential.Title = updatedCredential.Title;
                credential.Username = updatedCredential.Username;
                credential.KeyValue = updatedCredential.KeyValue;
                credential.Updated = updatedCredential.Updated;

                if (await _context.SaveChangesAsync() > 0)
                    return true;
            }
            return false;
        }

        public async Task<bool> DeleteCredential(int id)
        {
            var credential = await _context.Credentials.FirstOrDefaultAsync(c => c.Id == id);
            if (credential != null)
            {
                _context.Credentials.Remove(credential);
                if (await _context.SaveChangesAsync() > 0)
                {
                    return true;
                }
            }

            return false;
        }

    }
}
