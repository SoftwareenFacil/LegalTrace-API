using LegalTrace.BLL.Controllers.UserTaskControllers;
using LegalTrace.Controllers.Services;
using LegalTrace.DAL.Context;
using LegalTrace.Models;
using Microsoft.AspNetCore.Mvc;

namespace LegalTrace.Controllers.UserTaskApiControllers
{
    public class CheckRepetitiveTasks
    {
        private readonly AppDbContext _context;
        private readonly ResponseService _responseService;
        public CheckRepetitiveTasks(AppDbContext dbContext)
        {
            _context = dbContext;
            _responseService = new ResponseService();
        }

        public async Task<IActionResult> Check()
        {
            var checker = new CheckUserTasksController(_context);
            var isChecked = await checker.CheckRepetitiveUserTasks();
            if (!isChecked)
            {
                return _responseService.CreateResponse(ApiResponse<object>.ErrorResponse(500, $"User Tasks cannot be checked"));

            }
            return _responseService.CreateResponse(ApiResponse<object>.SuccessResponse(200, null,$"User Tasks checked"));

        }
    }
}
