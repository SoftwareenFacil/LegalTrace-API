using LegalTrace.BLL.Controllers.ChargeControllers;
using LegalTrace.BLL.Models.ChargeDTO;
using LegalTrace.Controllers.Services;
using LegalTrace.DAL.Context;
using LegalTrace.DAL.Models;
using LegalTrace.GoogleDrive;
using LegalTrace.Models;
using Microsoft.AspNetCore.Mvc;

namespace LegalTrace.Controllers.ChargeApiControllers
{
    public class ChargesController
    {
        private readonly AppDbContext _context;
        private readonly ResponseService _responseService;
        private readonly string _gdrivefileLoc;
        private readonly string _googleAppName;
        public ChargesController(AppDbContext context, string gdrivefileLoc, string GoogleAppName)
        {
            _context = context;
            _responseService = new ResponseService();
            _gdrivefileLoc = gdrivefileLoc;
            _googleAppName = GoogleAppName;
        }
        public async Task<IActionResult> Insert(ChargeInsertDTO charge)
        {

            var chargeCreator = new AddChargesController(_context, _gdrivefileLoc, _googleAppName);
            var dataModified = await chargeCreator.AddCharge(charge);

            if (dataModified > 0)
                return _responseService.CreateResponse(ApiResponse<object>.SuccessResponse(201, $"Charge created succesfully", "Create completed"));

            if (dataModified == 0)
                return _responseService.CreateResponse(ApiResponse<object>.BadRequest(400, "Bad request trying to insert a Charge", "Insert rejected"));
            return _responseService.CreateResponse(ApiResponse<object>.ErrorResponse(500, "Error trying to Insert an Charge"));

        }
        public async Task<IActionResult> DownloadFile(string id)
        {
            if (string.IsNullOrEmpty(id))
                return _responseService.CreateResponse(ApiResponse<object>.BadRequest(400, "String is empty or not found", "Insert rejected"));
            var library = new GoogleDriveLibrary(_gdrivefileLoc, _googleAppName);
            var file = await library.DownloadFile(id);
            var fileString = library.TransformMemoryStreamToString(file.Item3);
            if (!string.IsNullOrEmpty(fileString))
            {
                var FileInfo = new GoogleDrive.Models.GoogleFileDTO()
                {
                    Name = file.Item1,
                    Type = file.Item2,
                    FileString = fileString
                };
                return _responseService.CreateResponse(ApiResponse<object>.SuccessResponse(200, FileInfo, "Success"));
            }
            return _responseService.CreateResponse(ApiResponse<object>.ErrorResponse(500, "Error trying to retrieve file"));
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
        public async Task<IActionResult> Update(ChargeEditDTO chargeEdited)
        {
            var chargeUpdater = new UpdateChargesController(_context, _gdrivefileLoc, _googleAppName);
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
