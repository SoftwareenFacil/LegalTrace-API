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
    public class UserTaskGetByClientId
    {
        private AppDbContext _context;
        public UserTaskGetByClientId(AppDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<List<UserTask>> GetUserTaskByClientId(int clientId)
        {
            var userTasks = await _context.UserTasks
                                          .Where(userTask => userTask.ClientId == clientId)
                                          .ToListAsync();
            return userTasks;
        }
    }
}
