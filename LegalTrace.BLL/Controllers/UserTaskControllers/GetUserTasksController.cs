using LegalTrace.BLL.Models.UserDTO;
using LegalTrace.BLL.Models.UserTaskDTO;
using LegalTrace.DAL.Context;
using LegalTrace.DAL.Controllers.UserControllers;
using LegalTrace.DAL.Controllers.UserTaskControllers;
using LegalTrace.DAL.Models;
using Microsoft.VisualBasic;


namespace LegalTrace.BLL.Controllers.UserTaskControllers
{
    public class GetUserTasksController
    {
        private AppDbContext _context;
        public GetUserTasksController(AppDbContext _dbContext)
        {
            _context = _dbContext;
        }

        public async Task<List<UserTaskDTO>> GetUserTaskBy(int? id, int? userId, int? clientId, DateTime? dueDate, bool? repeatable, bool? vigency)
        {
            var userTaskController = new UserTaskController(_context);
            var userTasks = await userTaskController.GetUserTaskBy(id, userId,clientId,dueDate,repeatable,vigency);
            if (userTasks.Count() > 0)
            {
                List<UserTaskDTO> result = new List<UserTaskDTO>();
                foreach (var userTask in userTasks)
                {
                    result.Add(new UserTaskDTO()
                    {
                        Id = userTask.Id,
                        ClientId = userTask.ClientId,
                        UserId = userTask.UserId,
                        Title = userTask.Title,
                        Description = userTask.Description,
                        Type = userTask.Type,
                        Finished = userTask.Finished,
                        DueDate = userTask.DueDate
                    });
                }
                
                return result;
            }
            return new List<UserTaskDTO>();
        }
    }
}
