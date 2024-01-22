using LegalTrace.BLL.Controllers.ClientControllers;
using LegalTrace.BLL.Controllers.UserControllers;
using LegalTrace.BLL.Models.UserTaskDTO;
using LegalTrace.DAL.Context;
using LegalTrace.DAL.Controllers.UserControllers;
using LegalTrace.DAL.Controllers.ClientControllers;
using LegalTrace.DAL.Controllers.UserTaskControllers;
using LegalTrace.DAL.Models;

namespace LegalTrace.BLL.Controllers.UserTaskControllers
{
    public class UpdateUserTasksController
    {
        private AppDbContext _context;
        public UpdateUserTasksController(AppDbContext _dbContext)
        {
            _context = _dbContext;
        }

        public async Task<(bool, bool, bool, bool)> UpdateUserTask(UserTaskEditDTO userTaskEdited)
        {
            bool isUpdated = false;
            bool isUserTask = false;
            bool isUser = false;
            bool isClient = false;
            if (string.IsNullOrWhiteSpace(userTaskEdited.Title) && string.IsNullOrWhiteSpace(userTaskEdited.Description) && userTaskEdited.DueDate <= DateTime.Now)
                return (isUpdated, isUserTask, isUser, isClient);

            var userTaskValidator = new UserTaskGetById(_context);
            var userTask = await userTaskValidator.GetUserTaskById(userTaskEdited.Id);
            if (userTask != null)
            {
                isUserTask = true;
                var userTaskUpdater = new UserTaskUpdate(_context);

                var userValidator = new UserGetById(_context);
                var user = await userValidator.GetUserById(userTaskEdited.UserId);
                if (user != null)
                    isUser = true;

                var clientValidator = new ClientGetById(_context);
                var client = await clientValidator.GetClientById(userTaskEdited.ClientId);
                if (client != null)
                {
                    isClient = true;
                }

                userTask.Title = !string.IsNullOrEmpty(userTaskEdited.Title) ? userTaskEdited.Title : userTask.Title;
                userTask.Description = !string.IsNullOrEmpty(userTaskEdited.Description) ? userTaskEdited.Description : userTask.Description;
                userTask.DueDate = (userTaskEdited.DueDate > DateTime.Now) ? userTaskEdited.DueDate : userTask.DueDate;
                userTask.Type = userTaskEdited.Type;
                userTask.UserId = user != null ? userTaskEdited.UserId : userTask.UserId;
                userTask.ClientId = client != null ? userTaskEdited.ClientId : userTask.ClientId;
                DateTime utcNow = DateTime.UtcNow;
                userTask.Updated = DateTime.SpecifyKind(utcNow, DateTimeKind.Utc);
                isUpdated = await userTaskUpdater.UpdateUserTask(userTask);
            }
            return (isUpdated, isUserTask, isUser, isClient);
        }
    }
}
