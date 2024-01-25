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
                TimeZoneInfo chileTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Pacific SA Standard Time");
                DateTime chileTime = TimeZoneInfo.ConvertTimeFromUtc(userTask.DueDate, chileTimeZone);
                return new UserTaskDTO()
                {
                    Id = userTask.Id,
                    ClientId = userTask.ClientId,
                    UserId = userTask.UserId,
                    Title = userTask.Title,
                    Description = userTask.Description,
                    Type = userTask.Type,
                    Finished = userTask.Finished,
                    DueDate = DateTime.SpecifyKind(chileTime, DateTimeKind.Utc)
                };
            }
        }

        public async Task<List<UserTaskDTO>> GetUserTaskByUserId(int userId)
        {
            var userTaskGetter = new UserTaskGetByUserId(_context);
            var userTasks = await userTaskGetter.GetUserTaskByUserId(userId);
            if (userTasks.Count() > 0)
            {
                List<UserTaskDTO> result = new List<UserTaskDTO>();
                foreach (UserTask row in userTasks)
                {
                    TimeZoneInfo chileTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Pacific SA Standard Time");
                    DateTime chileTime = TimeZoneInfo.ConvertTimeFromUtc(row.DueDate, chileTimeZone);
                    result.Add(new UserTaskDTO()
                    {
                        Id = row.Id,
                        ClientId = row.ClientId,
                        UserId = row.UserId,
                        Title = row.Title,
                        Description = row.Description,
                        Type = row.Type,
                        Finished = row.Finished,
                        DueDate = DateTime.SpecifyKind(chileTime, DateTimeKind.Utc)
                    });
                }
                return result;
            }
            else
            {
                return new List<UserTaskDTO>();
            }
        }

        public async Task<List<UserTaskDTO>> GetUserTaskByClientId(int clientId)
        {
            var userTaskGetter = new UserTaskGetByClientId(_context);
            var userTasks = await userTaskGetter.GetUserTaskByClientId(clientId);
            if (userTasks.Count() > 0)
            {
                List<UserTaskDTO> result = new List<UserTaskDTO>();
                foreach (UserTask row in userTasks)
                {
                    TimeZoneInfo chileTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Pacific SA Standard Time");
                    DateTime chileTime = TimeZoneInfo.ConvertTimeFromUtc(row.DueDate, chileTimeZone);
                    result.Add(new UserTaskDTO()
                    {
                        Id = row.Id,
                        ClientId = row.ClientId,
                        UserId = row.UserId,
                        Title = row.Title,
                        Description = row.Description,
                        Type = row.Type,
                        Finished = row.Finished,
                        DueDate = DateTime.SpecifyKind(chileTime, DateTimeKind.Utc)
                    });
                }
                return result;
            }
            else
            {
                return new List<UserTaskDTO>();
            }
        }

        public async Task<List<UserTaskDTO>> GetUserTaskBy(int? userId, int? clientId, DateTime? dueDate)
        {
            var userTaskGetter = new UserTaskGetBy(_context);
            var userTasks = await userTaskGetter.GetUserTaskBy(userId,clientId,dueDate);
            if (userTasks.Count() > 0)
            {
                TimeZoneInfo chileTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Pacific SA Standard Time");
                List<UserTaskDTO> result = new List<UserTaskDTO>();
                foreach (var userTask in userTasks)
                {
                    DateTime chileTime = TimeZoneInfo.ConvertTimeFromUtc(userTask.DueDate, chileTimeZone);
                    result.Add(new UserTaskDTO()
                    {
                        Id = userTask.Id,
                        ClientId = userTask.ClientId,
                        UserId = userTask.UserId,
                        Title = userTask.Title,
                        Description = userTask.Description,
                        Type = userTask.Type,
                        Finished = userTask.Finished,
                        DueDate = DateTime.SpecifyKind(chileTime, DateTimeKind.Utc)
                    });
                }
                
                return result;
            }
            return new List<UserTaskDTO>();
        }

        public async Task<List<UserTaskDTO>> GetUserTaskByDueDate(DateTime dueDate)
        {
            var userTaskGetter = new UserTaskGetByDueDate(_context);
            var userTasks = await userTaskGetter.GetUserTaskByDueDate(dueDate);
            if (userTasks.Count() > 0)
            {
                List<UserTaskDTO> result = new List<UserTaskDTO>();
                foreach (UserTask row in userTasks)
                {
                    TimeZoneInfo chileTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Pacific SA Standard Time");
                    DateTime chileTime = TimeZoneInfo.ConvertTimeFromUtc(row.DueDate, chileTimeZone);
                    result.Add(new UserTaskDTO()
                    {
                        Id = row.Id,
                        ClientId = row.ClientId,
                        UserId = row.UserId,
                        Title = row.Title,
                        Description = row.Description,
                        Type = row.Type,
                        Finished = row.Finished,
                        DueDate = DateTime.SpecifyKind(chileTime, DateTimeKind.Utc)
                    });
                }
                return result;
            }
            else
            {
                return new List<UserTaskDTO>();
            }
        }

        public async Task<List<UserTaskDTO>?> GetAllUserTasks()
        {
            var userTasksGetter = new UserTaskGetAll(_context);
            var userTasks = await userTasksGetter.GetAllUserTasks();

            if (userTasks.Count() > 0)
            {
                List<UserTaskDTO> result = new List<UserTaskDTO>();
                foreach(UserTask row in userTasks)
                {
                    TimeZoneInfo chileTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Pacific SA Standard Time");
                    DateTime chileTime = TimeZoneInfo.ConvertTimeFromUtc(row.DueDate, chileTimeZone);
                    result.Add(new UserTaskDTO()
                    {
                        Id = row.Id,
                        ClientId = row.ClientId,
                        UserId = row.UserId,
                        Title = row.Title,
                        Description = row.Description,
                        Type = row.Type,
                        Finished = row.Finished,
                        DueDate = DateTime.SpecifyKind(chileTime, DateTimeKind.Utc)
                    });
                }
                return result;
            }
            else
            {
                return null;
            }

        }
    }
}
