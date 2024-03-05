using Microsoft.AspNetCore.Mvc;
using LegalTrace.BLL.Controllers.UserControllers;
using LegalTrace.DAL.Context;
using LegalTrace.Models;
using LegalTrace.Controllers.Services;

namespace LegalTrace.Controllers.UserApiControllers
{
    public class DeleteUser : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ResponseService _responseService;
        public DeleteUser(AppDbContext context)
        {
            _context = context;
            _responseService = new ResponseService();
        }

        public async Task<IActionResult> Delete(int id)
        {
            var deleter = new DeleteUsersController(_context);
            var isDeleted = await deleter.DeleteUserById(id);
            if (!isDeleted)
            {
                return _responseService.CreateResponse(ApiResponse<object>.NotFoundResponse(404, $"User with id {id} not found"));

            }
            return _responseService.CreateResponse(ApiResponse<object>.SuccessResponse(200, $"User with ID {id} deleted successfully", "Delete completed"));

        }
    }
}