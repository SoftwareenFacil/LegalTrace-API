using LegalTrace.DAL.Context;
using Microsoft.EntityFrameworkCore;

namespace LegalTrace.DAL.Controllers.UserTaskControllers
{
    public class UserTaskDelete
    {
        private AppDbContext _context;
        public UserTaskDelete(AppDbContext _dbContext)
        {
            _context = _dbContext;
        }

        public async Task<bool> DeleteUserTask(int id)
        {
            var userTask = await _context.UserTasks.Where(userAux => userAux.Id.Equals(id)).FirstOrDefaultAsync();
            if (userTask != null)
            {
                _context.UserTasks.Remove(userTask);
                if (await _context.SaveChangesAsync() > 0)
                {
                    return true;
                }
            }
            return false;
        }


    }
}
