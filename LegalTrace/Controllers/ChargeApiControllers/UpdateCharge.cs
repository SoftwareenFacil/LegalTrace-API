using LegalTrace.BLL.Controllers.ChargeControllers;
using LegalTrace.BLL.Models.ChargeDTO;
using LegalTrace.Controllers.Services;
using LegalTrace.DAL.Context;
using LegalTrace.Models;
using Microsoft.AspNetCore.Mvc;

namespace LegalTrace.Controllers.ChargeApiControllers
{
    public class UpdateCharge
    {
        private readonly AppDbContext _context;
        private readonly ResponseService _responseService;
        public UpdateCharge(AppDbContext context)
        {
            _context = context;
            _responseService = new ResponseService();
        }

        public async Task<IActionResult> Update(ChargeEditDTO chargeEdited)
        {
            var chargeUpdater = new UpdateChargesController(_context);
            var code = await chargeUpdater.UpdateCharge(chargeEdited);

            switch (code)
            {
                case 200:
                    return _responseService.CreateResponse(ApiResponse<object>.SuccessResponse(200, $"Charge with ID {chargeEdited.Id} updated", "Update completed"));
                case 404:
                    return _responseService.CreateResponse(ApiResponse<object>.NotFoundResponse(404, $"Charge with ID {chargeEdited.Id} not found."));
                case -1:
                    return _responseService.CreateResponse(ApiResponse<object>.NotFoundResponse(404, $"Client with ID {chargeEdited.ClientId} not found."));
                default:
                    return _responseService.CreateResponse(ApiResponse<object>.ErrorResponse(500, "Error trying to update Charge"));
            }
        }
    }
}
