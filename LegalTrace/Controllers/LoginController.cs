using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using LegalTrace.BLL.Models;
using LegalTrace.BLL.Controllers.JwtControllers;
using LegalTrace.DAL.Context;

namespace LegalTrace.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    
    public class LoginController : ControllerBase
    {
        private IManejoJwt manejoJwt;
        private readonly AppDbContext _context;

        public LoginController(IManejoJwt manejoJwt, AppDbContext dbContext)
        {
            this.manejoJwt = manejoJwt;
            _context = dbContext;
        }

        [AllowAnonymous]
        [HttpPost("Authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] UserCredentials credentials)
        {
            var userAuthenticator = new AuthUser(manejoJwt, _context);
            return await userAuthenticator.Authenticate(credentials);
        }
    }
}
