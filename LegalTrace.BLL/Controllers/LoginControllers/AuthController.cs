using LegalTrace.BLL.Models;
using LegalTrace.DAL.Context;
using LegalTrace.DAL.Controllers.UserControllers;
using LegalTrace.BLL.Controllers.JwtControllers;

namespace LegalTrace.BLL.Controllers.LoginControllers
{
    public class AuthController
    {
        private AppDbContext _context;
        public AuthController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<string?> Authenticate(IManejoJwt manejoJwt,UserCredentials credentials)
        {
            var userController = new UserController(_context);
            var user = await userController.GetUserByEmail(credentials.Email);
            var hasher = new Hasher();
            if (user != null && hasher.VerifyPassword(credentials.Password, user.Password))
            {
                var token = manejoJwt.GenerarToken(user.Name, user.Email, user.SuperAdmin);
                return token;
            }

            return null;
        }
    }
}