using Microsoft.AspNetCore.Mvc;
using LegalTrace.BLL.Models.UserDTO;
using LegalTrace.BLL.Controllers.UserControllers;
using LegalTrace.DAL.Context;
using LegalTrace.Models;
using LegalTrace.Controllers.Services;

namespace LegalTrace.Controllers.UserApiControllers
{
    public class UpdateUser : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ResponseService _responseService;
        public UpdateUser(AppDbContext context)
        {
            _context = context;
            _responseService = new ResponseService();
        }

        public async Task<IActionResult> Update(UserEditDTO userEdited)
        {
            var userUpdater = new UpdateUsersController(_context);
            var code = await userUpdater.UpdateUser(userEdited);

            switch (code)
            {
                case 200:
                    return _responseService.CreateResponse(ApiResponse<object>.SuccessResponse(200, $"User with ID {userEdited.Id} updated", "Update completed"));
                case 404:
                    return _responseService.CreateResponse(ApiResponse<object>.NotFoundResponse(404, $"User with ID {userEdited.Id} not found"));
                default:
                    return _responseService.CreateResponse(ApiResponse<object>.ErrorResponse(500, "Error trying to update User"));
            }
        }

        public async Task<IActionResult> UpdateVigency(int id)
        {
            var userUpdater = new UpdateUsersController(_context);
            var code = await userUpdater.UpdateUserVigency(id);

            switch (code)
            {
                case 200:
                    return _responseService.CreateResponse(ApiResponse<object>.SuccessResponse(200, $"User Vigency with ID {id} updated", "Update completed"));
                case 404:
                    return _responseService.CreateResponse(ApiResponse<object>.NotFoundResponse(404, $"User Vigency with ID {id} not found."));
                default:
                    return _responseService.CreateResponse(ApiResponse<object>.BadRequest(400, null, "Error trying to update User Vigency"));
            }
        }
    }
}
