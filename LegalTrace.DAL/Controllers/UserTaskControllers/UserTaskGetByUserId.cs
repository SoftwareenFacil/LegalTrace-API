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
    public class UserTaskGetByUserId
    {
        private AppDbContext _context;
        public UserTaskGetByUserId(AppDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<List<UserTask>> GetUserTaskByUserId(int userId)
        {
            var userTasks = await _context.UserTasks
                                          .Where(userTask => userTask.UserId == userId)
                                          .ToListAsync();
            return userTasks;
        }
    }
}
