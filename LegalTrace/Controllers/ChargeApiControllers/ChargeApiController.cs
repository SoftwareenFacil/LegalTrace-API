using Microsoft.AspNetCore.Mvc;
using LegalTrace.DAL.Context;
using LegalTrace.BLL.Models.ChargeDTO;

namespace LegalTrace.Controllers.ChargeApiControllers
{
    //pullrequest
    [SuperAdminRequired]
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ChargeApiController : ControllerBase
    {
        private AppDbContext _context;
        private readonly string _gdrivefileLoc;
        private readonly string _googleAppName;
        public ChargeApiController(AppDbContext dbContext, IConfiguration configuration)
        {
            _context = dbContext;
            _gdrivefileLoc = configuration["GoogleDriveSecurityLocation"];
            _googleAppName = configuration["GoogleAppName"];
        }

        [HttpGet]
        public async Task<IActionResult> GetCharges(int? id, int? clientId, DateTime? date, string? title, int? amount)
        {
            var chargeGetter = new GetCharges(_context);
            return await chargeGetter.GetBy(id, clientId, date, title, amount);
        }

        [HttpPost]
        public async Task<IActionResult> InsertCharge([FromBody] ChargeInsertDTO charge)
        {
            var add = new InsertCharge(_context, _gdrivefileLoc, _googleAppName);           
            return await add.Insert(charge);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCharge([FromBody] ChargeEditDTO chargeEdited)
        {
            var updater = new UpdateCharge(_context);
            return await updater.Update(chargeEdited);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCharge(int id)
        {
            var deleter = new DeleteCharge(_context);
            return await deleter.Delete(id);
        }
    }
}
