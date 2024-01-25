using LegalTrace.DAL.Context;
using LegalTrace.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace LegalTrace.DAL.Controllers.UserControllers
{
    public class UserGetBy
    {
        private AppDbContext _context;
        public UserGetBy(AppDbContext dbContext)
        {
            _context = dbContext;
        }
        
        public async Task<List<User>> GetUserBy(int? id, string? name, string? email, DateTime? created, bool? vigency)
        {
            if (id.HasValue)
            {
                return new List<User> { await _context.Users.FirstOrDefaultAsync(u => u.Id == id.Value) };
            }

            var query = _context.Users.AsQueryable();

            if (!string.IsNullOrWhiteSpace(name))
            {
                query = query.Where(u => u.Name == name);
            }

            if (!string.IsNullOrWhiteSpace(email))
            {
                query = query.Where(u => u.Email == email);
            }

            if (created.HasValue)
            {
                query = query.Where(u => u.Created == created.Value);
            }

            if (vigency.HasValue)
            {
                query = query.Where(u => u.Vigency == vigency.Value);
            }

            return await query.ToListAsync();
        }
    }
}
