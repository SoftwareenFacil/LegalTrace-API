using LegalTrace.DAL.Context;
using LegalTrace.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace LegalTrace.DAL.Controllers.UserTaskControllers
{
    public class UserTaskGetBy
    {
        private AppDbContext _context;
        public UserTaskGetBy(AppDbContext dbContext)
        {
            _context = dbContext;
        }
        public async Task<List<UserTask>> GetUserTaskBy(int? id, int? userId, int? clientId, DateTime? dueDate, bool? repeatable, bool? vigency)
        {
            if (id.HasValue)
            {
                if (id.Value == 0)
                {
                    return await _context.UserTasks.Take(100).ToListAsync();
                }
                else
                {
                    var userTask = await _context.UserTasks.FirstOrDefaultAsync(u => u.Id == id.Value);
                    return userTask == null ? new List<UserTask>() : new List<UserTask> { userTask };
                }
            }

            var query = _context.UserTasks.AsQueryable();

            if (userId.HasValue)
            {
                query = query.Where(userTask => userTask.UserId == userId.Value);
            }

            if (clientId.HasValue)
            {
                query = query.Where(userTask => userTask.ClientId == clientId.Value);
            }

            if (dueDate.HasValue)
            {
                var dueDateOnly = dueDate.Value.Date;
                query = query.Where(userTask => userTask.DueDate.Date == dueDateOnly);
            }

            if (repeatable.HasValue)
            {
                query = query.Where(userTask => userTask.Repeatable == repeatable.Value);
            }

            if (vigency.HasValue)
            {
                query = query.Where(userTask => userTask.Vigency == vigency.Value);
            }

            var userTasks = await query.ToListAsync();
            return userTasks;
        }
    }
}
