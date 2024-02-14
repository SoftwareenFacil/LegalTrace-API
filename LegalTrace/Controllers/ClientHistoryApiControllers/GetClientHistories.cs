using LegalTrace.BLL.Controllers.ClientHistoryControllers;
using LegalTrace.Controllers.Services;
using LegalTrace.DAL.Context;
using LegalTrace.Models;
using Microsoft.AspNetCore.Mvc;

namespace LegalTrace.Controllers.ClientHistoryApiControllers
{
    public class GetClientHistories
    {
        private AppDbContext _context;
        private readonly ResponseService _responseService;
        public GetClientHistories(AppDbContext dbContext)
        {
            _context = dbContext;
            _responseService = new ResponseService();
        }
        public async Task<IActionResult> GetBy(int id)
        {
            var clientHistoryGetter = new GetClientHistoryController(_context);
            var clientHistories = await clientHistoryGetter.GetClientHistoryById(id);
            if (clientHistories.Count() > 0)
            {
                return _responseService.CreateResponse(ApiResponse<object>.SuccessResponse(clientHistories, "Success when searching for client histories"));
            }
            return _responseService.CreateResponse(ApiResponse<object>.NotFoundResponse("There are no client histories with these parameters"));
        }
    }
}
