using LegalTrace.BLL.Controllers.UserTaskControllers;
using LegalTrace.Controllers.Services;
using LegalTrace.DAL.Context;
using LegalTrace.Models;
using Microsoft.AspNetCore.Mvc;

namespace LegalTrace.Controllers.UserTaskApiControllers
{
    public class DeleteUserTask
    {
        private readonly AppDbContext _context;
        private readonly ResponseService _responseService;
        public DeleteUserTask(AppDbContext dbContext)
        {
            _context = dbContext;
            _responseService = new ResponseService();
        }

        public async Task<IActionResult> Delete(int id)
        {
            var deleter = new DeleteUserTasksController(_context);
            var isDeleted = await deleter.DeleteUserTaskById(id);
            if (!isDeleted)
            {
                return _responseService.CreateResponse(ApiResponse<object>.NotFoundResponse($"User Task with id {id} not found"));

            }
            return _responseService.CreateResponse(ApiResponse<object>.SuccessResponse($"User Task with ID {id} deleted successfully", "Delete completed"));

        }
    }
}
