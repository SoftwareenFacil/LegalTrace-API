using LegalTrace.DAL.Context;
using LegalTrace.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace LegalTrace.DAL.Controllers.UserTaskControllers
{
    public class UserTaskGetById
    {
        private AppDbContext _context;
        public UserTaskGetById(AppDbContext _dbContext)
        {
            _context = _dbContext;
        }
        public async Task<UserTask> GetUserTaskById(int id)
        {
            var response = await _context.UserTasks.Where(userTaskAux => userTaskAux.Id.Equals(id)).FirstOrDefaultAsync();
            return response;
        }
    }
}
