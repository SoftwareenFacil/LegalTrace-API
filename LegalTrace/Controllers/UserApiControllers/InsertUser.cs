using Microsoft.AspNetCore.Mvc;
using LegalTrace.BLL.Models;
using LegalTrace.BLL.Models.UserDTO;
using LegalTrace.BLL.Controllers.UserControllers;
using LegalTrace.UserJwt;
using LegalTrace.DAL.Context;
using LegalTrace.Models;
using LegalTrace.Controllers.Services;


namespace LegalTrace.Controllers.UserApiControllers
{
    public class InsertUser : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ResponseService _responseService;
        public InsertUser(AppDbContext context)
        {
            _context = context;
            _responseService = new ResponseService();
        }

        public async Task<IActionResult> Insert(UserInsertDTO user)
        {
            var userCreator = new AddUsersController(_context);
            var dataModified = await userCreator.AddUser(user);

            if (dataModified > 0)
                return _responseService.CreateResponse(ApiResponse<object>.SuccessResponse($"User created succesfully", "Create completed"));
            else if (dataModified < 0)
                return _responseService.CreateResponse(ApiResponse<object>.BadRequest(user, $"User with Email {user.Email} already exists"));
            return _responseService.CreateResponse(ApiResponse<object>.ErrorResponse("Error trying to Insert User"));

        }
    }
}

