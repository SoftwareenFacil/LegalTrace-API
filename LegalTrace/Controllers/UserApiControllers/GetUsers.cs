using Microsoft.AspNetCore.Mvc;
using LegalTrace.BLL.Controllers.UserControllers;
using LegalTrace.UserJwt;
using LegalTrace.DAL.Context;
using LegalTrace.BLL.Models;
using LegalTrace.Models;
using LegalTrace.Controllers.Services;

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
    }
}
