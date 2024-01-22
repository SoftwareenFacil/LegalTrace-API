using Microsoft.EntityFrameworkCore;
using LegalTrace.DAL.Context;
using LegalTrace.DAL.Models;

namespace LegalTrace.DAL.Controllers.UserControllers
{
    public class UserUpdate
    {
        private AppDbContext _context;
        public UserUpdate(AppDbContext _dbContext)
        {
            _context = _dbContext;
        }

        public async Task<bool> UpdateUser(User user)
        {
            var response = await _context.Users.Where(userAux => userAux.Id.Equals(user.Id)).FirstOrDefaultAsync();
            if (response != null)
            {
                response.Name = user.Name;
                response.Email = user.Email;
                response.Password = user.Password;
                response.Phone = user.Phone;
                response.Updated = user.Updated;

                if (await _context.SaveChangesAsync() > 0)
                    return true;

            }
            return false;
        }
    }
}
