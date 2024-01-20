using LegalTrace.DAL.Context;
using LegalTrace.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace LegalTrace.DAL.Controllers.UserTaskControllers
{
    public class UserTaskUpdate
    {
        private AppDbContext _context;
        public UserTaskUpdate(AppDbContext _dbContext)
        {
            _context = _dbContext;
        }
        public async Task<bool> UpdateUserTask(UserTask userTask)
        {
            var response = await _context.UserTasks.Where(userTaskAux => userTaskAux.Id.Equals(userTask.Id)).FirstOrDefaultAsync();
            if (response != null)
            {
                response.UserId = userTask.UserId;
                response.ClientId = userTask.ClientId;
                response.Title = userTask.Title;
                response.Description = userTask.Description;
                response.Repeatable = userTask.Repeatable;
                response.Vigency = userTask.Vigency;
                response.Finished = userTask.Finished;
                response.FinishedDate = userTask.FinishedDate;
                DateTime utcNow = DateTime.UtcNow;
                TimeZoneInfo chileTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Pacific SA Standard Time");
                DateTime chileTime = TimeZoneInfo.ConvertTimeFromUtc(utcNow, chileTimeZone);
                response.Updated = DateTime.SpecifyKind(chileTime, DateTimeKind.Utc);

                if (await _context.SaveChangesAsync() > 0)
                    return true;

            }
            return false;
        }
    }
}
