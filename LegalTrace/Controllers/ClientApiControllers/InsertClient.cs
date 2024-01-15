using Microsoft.AspNetCore.Mvc;
using LegalTrace.BLL.Controllers.ClientControllers;
using LegalTrace.BLL.Models.ClientDTO;
using LegalTrace.Controllers.Services;
using LegalTrace.DAL.Context;
using LegalTrace.Models;

namespace LegalTrace.Controllers.ClientApiControllers
{
    public class InsertClient
    {
        private readonly AppDbContext _context;
        private readonly ResponseService _responseService;
        public InsertClient(AppDbContext context)
        {
            _context = context;
            _responseService = new ResponseService();
        }

        public async Task<IActionResult> Insert(ClientInsertDTO client)
        {
            var clientCreator = new AddClientsController(_context);
            var dataModified = await clientCreator.AddClient(client);

            if (dataModified > 0)
                return _responseService.CreateResponse(ApiResponse<object>.SuccessResponse($"Client with Email {client.Email} created succesfully","Create completed"));
            else if (dataModified < 0)
                return _responseService.CreateResponse(ApiResponse<object>.BadRequest(client, $"Client with Email {client.Email} already exists"));
            return _responseService.CreateResponse(ApiResponse<object>.ErrorResponse("Error trying to create a Client"));

        }
    }
}
