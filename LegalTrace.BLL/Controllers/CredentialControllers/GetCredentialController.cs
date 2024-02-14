using LegalTrace.BLL.Models.CredentialDTO;
using LegalTrace.DAL.Context;
using LegalTrace.DAL.Controllers.CredentialControllers;

namespace LegalTrace.BLL.Controllers.CredentialControllers
{
    public class GetCredentialController
    {
        private AppDbContext _context;
        public GetCredentialController(AppDbContext _dbContext)
        {
            _context = _dbContext;
        }
        public async Task<List<CredentialDTO>> GetCredentialById(int id)
        {
            var credentialController = new CredentialController(_context);
            var credentials = await credentialController.GetCredentialsBy(id);
            if (credentials.Count() > 0)
            {
                List<CredentialDTO> result = new List<CredentialDTO>();
                credentials.ForEach(row => result.Add(new CredentialDTO()
                {
                    Id = row.Id,
                    ClientId = row.ClientId,
                    Title = row.Title,
                    Username = row.Username,
                    KeyValue = row.KeyValue,
                    Created = row.Created,
                    Vigency = row.Vigency
                }));
                return result;
            }

            return new List<CredentialDTO>();
        }
    }
}
