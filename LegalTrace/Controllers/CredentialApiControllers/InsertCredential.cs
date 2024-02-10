using LegalTrace.BLL.Controllers.ClientControllers;
using LegalTrace.BLL.Controllers.CredentialControllers;
using LegalTrace.BLL.Models.CredentialDTO;
using LegalTrace.Controllers.Services;
using LegalTrace.DAL.Context;
using LegalTrace.Models;
using Microsoft.AspNetCore.Mvc;

namespace LegalTrace.Controllers.CredentialApiControllers
{
    public class InsertCredential
    {
        private AppDbContext _context;
        private readonly ResponseService _responseService;
        public InsertCredential(AppDbContext dbContext)
        {
            _context = dbContext;
            _responseService = new ResponseService();
        }
        public async Task<IActionResult> Insert(CredentialInsertDTO credential)
        {
            var clientVerify = new GetClientsController(_context);
            var client = await clientVerify.GetClientById(credential.ClientId);
            if (client != null)
            {
                var credentialCreator = new AddCredentialController(_context);
                var dataModified = await credentialCreator.AddCredential(credential);

                if (dataModified > 0)
                    return _responseService.CreateResponse(ApiResponse<object>.SuccessResponse($"Client Credential created succesfully", "Create completed"));

                return _responseService.CreateResponse(ApiResponse<object>.ErrorResponse("Error trying to create a Client Credential"));
            }
            return _responseService.CreateResponse(ApiResponse<object>.NotFoundResponse($"Client with ID {credential.ClientId} not found."));
        }
    }
}
