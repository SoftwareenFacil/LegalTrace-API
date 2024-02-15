using LegalTrace.BLL.Controllers.UserTaskControllers;
using LegalTrace.BLL.Models.UserTaskDTO;
using LegalTrace.Controllers.Services;
using LegalTrace.DAL.Context;
using LegalTrace.Models;
using Microsoft.AspNetCore.Mvc;

namespace LegalTrace.Controllers.UserTaskApiControllers
{
    public class UpdateUserTask
    {
        private readonly AppDbContext _context;
        private readonly ResponseService _responseService;
        public UpdateUserTask(AppDbContext dbContext)
        {
            _context = dbContext;
            _responseService = new ResponseService();
        }
        public async Task<IActionResult> Update(UserTaskEditDTO userTaskEdited)
        {
            var userTaskUpdater = new UpdateUserTasksController(_context);
            var code = await userTaskUpdater.UpdateUserTask(userTaskEdited);

            switch (code)
            {
                case 1:
                    return _responseService.CreateResponse(ApiResponse<object>.SuccessResponse(200, $"User Task with ID {userTaskEdited.Id} updated", "Update completed"));
                case -1:
                    return _responseService.CreateResponse(ApiResponse<object>.NotFoundResponse(404, $"User Task with ID {userTaskEdited.Id} not found."));
                case -2:
                    return _responseService.CreateResponse(ApiResponse<object>.NotFoundResponse(404, $"User with ID {userTaskEdited.UserId} not found."));
                case -3:
                    return _responseService.CreateResponse(ApiResponse<object>.NotFoundResponse(404, $"Client with ID {userTaskEdited.ClientId} not found."));
                case -4:
                    return _responseService.CreateResponse(ApiResponse<object>.BadRequest(400, "Error trying to update User Task", "Update rejected"));
                default:
                    return _responseService.CreateResponse(ApiResponse<object>.ErrorResponse(500, "Error trying to update User Task"));
            }
        }
    }
}
