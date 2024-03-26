using LegalTrace.BLL.Models.ClientHistoryDTO;
using LegalTrace.DAL.Context;
using Microsoft.AspNetCore.Mvc;

namespace LegalTrace.Controllers.ClientHistoryApiControllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ClientHistoryApiController : ControllerBase
    {
        private AppDbContext _context;
        public ClientHistoryApiController(AppDbContext dbContext)
        {
            _context = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetClientHistories(int id)
        {
            var clientHistoryGetter = new GetClientHistories(_context);
            return await clientHistoryGetter.GetBy(id);
        }

        [HttpPost]
        public async Task<IActionResult> InsertClientHistory([FromBody] ClientHistoryInsertDTO clientHistory)
        {
            var add = new InsertClientHistory(_context);
            return await add.Insert(clientHistory);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateClientHistory([FromBody] ClientHistoryEditDTO clientHistoryEdited)
        {
            var updater = new UpdateClientHistory(_context);
            return await updater.Update(clientHistoryEdited);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateClientHistoryVigency(int id)
        {
            var updater = new UpdateClientHistory(_context);
            return await updater.UpdateVigency(id);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteClientHistory(int id)
        {
            var deleter = new DeleteClientHistory(_context);
            return await deleter.Delete(id);
        }
    }
}
