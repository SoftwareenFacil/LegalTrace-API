using LegalTrace.DAL.Context;
using LegalTrace.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace LegalTrace.DAL.Controllers.UserTaskControllers
{
    public class UserTaskGetByDueDate
    {
        private AppDbContext _context;
        public UserTaskGetByDueDate(AppDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<List<UserTask>> GetUserTaskByDueDate(DateTime dueDate)
        {
            var userTasks = await _context.UserTasks
                                          .Where(userTask => userTask.DueDate.Date == dueDate.Date)
                                          .ToListAsync();
            return userTasks;
        }
    }
}
