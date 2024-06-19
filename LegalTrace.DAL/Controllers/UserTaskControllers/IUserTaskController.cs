using LegalTrace.DAL.Models;

namespace LegalTrace.DAL.Controllers.UserTaskControllers
{
    public interface IUserTaskController
    {
        Task<UserTask> GetUserTaskById(int id);
        Task<List<UserTask>> GetUserTaskBy(int? id, string? title, int? userId, int? clientId, DateTime? dueDate, bool? repeatable, bool? vigency, bool? finished, DateTime? createdFrom, DateTime? createdTo);
        Task<List<UserTask>> GetRepeatableUserTasks();
        Task<bool> DeleteUserTask(int id);
        Task<int> InsertUserTask(UserTask userTask);
        Task<bool> UpdateUserTask(UserTask userTask);
        Task<bool> CheckRepeatableUserTasks(List<UserTask> oldUserTasks, List<UserTask> newUserTasks);
    }
}
