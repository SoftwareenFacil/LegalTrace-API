using Microsoft.EntityFrameworkCore;
using LegalTrace.DAL.Context;
using LegalTrace.DAL.Models;

namespace LegalTrace.DAL.Controllers.UserControllers
{
    public class UserGetAll
    {
        private AppDbContext _context;
        public UserGetAll(AppDbContext _dbContext)
        {
            _context = _dbContext;
        }

        public async Task<List<User>> GetAllUsers()
        {
            var users = await _context.Users.Take(100).ToListAsync();
            return users;
        }

    }
}
