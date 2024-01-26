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
                case 200:
                    return _responseService.CreateResponse(ApiResponse<object>.SuccessResponse($"User Task with ID {userTaskEdited.Id} updated", "Update completed"));
                case 404:
                    return _responseService.CreateResponse(ApiResponse<object>.NotFoundResponse($"User Task with ID {userTaskEdited.Id}, User with ID {userTaskEdited.UserId} or Client with ID {userTaskEdited.ClientId} not found."));
                case 400:
                    return _responseService.CreateResponse(ApiResponse<object>.BadRequest("Error trying to update User Task", "Update rejected"));
                default:
                    return _responseService.CreateResponse(ApiResponse<object>.ErrorResponse("Error trying to update User Task"));
            }
        }
    }
}
