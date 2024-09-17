using LegalTrace.BLL.Models.ChargeDTO;
using LegalTrace.Controllers.Services;
using LegalTrace.DAL.Context;
using LegalTrace.GoogleDrive;
using LegalTrace.GoogleDrive.Models;
using LegalTrace.Models;
using Microsoft.AspNetCore.Mvc;

namespace LegalTrace.Controllers.ChargeApiControllers
{
    public class ChargesController
    {
        private readonly ResponseService _responseService;
        private readonly GoogleServiceAccountJson _gdriveServiceAccount;
        private readonly string _googleAppName;
        private readonly BLL.Controllers.ChargesController _BLL;
        public ChargesController(AppDbContext context, GoogleServiceAccountJson _gdriveServiceAccount, string GoogleAppName)
        {
            _responseService = new ResponseService();
            _BLL = new BLL.Controllers.ChargesController(context, _gdriveServiceAccount, GoogleAppName);
        }
        public async Task<IActionResult> Insert(ChargeInsertDTO charge)
        {

            var dataModified = await _BLL.AddCharge(charge);

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
            var library = new GoogleDriveLibrary(_gdriveServiceAccount, _googleAppName);
            var file = await library.DownloadFile(id);
            var fileString = library.TransformMemoryStreamToString(file.Item3);
            if (!string.IsNullOrEmpty(fileString))
            {
                var FileInfo = new GoogleFileDTO()
                {
                    Name = file.Item1,
                    Type = file.Item2,
                    FileString = fileString
                };
                return _responseService.CreateResponse(ApiResponse<object>.SuccessResponse(200, FileInfo, "Success"));
            }
            return _responseService.CreateResponse(ApiResponse<object>.ErrorResponse(500, "Error trying to retrieve file"));
        }
        public async Task<IActionResult> GetBy(int? id, int? clientId, DateTime? date, DateTime? dateTo, string? title, int? amount, int? type)
        {
            var charge = await _BLL.GetChargeBy(id, clientId, date, dateTo, title, amount, type);
            if (charge.Count() > 0)
            {
                return _responseService.CreateResponse(ApiResponse<object>.SuccessResponse(200, charge, "Success when searching for charges"));
            }
            return _responseService.CreateResponse(ApiResponse<object>.NotFoundResponse(404, "There are no Charges with these parameters"));
        }
        public async Task<IActionResult> Update(ChargeEditDTO chargeEdited)
        {
            var code = await _BLL.UpdateCharge(chargeEdited);

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
            var isDeleted = await _BLL.DeleteChargeById(id);
            if (!isDeleted)
            {
                return _responseService.CreateResponse(ApiResponse<object>.NotFoundResponse(404, $"Charge with id {id} not found"));

            }
            return _responseService.CreateResponse(ApiResponse<object>.SuccessResponse(200, $"User Task with ID {id} deleted successfully", "Delete completed"));

        }
    }
}
