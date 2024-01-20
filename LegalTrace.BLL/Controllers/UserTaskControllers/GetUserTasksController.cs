using LegalTrace.BLL.Models.UserTaskDTO;
using LegalTrace.DAL.Context;
using LegalTrace.DAL.Controllers.UserTaskControllers;
using LegalTrace.DAL.Models;


namespace LegalTrace.BLL.Controllers.UserTaskControllers
{
    public class GetUserTasksController
    {
        private AppDbContext _context;
        public GetUserTasksController(AppDbContext _dbContext)
        {
            _context = _dbContext;
        }

        public async Task<dynamic?> GetUserTasks(int id)
        {
            if (id == 0)
                return await GetAllUserTasks();
            return await GetUserTaskById(id);
        }

        public async Task<UserTaskDTO?> GetUserTaskById(int id)
        {
            var userTaskGetter = new UserTaskGetById(_context);
            var userTask = await userTaskGetter.GetUserTaskById(id);
            if (userTask == null)
            {
                return null;
            }
            else
            {
                return new UserTaskDTO()
                {
                    Id = userTask.Id,
                    ClientId = userTask.ClientId,
                    UserId = userTask.UserId,
                    Title = userTask.Title,
                    Description = userTask.Description,
                    Type = userTask.Type,
                    Finished = userTask.Finished,
                    DueDate = userTask.DueDate
                };
            }
        }

        public async Task<List<UserTaskDTO>?> GetAllUserTasks()
        {
            var userTasksGetter = new UserTaskGetAll(_context);
            var userTasks = await userTasksGetter.GetAllUserTasks();

            if (userTasks.Count() > 0)
            {
                List<UserTaskDTO> result = new List<UserTaskDTO>();
                userTasks.ForEach(row => result.Add(new UserTaskDTO()
                {
                    Id = row.Id,
                    ClientId = row.ClientId,
                    UserId = row.UserId,
                    Title = row.Title,
                    Description = row.Description,
                    Type= row.Type,
                    Finished = row.Finished,
                    DueDate = row.DueDate
                }));
                return result;
            }
            else
            {
                return null;
            }

        }
    }
}
