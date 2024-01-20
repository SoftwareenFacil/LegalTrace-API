using LegalTrace.Controllers.Services;
using LegalTrace.DAL.Context;
using LegalTrace.Models;
using Microsoft.AspNetCore.Mvc;
using LegalTrace.BLL.Controllers.UserTaskControllers;

namespace LegalTrace.Controllers.UserTaskApiControllers
{
    public class GetUserTasks : ControllerBase
    {
        
        private readonly AppDbContext _context;
        private readonly ResponseService _responseService;
        public GetUserTasks(AppDbContext dbContext)
        {
            _context = dbContext;
            _responseService = new ResponseService();

        }

        public async Task<IActionResult> GetResponseUserTasks(int id)
        {
            var userTasksGetter = new GetUserTasksController(_context);
            var userTasks = await userTasksGetter.GetUserTasks(id);
            if (userTasks == null)
            {
                return _responseService.CreateResponse(ApiResponse<object>.NotFoundResponse(
                id == 0 ? "There are no user tasks" : $"User task with id {id} not found"));

            }
            return _responseService.CreateResponse(ApiResponse<object>.SuccessResponse(userTasks, "Success when searching for user tasks"));
        }
        
    }
}
