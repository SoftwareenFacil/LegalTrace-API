using LegalTrace.DAL.Context;
using LegalTrace.DAL.Controllers.UserControllers;


namespace LegalTrace.BLL.Controllers.UserControllers
{
    public class DeleteUsersController
    {
        private AppDbContext _context;
        public DeleteUsersController(AppDbContext _dbContext)
        {
            _context = _dbContext;
        }

        public async Task<bool> DeleteUserById(int id)
        {
            var userController = new UserController(_context);
            var exist =  await userController.GetUserById(id);
            if (exist == null)
            {
                return false;
            }
            return await userController.DeleteUser(id);
        }
    }
}
