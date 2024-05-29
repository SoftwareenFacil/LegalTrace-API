using LegalTrace.BLL.Models.UserTaskDTO;
using LegalTrace.DAL.Context;
using LegalTrace.DAL.Controllers.UserTaskControllers;
using LegalTrace.DAL.Models;
using System;

namespace LegalTrace.BLL.Controllers.UserTaskControllers
{
    public class CheckUserTasksController
    {
        private AppDbContext _context;
        public CheckUserTasksController(AppDbContext _dbContext)
        {
            _context = _dbContext;
        }

        public async Task<bool> CheckRepetitiveUserTasks()
        {
            var userTaskController = new UserTaskController(_context);
            var userTasks = await userTaskController.GetRepeatableUserTasks();
            if (userTasks.Any())
            {
                List<UserTask> newUserTaskList = new List<UserTask>();
                DateTime utcNow = DateTime.UtcNow;
                foreach (var userTask in userTasks)
                {
                    userTask.Vigency = false;
                    userTask.Repeatable = false;
                    userTask.Updated = DateTime.SpecifyKind(utcNow, DateTimeKind.Utc);
                    var userTaskCreate = new UserTask()
                    {
                        UserId = userTask.UserId,
                        ClientId = userTask.ClientId,
                        Title = userTask.Title,
                        Description = userTask.Description,
                        Type = userTask.Type,
                        Repeatable = true,
                        Vigency = true,
                        Created = DateTime.SpecifyKind(utcNow, DateTimeKind.Utc),
                        Updated = DateTime.SpecifyKind(utcNow, DateTimeKind.Utc),
                        Finished = false,
                        DueDate = userTask.DueDate.AddDays(7)
                    };
                    newUserTaskList.Add(userTaskCreate);
                }
                var isChecked = await userTaskController.CheckRepeatableUserTasks(userTasks, newUserTaskList);
                
                return isChecked? true : false;
            }
            return false;
        }
    }
}
