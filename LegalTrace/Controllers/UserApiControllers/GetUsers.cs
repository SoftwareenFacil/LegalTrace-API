using Microsoft.AspNetCore.Mvc;
using LegalTrace.BLL.Controllers.UserControllers;
using LegalTrace.UserJwt;
using LegalTrace.DAL.Context;
using LegalTrace.BLL.Models;
using LegalTrace.Models;
using LegalTrace.Controllers.Services;
using System.Runtime.CompilerServices;
using LegalTrace.DAL.Models;

namespace LegalTrace.Controllers.UserApiControllers
{
    public class GetUsers : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ResponseService _responseService;
        public GetUsers(AppDbContext dbContext)
        {
            _context = dbContext;
            _responseService = new ResponseService();

        }

        public async Task<IActionResult> GetResponseUsers(int id)
        {
            var userGetter = new GetUsersController(_context);
            var user = await userGetter.GetUsers(id);
            if (user == null)
            {
                return _responseService.CreateResponse(ApiResponse<object>.NotFoundResponse(
                id == 0 ? "There are no users" : $"User with id {id} not found"));
            }
            return _responseService.CreateResponse(ApiResponse<object>.SuccessResponse(user, "Success when searching for users"));
        }

        public async Task<IActionResult> GetBy(int? id, string? name, string? email, DateTime? created, bool? vigency)
        {
            var userGetter = new GetUsersController(_context);
            var users = await userGetter.GetUsersBy(id,name,email,created,vigency);
            if(users.Count() > 0)
            {
                return _responseService.CreateResponse(ApiResponse<object>.SuccessResponse(users, "Success when searching for users"));
            }
            return _responseService.CreateResponse(ApiResponse<object>.NotFoundResponse("There are no users with these parameters"));
        }
    }
}
