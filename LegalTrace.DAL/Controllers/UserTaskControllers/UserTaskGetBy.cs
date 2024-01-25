using LegalTrace.DAL.Context;
using LegalTrace.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegalTrace.DAL.Controllers.UserTaskControllers
{
    public class UserTaskGetBy
    {
        private AppDbContext _context;
        public UserTaskGetBy(AppDbContext dbContext)
        {
            _context = dbContext;
        }
        public async Task<List<UserTask>> GetUserTaskBy(int? userId, int? clientId, DateTime? dueDate)
        {
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

            var userTasks = await query.ToListAsync();
            return userTasks;
        }
    }
}
