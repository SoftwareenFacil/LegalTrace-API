using Microsoft.AspNetCore.Mvc;
using LegalTrace.BLL.Models;
using LegalTrace.BLL.Controllers.LoginControllers;
using LegalTrace.UserJwt;
using LegalTrace.BLL.Controllers.JwtControllers;
using LegalTrace.DAL.Context;

namespace LegalTrace.Controllers
{
    public class AuthUser : ControllerBase
    {
        private AppDbContext _context;
        private IManejoJwt _manejoJwt;
        public AuthUser(IManejoJwt manejoJwt, AppDbContext context)
        {
            _context = context;
            _manejoJwt = manejoJwt;
        }

        public async Task<IActionResult> Authenticate(UserCredentials credentials)
        {
            ResponseType type = ResponseType.Failure;
            if (credentials == null || string.IsNullOrWhiteSpace(credentials.Email) || string.IsNullOrWhiteSpace(credentials.Password))
            {
                return BadRequest(ResponseHandler.GetAppResponse(type, "Invalid Credentials."));
            }
            var loger = new AuthController(_context);
            var token = await loger.Authenticate(_manejoJwt, credentials);
            if (token != null)
            {
                type = ResponseType.Success;
                return Ok(ResponseHandler.GetAppResponse(type, token));

            }
            return Unauthorized(ResponseHandler.GetAppResponse(type, "Unauthorized Credentials"));
        }
    }
}
