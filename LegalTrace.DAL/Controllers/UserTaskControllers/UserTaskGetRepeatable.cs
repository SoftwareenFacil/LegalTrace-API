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
    public class UserTaskGetRepeatable
    {
        private AppDbContext _context;
        public UserTaskGetRepeatable(AppDbContext _dbContext)
        {
            _context = _dbContext;
        }
        public async Task<List<UserTask>> GetRepeatableUserTasks()
        {
            var repeatableTasks = await _context.UserTasks
                                    .Where(userTask => userTask.Vigency == true && userTask.Repeatable == true)
                                    .ToListAsync();
            return repeatableTasks;
        }
    }
}
