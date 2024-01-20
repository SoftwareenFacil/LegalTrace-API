using LegalTrace.DAL.Context;
using LegalTrace.DAL.Models;

namespace LegalTrace.DAL.Controllers.UserTaskControllers
{
    public class UserTaskPost
    {
        private AppDbContext _context;
        public UserTaskPost(AppDbContext _dbContext)
        {
            _context = _dbContext;
        }
        public async Task<int> InsertUserTask(UserTask userTask)
        {
            await _context.UserTasks.AddAsync(userTask);
            return await _context.SaveChangesAsync();
        }
    }
}
