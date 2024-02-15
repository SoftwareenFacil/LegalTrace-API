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

        public async Task<IActionResult> GetBy(int? id, string? name, string? email, DateTime? created, bool? vigency)
        {
            var userGetter = new GetUsersController(_context);
            var users = await userGetter.GetUsersBy(id,name,email,created,vigency);
            if(users.Count() > 0)
            {
                return _responseService.CreateResponse(ApiResponse<object>.SuccessResponse(200, users, "Success when searching for users"));
            }
            return _responseService.CreateResponse(ApiResponse<object>.NotFoundResponse(404, "There are no users with these parameters"));
        }
    }
}
