using LegalTrace.BLL.Controllers.CredentialControllers;
using LegalTrace.BLL.Models.CredentialDTO;
using LegalTrace.Controllers.Services;
using LegalTrace.DAL.Context;
using LegalTrace.Models;
using Microsoft.AspNetCore.Mvc;

namespace LegalTrace.Controllers.CredentialApiControllers
{
    public class UpdateCredential
    {
        private AppDbContext _context;
        private readonly ResponseService _responseService;
        public UpdateCredential(AppDbContext dbContext)
        {
            _context = dbContext;
            _responseService = new ResponseService();
        }
        public async Task<IActionResult> Update(CredentialEditDTO credentialEdited)
        {
            var credentialUpdater = new UpdateCredentialController(_context);
            var code = await credentialUpdater.UpdateCredential(credentialEdited);

            switch (code)
            {
                case 200:
                    return _responseService.CreateResponse(ApiResponse<object>.SuccessResponse(200, $"Client Credential with ID {credentialEdited.Id} updated", "Update completed"));
                case 404:
                    return _responseService.CreateResponse(ApiResponse<object>.NotFoundResponse(404, $"Client Credential with ID {credentialEdited.Id} not found."));
                default:
                    return _responseService.CreateResponse(ApiResponse<object>.ErrorResponse(500, "Error trying to update Client Credential"));
            }
        }
    }
}
