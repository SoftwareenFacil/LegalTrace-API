using LegalTrace.BLL.Models.CredentialDTO;
using LegalTrace.DAL.Context;
using LegalTrace.DAL.Controllers.CredentialControllers;

namespace LegalTrace.BLL.Controllers.CredentialControllers
{
    public class UpdateCredentialController
    {
        private AppDbContext _context;
        public UpdateCredentialController(AppDbContext _dbContext)
        {
            _context = _dbContext;
        }

        public async Task<int> UpdateCredential(CredentialEditDTO credentialEdited)
        {
            if (credentialEdited.Id > 0)
            {
                if (string.IsNullOrWhiteSpace(credentialEdited.Title) && string.IsNullOrWhiteSpace(credentialEdited.Username) && string.IsNullOrWhiteSpace(credentialEdited.KeyValue))
                    return 400;

                var credentialController = new CredentialController(_context);
                var credential = await credentialController.GetCredentialById(credentialEdited.Id);
                if (credential != null)
                {
                    credential.Title = !string.IsNullOrEmpty(credentialEdited.Title) ? credentialEdited.Title : credential.Title;
                    credential.Username = !string.IsNullOrEmpty(credentialEdited.Username) ? credentialEdited.Username : credential.Username;
                    credential.KeyValue = !string.IsNullOrEmpty(credentialEdited.KeyValue) ? credentialEdited.KeyValue : credential.KeyValue;
                    DateTime utcNow = DateTime.UtcNow;
                    credential.Updated = DateTime.SpecifyKind(utcNow, DateTimeKind.Utc);
                    var isUpdated = await credentialController.UpdateCredential(credential);
                    if (!isUpdated)
                        return 400;
                    return 200;
                }
                return 404;
            }
            return 400;
        }

        public async Task<int> UpdateCredentialVigency(int id)
        {
            var credentialController = new CredentialController(_context);
            var credential = await credentialController.GetCredentialById(id);
            if (credential != null)
            {
                if (credential.Vigency)
                {
                    credential.Vigency = false;
                }
                else { credential.Vigency = true; }

                var isUpdated = await credentialController.UpdateCredential(credential);
                if (isUpdated)
                    return 200;
                return 500;
            }
            return 404;
        }
    }
}
