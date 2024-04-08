using Microsoft.AspNetCore.Mvc;
using LegalTrace.DAL.Context;
using LegalTrace.BLL.Models.ChargeDTO;

namespace LegalTrace.Controllers.ChargeApiControllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ChargeApiController : ControllerBase
    {
        private AppDbContext _context;
        private readonly string _gdrivefileLoc;
        private readonly string _googleAppName;
        private readonly ChargesController _chargesController;
        public ChargeApiController(AppDbContext dbContext, IConfiguration configuration)
        {
            _context = dbContext;
            _gdrivefileLoc = configuration["GoogleDriveSecurityLocation"];
            _googleAppName = configuration["GoogleAppName"];
            _chargesController = new ChargesController(_context, _gdrivefileLoc, _googleAppName);
        }

        [HttpGet]
        public async Task<IActionResult> GetCharges(int? id, int? clientId, DateTime? date, string? title, int? amount)
        {
            return await _chargesController.GetBy(id, clientId, date, title, amount);
        }

        [HttpGet]
        public async Task<IActionResult> DownloadFile(string id)
        {
            return await _chargesController.DownloadFile(id);
        }

        [HttpPost]
        public async Task<IActionResult> InsertCharge([FromBody] ChargeInsertDTO charge)
        {       
            return await _chargesController.Insert(charge);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCharge([FromBody] ChargeEditDTO chargeEdited)
        {
            return await _chargesController.Update(chargeEdited);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCharge(int id)
        {
            return await _chargesController.Delete(id);
        }
    }
}
