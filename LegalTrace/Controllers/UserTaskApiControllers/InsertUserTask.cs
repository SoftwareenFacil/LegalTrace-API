using LegalTrace.BLL.Controllers.UserTaskControllers;
using LegalTrace.BLL.Models.UserTaskDTO;
using LegalTrace.Controllers.Services;
using LegalTrace.DAL.Context;
using LegalTrace.Models;
using Microsoft.AspNetCore.Mvc;

namespace LegalTrace.Controllers.UserTaskApiControllers
{
    public class InsertUserTask
    {
        private readonly AppDbContext _context;
        private readonly ResponseService _responseService;
        public InsertUserTask(AppDbContext dbContext)
        {
            _context = dbContext;
            _responseService = new ResponseService();

        }
        public async Task<IActionResult> Insert(UserTaskInsertDTO userTask)
        {
            var userTaskCreator = new AddUserTasksController(_context);
            var dataModified = await userTaskCreator.AddUserTask(userTask);

            if (dataModified > 0)
                return _responseService.CreateResponse(ApiResponse<object>.SuccessResponse(201, $"User Task created succesfully", "Create completed"));
            
            return _responseService.CreateResponse(ApiResponse<object>.ErrorResponse(500, "Error trying to Insert User Task"));

         }
    }
}
