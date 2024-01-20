using LegalTrace.DAL.Context;
using LegalTrace.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace LegalTrace.DAL.Controllers.UserTaskControllers
{
    public class UserTaskGetAll
    {
        private AppDbContext _context;
        public UserTaskGetAll(AppDbContext _dbContext)
        {
            _context = _dbContext;
        }

        public async Task<List<UserTask>> GetAllUserTasks()
        {
            var userTasks = await _context.UserTasks
                                          .OrderByDescending(ut => ut.Updated) 
                                          .Take(100)
                                          .ToListAsync();
            return userTasks;
        }
    }
}
