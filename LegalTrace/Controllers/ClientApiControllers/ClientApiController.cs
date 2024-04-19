using Microsoft.AspNetCore.Mvc;
using LegalTrace.DAL.Context;
using LegalTrace.BLL.Models.ClientDTO;

namespace LegalTrace.Controllers.ClientApiControllers
{
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
        public async Task<IActionResult> GetClients(int? id, string? name, string? email, string? taxId, DateTime? createdFrom, DateTime? createdTo, bool? vigency)
        {
            var userGetter = new GetClients(_context);
            return await userGetter.GetBy(id, name, email, taxId, createdFrom, createdTo, vigency);
        }
        [SuperAdminRequired]

        [HttpPost]
        public async Task<IActionResult> InsertClient([FromBody] ClientInsertDTO client)
        {
            var add = new InsertClient(_context);
            return await add.Insert(client);
        }
        [SuperAdminRequired]

        [HttpPut]
        public async Task<IActionResult> UpdateClient([FromBody] ClientEditDTO clientEdited)
        {
            var updater = new UpdateClient(_context);
            return await updater.Update(clientEdited);
        }
        [SuperAdminRequired]

        [HttpPut]
        public async Task<IActionResult> UpdateClientVigency(int id)
        {
            var updater = new UpdateClient(_context);
            return await updater.UpdateVigency(id);
        }
        [SuperAdminRequired]

        [HttpDelete]
        public async Task<IActionResult> DeleteClient(int id)
        {
            var deleter = new DeleteClient(_context);
            return await deleter.Delete(id);
        }
    }
}