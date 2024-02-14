using Microsoft.AspNetCore.Mvc;
using LegalTrace.DAL.Context;
using LegalTrace.BLL.Models.ClientDTO;

namespace LegalTrace.Controllers.ClientApiControllers
{
    [SuperAdminRequired]
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ClientApiController : ControllerBase
    {
        private AppDbContext _context;
        public ClientApiController(AppDbContext dbContext)
        {
            _context = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetClients(int? id, string? name, string? email, string? taxId, DateTime? created, bool? vigency)
        {
            var userGetter = new GetClients(_context);
            return await userGetter.GetBy(id, name, email, taxId, created, vigency);
        }

        [HttpPost]
        public async Task<IActionResult> InsertClient([FromBody] ClientInsertDTO client)
        {
            var add = new InsertClient(_context);
            return await add.Insert(client);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateClient([FromBody] ClientEditDTO clientEdited)
        {
            var updater = new UpdateClient(_context);
            return await updater.Update(clientEdited);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteClient(int id)
        {
            var deleter = new DeleteClient(_context);
            return await deleter.Delete(id);
        }
    }
}