using LegalTrace.BLL.Controllers.CredentialControllers;
using LegalTrace.Controllers.Services;
using LegalTrace.DAL.Context;
using LegalTrace.Models;
using Microsoft.AspNetCore.Mvc;

namespace LegalTrace.Controllers.CredentialApiControllers
{
    public class GetCredentials
    {
        private AppDbContext _context;
        private readonly ResponseService _responseService;
        public GetCredentials(AppDbContext dbContext)
        {
            _context = dbContext;
            _responseService = new ResponseService();
        }
        public async Task<IActionResult> GetBy(int id)
        {
            var credentialsGetter = new GetCredentialController(_context);
            var credentials = await credentialsGetter.GetCredentialById(id);
            if (credentials.Count() > 0)
            {
                return _responseService.CreateResponse(ApiResponse<object>.SuccessResponse(credentials, "Success when searching for client credentials"));
            }
            return _responseService.CreateResponse(ApiResponse<object>.NotFoundResponse("There are no client credentials with these parameters"));
        }
    }
}
