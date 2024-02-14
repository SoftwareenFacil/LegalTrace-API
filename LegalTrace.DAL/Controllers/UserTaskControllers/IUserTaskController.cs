using LegalTrace.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegalTrace.DAL.Controllers.UserTaskControllers
{
    public interface IUserTaskController
    {
        Task<UserTask> GetUserTaskById(int id);
        Task<List<UserTask>> GetUserTaskBy(int? id, int? userId, int? clientId, DateTime? dueDate, bool? repeatable, bool? vigency);
        Task<List<UserTask>> GetRepeatableUserTasks();
        Task<bool> DeleteUserTask(int id);
        Task<int> InsertUserTask(UserTask userTask);
        Task<bool> UpdateUserTask(UserTask userTask);
        Task<bool> CheckRepeatableUserTasks(List<UserTask> oldUserTasks, List<UserTask> newUserTasks);
    }
}
