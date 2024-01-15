using Microsoft.EntityFrameworkCore;
using LegalTrace.DAL.Context;
using LegalTrace.DAL.Models;

namespace LegalTrace.DAL.Controllers.UserControllers
{
    public class UserGetByEmail
    {
        private AppDbContext _context;
        public UserGetByEmail(AppDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<User> GetUserByEmail(string email)
        {
            var user = await _context.Users.Where(u => u.Email == email).FirstOrDefaultAsync();
            return user;
        }
    }
}
