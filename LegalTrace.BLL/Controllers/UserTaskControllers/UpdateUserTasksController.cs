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

        public async Task<int> UpdateUserTask(UserTaskEditDTO userTaskEdited)
        {

            if (string.IsNullOrWhiteSpace(userTaskEdited.Title) && string.IsNullOrWhiteSpace(userTaskEdited.Description) && string.IsNullOrWhiteSpace(userTaskEdited.Type) && userTaskEdited.DueDate <= DateTime.Now && userTaskEdited.UserId == 0 && userTaskEdited.ClientId == 0 )

                return 500;

            var userTaskValidator = new UserTaskGetById(_context);
            var userTask = await userTaskValidator.GetUserTaskById(userTaskEdited.Id);
            if (userTask != null)
            {
                if(userTaskEdited.UserId > 0)
                {
                    var userValidator = new UserGetById(_context);
                    var user = await userValidator.GetUserById(userTaskEdited.UserId);
                    if (user == null)
                        return -2;
                }
                
                if(userTaskEdited.ClientId > 0)
                {
                    var clientValidator = new ClientGetById(_context);
                    var client = await clientValidator.GetClientById(userTaskEdited.ClientId);
                    if (client == null)
                        return -3;
                }

                var userTaskUpdater = new UserTaskUpdate(_context);
                userTask.Title = !string.IsNullOrEmpty(userTaskEdited.Title) ? userTaskEdited.Title : userTask.Title;
                userTask.Description = !string.IsNullOrEmpty(userTaskEdited.Description) ? userTaskEdited.Description : userTask.Description;
                userTask.DueDate = (userTaskEdited.DueDate > DateTime.Now) ? userTaskEdited.DueDate : userTask.DueDate;
                userTask.Type = userTaskEdited.Type;
                userTask.UserId = userTaskEdited.UserId > 0 ? userTaskEdited.UserId : userTask.UserId;
                userTask.ClientId = userTaskEdited.ClientId > 0 ? userTaskEdited.ClientId : userTask.ClientId;
                DateTime utcNow = DateTime.UtcNow;
                userTask.Updated = DateTime.SpecifyKind(utcNow, DateTimeKind.Utc);
                var isUpdated = await userTaskUpdater.UpdateUserTask(userTask);
                if (!isUpdated)
                    return -4;
                return 1;
            }
            return -1;
        }
    }
}
