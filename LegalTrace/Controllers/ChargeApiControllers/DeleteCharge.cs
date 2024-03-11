using LegalTrace.BLL.Controllers.ChargeControllers;
using LegalTrace.Controllers.Services;
using LegalTrace.DAL.Context;
using LegalTrace.Models;
using Microsoft.AspNetCore.Mvc;

namespace LegalTrace.Controllers.ChargeApiControllers
{
    public class DeleteCharge
    {
        private readonly AppDbContext _context;
        private readonly ResponseService _responseService;
        public DeleteCharge(AppDbContext context)
        {
            _context = context;
            _responseService = new ResponseService();
        }

        public async Task<IActionResult> Delete(int id)
        {
            var deleter = new DeleteChargesController(_context);
            var isDeleted = await deleter.DeleteChargeById(id);
            if (!isDeleted)
            {
                return _responseService.CreateResponse(ApiResponse<object>.NotFoundResponse(404, $"Charge with id {id} not found"));

            }
            return _responseService.CreateResponse(ApiResponse<object>.SuccessResponse(200, $"User Task with ID {id} deleted successfully", "Delete completed"));

        }
    }
}
