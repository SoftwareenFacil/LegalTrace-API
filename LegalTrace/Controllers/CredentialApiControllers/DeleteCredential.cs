using LegalTrace.BLL.Controllers.CredentialControllers;
using LegalTrace.Controllers.Services;
using LegalTrace.DAL.Context;
using LegalTrace.Models;
using Microsoft.AspNetCore.Mvc;

namespace LegalTrace.Controllers.CredentialApiControllers
{
    public class DeleteCredential
    {
        private AppDbContext _context;
        private readonly ResponseService _responseService;
        public DeleteCredential(AppDbContext dbContext)
        {
            _context = dbContext;
            _responseService = new ResponseService();
        }

        public async Task<IActionResult> Delete(int id)
        {
            var deleter = new DeleteCredentialController(_context);
            var isDeleted = await deleter.DeleteCredentialById(id);
            if (!isDeleted)
            {
                return _responseService.CreateResponse(ApiResponse<object>.NotFoundResponse(404, $"Client Credential with id {id} not found"));

            }
            return _responseService.CreateResponse(ApiResponse<object>.SuccessResponse(200, $"Client Credential with ID {id} deleted successfully", "Delete completed"));

        }
    }
}
