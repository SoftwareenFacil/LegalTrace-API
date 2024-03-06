using LegalTrace.BLL.Controllers.ChargeControllers;
using LegalTrace.Controllers.Services;
using LegalTrace.DAL.Context;
using LegalTrace.Models;
using Microsoft.AspNetCore.Mvc;

namespace LegalTrace.Controllers.ChargeApiControllers
{
    public class GetCharges
    {
        private readonly AppDbContext _context;
        private readonly ResponseService _responseService;
        public GetCharges(AppDbContext context)
        {
            _context = context;
            _responseService = new ResponseService();
        }
        public async Task<IActionResult> GetBy(int? id, int? clientId, DateTime? date, string? title, int? amount)
        {
            var chargesGetter = new GetChargesController(_context);
            var charge = await chargesGetter.GetChargeBy(id, clientId, date, title, amount);
            if (charge.Count() > 0)
            {
                return _responseService.CreateResponse(ApiResponse<object>.SuccessResponse(200, charge, "Success when searching for charges"));
            }
            return _responseService.CreateResponse(ApiResponse<object>.NotFoundResponse(404, "There are no Charges with these parameters"));
        }
    }
}
