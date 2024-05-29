using Microsoft.AspNetCore.Mvc;
using LegalTrace.BLL.Controllers.ClientControllers;
using LegalTrace.Controllers.Services;
using LegalTrace.DAL.Context;
using LegalTrace.Models;
using LegalTrace.BLL.Controllers.UserControllers;

namespace LegalTrace.Controllers.ClientApiControllers
{
    public class GetClients
    {
        private readonly AppDbContext _context;
        private readonly ResponseService _responseService;
        public GetClients(AppDbContext context)
        {
            _context = context;
            _responseService = new ResponseService();
        }

        public async Task<IActionResult> GetBy(int? id, string? name, string? email, string? taxId, DateTime? createdFrom, DateTime? createdTo, bool? vigency)
        {
            var clientGetter = new GetClientsController(_context);
            var clients = await clientGetter.GetClientBy(id, name, email, taxId, createdFrom, createdTo, vigency);
            if (clients.Count() > 0)
            {
                return _responseService.CreateResponse(ApiResponse<object>.SuccessResponse(200, clients, "Success when searching for clients"));
            }
            return _responseService.CreateResponse(ApiResponse<object>.NotFoundResponse(404,"There are no clients with these parameters"));
        }
    }
}
