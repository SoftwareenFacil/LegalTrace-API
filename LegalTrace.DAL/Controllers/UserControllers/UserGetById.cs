using Microsoft.EntityFrameworkCore;
using LegalTrace.DAL.Context;
using LegalTrace.DAL.Models;

namespace LegalTrace.DAL.Controllers.UserControllers
{
    public class UserGetById
    {
        private AppDbContext _context;
        public UserGetById(AppDbContext _dbContext)
        {
            _context = _dbContext;
        }

        public async Task<User> GetUserById(int id)
        {
            var response = await _context.Users.Where(userAux => userAux.Id.Equals(id)).FirstOrDefaultAsync();
            return response;
        }
    }

}
