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
    public class UserTaskController : IUserTaskController
    {
        private AppDbContext _context;
        public UserTaskController(AppDbContext _dbContext)
        {
            _context = _dbContext;
        }
        public async Task<UserTask> GetUserTaskById(int id)
        {
            var response = await _context.UserTasks.Where(userTaskAux => userTaskAux.Id.Equals(id)).FirstOrDefaultAsync();
            return response;
        }
        public async Task<List<UserTask>> GetUserTaskBy(int? id, int? userId, int? clientId, DateTime? dueDate, bool? repeatable, bool? vigency)
        {
            if (id.HasValue)
            {
                if (id.Value == 0)
                {
                    return await _context.UserTasks.Take(100).ToListAsync();
                }
                else
                {
                    var userTask = await _context.UserTasks.FirstOrDefaultAsync(u => u.Id == id.Value);
                    return userTask == null ? new List<UserTask>() : new List<UserTask> { userTask };
                }
            }

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

            if (repeatable.HasValue)
            {
                query = query.Where(userTask => userTask.Repeatable == repeatable.Value);
            }

            if (vigency.HasValue)
            {
                query = query.Where(userTask => userTask.Vigency == vigency.Value);
            }

            var userTasks = await query.ToListAsync();
            return userTasks;
        }
        public async Task<List<UserTask>> GetRepeatableUserTasks()
        {
            var repeatableTasks = await _context.UserTasks
                                    .Where(userTask => userTask.Vigency == true && userTask.Repeatable == true)
                                    .ToListAsync();
            return repeatableTasks;
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
        public async Task<int> InsertUserTask(UserTask userTask)
        {
            await _context.UserTasks.AddAsync(userTask);
            return await _context.SaveChangesAsync();
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
                response.Type = userTask.Type;
                response.Vigency = userTask.Vigency;
                response.Finished = userTask.Finished;
                response.FinishedDate = userTask.FinishedDate;
                response.Updated = userTask.Updated;

                if (await _context.SaveChangesAsync() > 0)
                    return true;
            }
            return false;
        }

        public async Task<bool> CheckRepeatableUserTasks(List<UserTask> oldUserTasks, List<UserTask> newUserTasks)
        {
            foreach (var oldTask in oldUserTasks)
            {
                var taskToUpdate = await _context.UserTasks.FirstOrDefaultAsync(t => t.Id == oldTask.Id);
                if (taskToUpdate != null)
                {
                    taskToUpdate.UserId = oldTask.UserId;
                    taskToUpdate.ClientId = oldTask.ClientId;
                    taskToUpdate.Title = oldTask.Title;
                    taskToUpdate.Description = oldTask.Description;
                    taskToUpdate.Repeatable = oldTask.Repeatable;
                    taskToUpdate.Type = oldTask.Type;
                    taskToUpdate.Vigency = oldTask.Vigency;
                    taskToUpdate.Finished = oldTask.Finished;
                    taskToUpdate.FinishedDate = oldTask.FinishedDate;
                    taskToUpdate.DueDate = oldTask.DueDate;
                    taskToUpdate.Updated = oldTask.Updated; 
                                                        
                }
            }

            foreach (var newUserTask in newUserTasks)
            {
                await _context.UserTasks.AddAsync(newUserTask);
            }

            var saved = await _context.SaveChangesAsync();
            return saved > 0;
        }
    }
}
