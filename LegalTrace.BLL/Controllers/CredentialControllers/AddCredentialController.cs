using LegalTrace.BLL.Models.CredentialDTO;
using LegalTrace.DAL.Context;
using LegalTrace.DAL.Controllers.CredentialControllers;
using LegalTrace.DAL.Models;

namespace LegalTrace.BLL.Controllers.CredentialControllers
{
    public class AddCredentialController
    {
        private AppDbContext _context;
        public AddCredentialController(AppDbContext _dbContext)
        {
            _context = _dbContext;
        }
        public async Task<int> AddCredential(CredentialInsertDTO credential)
        {
            DateTime utcNow = DateTime.UtcNow;
            var credentialController = new CredentialController(_context);
            var credentialCreate = new Credential()
            {
                ClientId = credential.ClientId,
                Title = credential.Title,
                Username = credential.Username,
                KeyValue = credential.KeyValue,
                Vigency = true,
                Created = DateTime.SpecifyKind(utcNow, DateTimeKind.Utc),
                Updated = DateTime.SpecifyKind(utcNow, DateTimeKind.Utc),
            };
            return await credentialController.InsertCredential(credentialCreate);
        }
    }
}
