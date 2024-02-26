using LegalTrace.BLL.Controllers.ClientHistoryControllers;
using LegalTrace.BLL.Models.ClientHistoryDTO;
using LegalTrace.Controllers.Services;
using LegalTrace.DAL.Context;
using LegalTrace.Models;
using Microsoft.AspNetCore.Mvc;

namespace LegalTrace.Controllers.ClientHistoryApiControllers
{
    public class UpdateClientHistory
    {
        private AppDbContext _context;
        private readonly ResponseService _responseService;
        public UpdateClientHistory(AppDbContext dbContext)
        {
            _context = dbContext;
            _responseService = new ResponseService();
        }
        public async Task<IActionResult> Update(ClientHistoryEditDTO clientHistoryEdited)
        {
            var clientHistoryUpdater = new UpdateClientHistoryController(_context);
            var code = await clientHistoryUpdater.UpdateClientHistory(clientHistoryEdited);

            switch (code)
            {
                case 200:
                    return _responseService.CreateResponse(ApiResponse<object>.SuccessResponse(200, $"Client History with ID {clientHistoryEdited.Id} updated", "Update completed"));
                case 404:
                    return _responseService.CreateResponse(ApiResponse<object>.NotFoundResponse(404, $"Client History with ID {clientHistoryEdited.Id} not found."));
                default:
                    return _responseService.CreateResponse(ApiResponse<object>.ErrorResponse(500, "Error trying to update Client History"));
            }
        }

        public async Task<IActionResult> UpdateVigency(int id)
        {
            var clientHistoryUpdater = new UpdateClientHistoryController(_context);
            var code = await clientHistoryUpdater.UpdateClientHistoryVigency(id);

            switch (code)
            {
                case 200:
                    return _responseService.CreateResponse(ApiResponse<object>.SuccessResponse(200, $"Client History Vigency with ID {id} updated", "Update completed"));
                case 404:
                    return _responseService.CreateResponse(ApiResponse<object>.NotFoundResponse(404, $"Client History Vigency with ID {id} not found."));
                default:
                    return _responseService.CreateResponse(ApiResponse<object>.BadRequest(400, null, "Error trying to update the Client History Vigency"));
            }
        }
    }
}
