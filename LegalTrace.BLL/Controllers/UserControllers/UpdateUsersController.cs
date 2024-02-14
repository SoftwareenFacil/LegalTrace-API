using LegalTrace.DAL.Context;
using LegalTrace.BLL.Models.UserDTO;
using LegalTrace.DAL.Controllers.UserControllers;

namespace LegalTrace.BLL.Controllers.UserControllers
{
    public class UpdateUsersController
    {
        private AppDbContext _context;
        public UpdateUsersController(AppDbContext _dbContext)
        {
            _context = _dbContext;
        }
        public async Task<int> UpdateUser(UserEditDTO userEdited)
        {
            if (string.IsNullOrWhiteSpace(userEdited.Name) && string.IsNullOrWhiteSpace(userEdited.Email) && string.IsNullOrWhiteSpace(userEdited.Password) && string.IsNullOrWhiteSpace(userEdited.Address) &&  userEdited.Phone == 0)
                return 500;

            var userController = new UserController(_context);
            var user = await userController.GetUserById(userEdited.Id);
            if (user != null)
            {
                user.Name = !string.IsNullOrEmpty(userEdited.Name) ? userEdited.Name : user.Name;
                user.Email = !string.IsNullOrEmpty(userEdited.Email) ? userEdited.Email : user.Email;
                user.Address = !string.IsNullOrEmpty(userEdited.Address) ? userEdited.Address : user.Address;
                user.Password = CheckifNewPassword(userEdited.Password, user.Password);
                user.Phone = userEdited.Phone > 0 ? userEdited.Phone : user.Phone;
                DateTime utcNow = DateTime.UtcNow;
                user.Updated = DateTime.SpecifyKind(utcNow, DateTimeKind.Utc);
                var isUpdated = await userController.UpdateUser(user);
                if (!isUpdated)
                    return 500;
                return 200;
            }
            return 404;
        }

        private string CheckifNewPassword(string password, string userpassword)
        {
            if (!string.IsNullOrEmpty(password))
            {
                var hasher = new Hasher();
                return hasher.HashPassword(password);
            }
            return userpassword;

        }
    }
}
