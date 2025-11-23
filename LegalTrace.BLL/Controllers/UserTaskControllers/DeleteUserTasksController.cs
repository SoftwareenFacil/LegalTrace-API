using LegalTrace.DAL.Context;
using LegalTrace.DAL.Repository;

namespace LegalTrace.BLL.Controllers.UserTaskControllers
{
    public class DeleteUserTasksController
    {
        private AppDbContext _context;
        public DeleteUserTasksController(AppDbContext _dbContext)
        {
            _context = _dbContext;
        }

        public async Task<bool> DeleteUserTaskById(int id)
        {
            var userTaskController = new UserTaskRepository(_context);
            var exist = await userTaskController.GetUserTaskById(id);
            if (exist == null)
            {
                return false;
            }
            return await userTaskController.DeleteUserTask(id);
        }
    }
}
