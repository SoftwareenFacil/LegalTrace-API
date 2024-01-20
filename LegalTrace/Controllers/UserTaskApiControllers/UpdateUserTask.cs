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
            var (isUpdated, isUserTask, isUser, isClient) = await userTaskUpdater.UpdateUserTask(userTaskEdited);

            if (isUpdated)
                return _responseService.CreateResponse(ApiResponse<object>.SuccessResponse($"User Task with ID {userTaskEdited.Id} updated", "Update completed"));
            else if (!isUserTask)
                return _responseService.CreateResponse(ApiResponse<object>.NotFoundResponse($"User Task with ID {userTaskEdited.Id} not found."));
            else if (!isUser)
                return _responseService.CreateResponse(ApiResponse<object>.NotFoundResponse($"User with ID {userTaskEdited.UserId} not found."));
            else if (!isClient)
                return _responseService.CreateResponse(ApiResponse<object>.NotFoundResponse($"Client with ID {userTaskEdited.ClientId} not found."));
            return _responseService.CreateResponse(ApiResponse<object>.ErrorResponse("Error trying to update User Task"));
        }
    }
}
