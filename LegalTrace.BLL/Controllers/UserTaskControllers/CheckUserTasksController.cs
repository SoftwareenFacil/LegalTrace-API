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
            var userTaskGetter = new UserTaskGetAll(_context);
            var userTasks = await userTaskGetter.GetAllUserTasks();
            if (userTasks.Any())
            {
                var necesaryUserTasks = new List<UserTask>();
                var newUserTasks = new List<UserTask>();
                DateTime utcNow = DateTime.UtcNow;

                foreach (var userTask in userTasks)
                {
                    if (userTask.Vigency == true && userTask.Repeatable == true)
                    {
                        userTask.Vigency = false;
                        userTask.Repeatable = false;
                        necesaryUserTasks.Add(userTask);

                        newUserTasks.Add(userTask);
                    }
                }

                var userTaskUpdater = new UserTaskUpdate(_context);
                foreach (var userTask in necesaryUserTasks)
                {
                    await userTaskUpdater.UpdateUserTask(userTask);
                }

                var userTaskCreator = new UserTaskPost(_context);
                foreach (var userTask in newUserTasks)
                {
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

                    await userTaskCreator.InsertUserTask(userTaskCreate);
                }
                return true;
            }
            return false;
        }
    }
}
