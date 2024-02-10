using LegalTrace.BLL.Controllers.ClientHistoryControllers;
using LegalTrace.Controllers.Services;
using LegalTrace.DAL.Context;
using LegalTrace.Models;
using Microsoft.AspNetCore.Mvc;

namespace LegalTrace.Controllers.ClientHistoryApiControllers
{
    public class DeleteClientHistory
    {
        private AppDbContext _context;
        private readonly ResponseService _responseService;
        public DeleteClientHistory(AppDbContext dbContext)
        {
            _context = dbContext;
            _responseService = new ResponseService();
        }

        public async Task<IActionResult> Delete(int id)
        {
            var deleter = new DeleteClientHistoryController(_context);
            var isDeleted = await deleter.DeleteClientHistoryById(id);
            if (!isDeleted)
            {
                return _responseService.CreateResponse(ApiResponse<object>.NotFoundResponse($"Client History with id {id} not found"));

            }
            return _responseService.CreateResponse(ApiResponse<object>.SuccessResponse($"Client History with ID {id} deleted successfully", "Delete completed"));

        }
    }
}
