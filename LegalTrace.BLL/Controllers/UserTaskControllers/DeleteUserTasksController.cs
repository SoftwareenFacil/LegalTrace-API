using LegalTrace.DAL.Context;
using LegalTrace.DAL.Controllers.UserTaskControllers;

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
            var userTaskVerify = new UserTaskGetById(_context);
            var exist = await userTaskVerify.GetUserTaskById(id);
            if (exist == null)
            {
                return false;
            }

            var userTaskDeleter = new UserTaskDelete(_context);
            return await userTaskDeleter.DeleteUserTask(id);
        }
    }
}
