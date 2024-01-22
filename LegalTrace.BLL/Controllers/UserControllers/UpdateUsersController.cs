using LegalTrace.DAL.Context;
using LegalTrace.BLL.Models;
using LegalTrace.BLL.Models.UserDTO;
using LegalTrace.DAL.Controllers.UserControllers;
using LegalTrace.DAL.Models;

namespace LegalTrace.BLL.Controllers.UserControllers
{
    public class UpdateUsersController
    {
        private AppDbContext _context;
        public UpdateUsersController(AppDbContext _dbContext)
        {
            _context = _dbContext;
        }
        public async Task<(bool, bool)> UpdateUser(UserEditDTO userEdited)
        {
            bool isUpdated = false;
            bool isUser = false;
            if (string.IsNullOrWhiteSpace(userEdited.Name) && string.IsNullOrWhiteSpace(userEdited.Email) && string.IsNullOrWhiteSpace(userEdited.Password) && userEdited.Phone == 0)
                return (isUpdated, isUser);

            var userVerify = new UserGetById(_context);
            var userUpdater = new UserUpdate(_context);
            var user = await userVerify.GetUserById(userEdited.Id);
            if (user != null)
            {
                isUser = true;
                user.Name = !string.IsNullOrEmpty(userEdited.Name) ? userEdited.Name : user.Name;
                user.Email = !string.IsNullOrEmpty(userEdited.Email) ? userEdited.Email : user.Email;
                user.Password = CheckifNewPassword(userEdited.Password, user.Password);
                user.Phone = userEdited.Phone > 0 ? userEdited.Phone : user.Phone;
                DateTime utcNow = DateTime.UtcNow;
                user.Updated = DateTime.SpecifyKind(utcNow, DateTimeKind.Utc);
                isUpdated = await userUpdater.UpdateUser(user);

                return (isUpdated, isUser);
            }
            return (isUpdated, isUser);
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
