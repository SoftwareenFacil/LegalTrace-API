using LegalTrace.DAL.Context;
using LegalTrace.DAL.Controllers.CredentialControllers;

namespace LegalTrace.BLL.Controllers.CredentialControllers
{
    public class DeleteCredentialController
    {
        private AppDbContext _context;
        public DeleteCredentialController(AppDbContext _dbContext)
        {
            _context = _dbContext;
        }
        public async Task<bool> DeleteCredentialById(int id)
        {
            var credentialController = new CredentialController(_context);
            var exist = await credentialController.GetCredentialById(id);
            if (exist == null)
            {
                return false;
            }

            return await credentialController.DeleteCredential(id);
        }
    }
}
