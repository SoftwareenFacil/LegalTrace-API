using LegalTrace.BLL.Models.UserTaskDTO;
using LegalTrace.DAL.Context;
using LegalTrace.DAL.Repository;
using LegalTrace.DAL.Models;

namespace LegalTrace.BLL.Controllers.UserTaskControllers
{
    public class AddUserTasksController
    {
        private AppDbContext _context;
        public AddUserTasksController(AppDbContext _dbContext)
        {
            _context = _dbContext;
        }

        public async Task<int> AddUserTask(UserTaskInsertDTO userTask)
        {
            var userController = new UserRepository(_context);
            var user = await userController.GetUserById(userTask.UserId);
            if (user != null)
            {
                var clientController = new ClientRepository(_context);
                var client = await clientController.GetClientById(userTask.ClientId);
                if(client != null)
                {
                    if (!client.Vigency || !user.Vigency)
                        return -2;
                    if(userTask.DueDate > DateTime.Now && !string.IsNullOrEmpty(userTask.Title) && !string.IsNullOrEmpty(userTask.Description))
                    {

                        DateTime utcNow = DateTime.UtcNow;
                        var userTaskController = new UserTaskRepository(_context);
                        var userTaskCreate = new UserTask()
                        {
                            UserId = userTask.UserId,
                            ClientId = userTask.ClientId,
                            Title = userTask.Title,
                            Description = userTask.Description,
                            Type = userTask.Type,
                            Repeatable = (userTask.Repeatable == 0 ? false : true),
                            Vigency = true,
                            Created = DateTime.SpecifyKind(utcNow, DateTimeKind.Utc),
                            Updated = DateTime.SpecifyKind(utcNow, DateTimeKind.Utc),
                            Finished = false,
                            DueDate = DateTime.SpecifyKind(userTask.DueDate, DateTimeKind.Utc)
                        };
                        if(await userTaskController.InsertUserTask(userTaskCreate) > 0)
                            return userTaskCreate.Id;
                    }

                    return -1;
                }
            }
            return 0;
        }
    }
}
