using Microsoft.AspNetCore.Mvc;
using LegalTrace.BLL.Controllers.ClientControllers;
using LegalTrace.BLL.Models.ClientDTO;
using LegalTrace.Controllers.Services;
using LegalTrace.DAL.Context;
using LegalTrace.Models;

namespace LegalTrace.Controllers.ClientApiControllers
{
    public class UpdateClient
    {
        private readonly AppDbContext _context;
        private readonly ResponseService _responseService;
        public UpdateClient(AppDbContext context)
        {
            _context = context;
            _responseService = new ResponseService();
        }

        public async Task<IActionResult> Update(ClientEditDTO clientEdited)
        {
            var clientUpdater = new UpdateClientsController(_context);
            var (isUpdated, isClient) = await clientUpdater.UpdateClient(clientEdited);

            if (isUpdated)
                return _responseService.CreateResponse(ApiResponse<object>.SuccessResponse($"Client with ID {clientEdited.Id} updated","Update completed"));
            else if (!isClient)
                return _responseService.CreateResponse(ApiResponse<object>.NotFoundResponse($"User with ID {clientEdited.Id} not found."));
            return _responseService.CreateResponse(ApiResponse<object>.ErrorResponse("Error trying to update User"));
        }
    }
}
