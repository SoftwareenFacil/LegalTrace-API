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

            if (string.IsNullOrWhiteSpace(userTaskEdited.Title) && string.IsNullOrWhiteSpace(userTaskEdited.Description) && string.IsNullOrWhiteSpace(userTaskEdited.Type) && userTaskEdited.DueDate == null && userTaskEdited.UserId == 0 && userTaskEdited.ClientId == 0 )

                return 500;

            var userTaskController = new UserTaskController(_context);
            var userTask = await userTaskController.GetUserTaskById(userTaskEdited.Id);
            if (userTask != null)
            {
                if(userTaskEdited.UserId > 0)
                {
                    var userController = new UserController(_context);
                    var user = await userController.GetUserById(userTaskEdited.UserId);
                    if (user == null)
                        return -2;
                }
                
                if(userTaskEdited.ClientId > 0)
                {
                    var clientController = new ClientController(_context);
                    var client = await clientController.GetClientById(userTaskEdited.ClientId);
                    if (client == null)
                        return -3;
                }

                userTask.Title = !string.IsNullOrEmpty(userTaskEdited.Title) ? userTaskEdited.Title : userTask.Title;
                userTask.Description = !string.IsNullOrEmpty(userTaskEdited.Description) ? userTaskEdited.Description : userTask.Description;
                userTask.DueDate = (((DateTime)userTaskEdited.DueDate) > DateTime.Now) ? DateTime.SpecifyKind((DateTime)userTaskEdited.DueDate, DateTimeKind.Utc) : userTask.DueDate;
                userTask.Type = userTaskEdited.Type;
                userTask.UserId = userTaskEdited.UserId > 0 ? userTaskEdited.UserId : userTask.UserId;
                userTask.ClientId = userTaskEdited.ClientId > 0 ? userTaskEdited.ClientId : userTask.ClientId;
                userTask.Finished = (userTaskEdited.Finished != null) ? (bool)userTaskEdited.Finished : userTask.Finished;
                userTask.Updated = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc);
                var isUpdated = await userTaskController.UpdateUserTask(userTask);
                if (!isUpdated)
                    return -4;
                return 1;
            }
            return -1;
        }

        public async Task<int> UpdateUserTaskVigency(int id)
        {
            var userTaskController = new UserTaskController(_context);
            var userTask = await userTaskController.GetUserTaskById(id);
            if (userTask != null)
            {
                if (userTask.Vigency)
                {
                    userTask.Vigency = false;
                }
                else { userTask.Vigency = true; }

                var isUpdated = await userTaskController.UpdateUserTask(userTask);
                if (isUpdated)
                    return 200;
                return 500;
            }
            return 404;
        }
    }
}
