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

        public async Task<IActionResult> GetUserTaskBy(int? id, int? userId, int? clientId, DateTime? dueDate, bool? repeatable, bool? vigency)
        {
            var userTasksGetter = new GetUserTasksController(_context);
            var userTasks = await userTasksGetter.GetUserTaskBy(id, userId, clientId, dueDate, repeatable, vigency);
            if(userTasks.Count() > 0)
            {
                return _responseService.CreateResponse(ApiResponse<object>.SuccessResponse(200, userTasks, "Success when searching for user tasks"));
            }
            return _responseService.CreateResponse(ApiResponse<object>.NotFoundResponse(404, "There are no user tasks with these parameters"));
        }
        
    }
}
