using LegalTrace.BLL.Controllers.ClientHistoryControllers;
using LegalTrace.BLL.Controllers.ClientControllers;
using LegalTrace.BLL.Models.ClientHistoryDTO;
using LegalTrace.Controllers.Services;
using LegalTrace.DAL.Context;
using LegalTrace.Models;
using Microsoft.AspNetCore.Mvc;

namespace LegalTrace.Controllers.ClientHistoryApiControllers
{
    public class InsertClientHistory
    {
        private AppDbContext _context;
        private readonly ResponseService _responseService;
        public InsertClientHistory(AppDbContext dbContext)
        {
            _context = dbContext;
            _responseService = new ResponseService();
        }
        public async Task<IActionResult> Insert(ClientHistoryInsertDTO clientHistory)
        {
            var clientVerify = new GetClientsController(_context);
            var client = await clientVerify.GetClientById(clientHistory.ClientId);
            if (client != null)
            {
                var clientHistoryCreator = new AddClientHistoryController(_context);
                var dataModified = await clientHistoryCreator.AddClientHistory(clientHistory);

                if (dataModified > 0)
                    return _responseService.CreateResponse(ApiResponse<object>.SuccessResponse(201, $"Client history created succesfully", "Create completed"));

                return _responseService.CreateResponse(ApiResponse<object>.BadRequest(400, "Error trying to Insert a Client History", "Insert rejected"));
            }
            return _responseService.CreateResponse(ApiResponse<object>.NotFoundResponse(404,$"Client with ID {clientHistory.ClientId} not found."));
        }
    }
}
