using LegalTrace.BLL.Models.CredentialDTO;
using LegalTrace.Controllers.CredentialApiControllers;
using LegalTrace.DAL.Context;
using Microsoft.AspNetCore.Mvc;

namespace LegalTrace.Controllers.CredentialApiControllers
{
    [SuperAdminRequired]
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class CredentialApiController : ControllerBase
    {
        private AppDbContext _context;
        public CredentialApiController(AppDbContext dbContext)
        {
            _context = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetCredentials(int id)
        {
            var credentialsGetter = new GetCredentials(_context);
            return await credentialsGetter.GetBy(id);
        }

        [HttpPost]
        public async Task<IActionResult> InsertCredential([FromBody] CredentialInsertDTO credential)
        {
            var add = new InsertCredential(_context);
            return await add.Insert(credential);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCredential([FromBody] CredentialEditDTO credentialEdited)
        {
            var updater = new UpdateCredential(_context);
            return await updater.Update(credentialEdited);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCredential(int id)
        {
            var deleter = new DeleteCredential(_context);
            return await deleter.Delete(id);
        }
    }
}
