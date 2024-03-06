using LegalTrace.BLL.Controllers.ChargeControllers;
using LegalTrace.BLL.Models.ChargeDTO;
using LegalTrace.Controllers.Services;
using LegalTrace.DAL.Context;
using LegalTrace.Models;
using Microsoft.AspNetCore.Mvc;

namespace LegalTrace.Controllers.ChargeApiControllers
{
    public class InsertCharge
    {
        private readonly AppDbContext _context;
        private readonly ResponseService _responseService;
        public InsertCharge(AppDbContext context)
        {
            _context = context;
            _responseService = new ResponseService();
        }
        public async Task<IActionResult> Insert(ChargeInsertDTO charge)
        {
            var chargeCreator = new AddChargesController(_context);
            var dataModified = await chargeCreator.AddCharge(charge);

            if (dataModified > 0)
                return _responseService.CreateResponse(ApiResponse<object>.SuccessResponse(201, $"Charge created succesfully", "Create completed"));

            if (dataModified == 0)
                return _responseService.CreateResponse(ApiResponse<object>.BadRequest(400, "Bad request trying to insert a Charge", "Insert rejected"));
            return _responseService.CreateResponse(ApiResponse<object>.ErrorResponse(500, "Error trying to Insert an Charge"));

        }
    }
}
