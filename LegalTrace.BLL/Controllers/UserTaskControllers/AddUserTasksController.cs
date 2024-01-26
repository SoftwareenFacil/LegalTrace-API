using LegalTrace.BLL.Controllers.UserControllers;
using LegalTrace.BLL.Controllers.ClientControllers;
using LegalTrace.BLL.Models.UserTaskDTO;
using LegalTrace.DAL.Context;
using LegalTrace.DAL.Controllers.UserTaskControllers;
using LegalTrace.DAL.Models;
using LegalTrace.DAL.Controllers.UserControllers;
using LegalTrace.DAL.Controllers.ClientControllers;

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
            var userValidator = new UserGetById(_context);
            var user = await userValidator.GetUserById(userTask.UserId);
            if (user != null)
            {
                var clientValidator = new ClientGetById(_context);
                var client = await clientValidator.GetClientById(userTask.ClientId);
                if(client != null)
                {
                    if(userTask.DueDate > DateTime.Now && !string.IsNullOrEmpty(userTask.Title) && !string.IsNullOrEmpty(userTask.Description))
                    {

                        DateTime utcNow = DateTime.UtcNow;
                        var userTaskCreator = new UserTaskPost(_context);
                        var userTaskCreate = new UserTask()
                        {
                            UserId = userTask.UserId,
                            ClientId = userTask.ClientId,
                            Title = userTask.Title,
                            Description = userTask.Description,
                            Type = userTask.Type,
                            Repeatable = false,
                            Vigency = true,
                            Created = DateTime.SpecifyKind(utcNow, DateTimeKind.Utc),
                            Updated = DateTime.SpecifyKind(utcNow, DateTimeKind.Utc),
                            Finished = false,
                            DueDate = userTask.DueDate
                        };
                        return await userTaskCreator.InsertUserTask(userTaskCreate);
                    } 
                }
            }
            return 0;
        }
    }
}
