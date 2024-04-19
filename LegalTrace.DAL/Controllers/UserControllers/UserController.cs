using LegalTrace.DAL.Context;
using LegalTrace.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace LegalTrace.DAL.Controllers.UserControllers
{
    public class UserController : IUserController
    {
        private AppDbContext _context;
        public UserController(AppDbContext _dbContext)
        {
            _context = _dbContext;
        }
        public async Task<User> GetUserById(int id)
        {
            var response = await _context.Users.Where(userAux => userAux.Id.Equals(id)).FirstOrDefaultAsync();
            return response;
        }
        public async Task<User> GetUserByEmail(string email)
        {
            var user = await _context.Users.Where(u => u.Email == email).FirstOrDefaultAsync();
            return user;
        }
        public async Task<List<User>> GetUserBy(int? id, string? name, string? email, DateTime? createdFrom, DateTime? createdTo, bool? vigency)
        {
            if (id.HasValue)
            {
                if (id.Value == 0)
                {
                    return await _context.Users.Take(100).ToListAsync();
                }
                else
                {
                    var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id.Value);
                    return user == null ? new List<User>() : new List<User> { user };
                }
            }

            var query = _context.Users.AsQueryable();

            if (!string.IsNullOrWhiteSpace(name))
                query = query.Where(u => EF.Functions.Like(u.Name, $"%{name}%"));

            if (!string.IsNullOrWhiteSpace(email))
                query = query.Where(u => EF.Functions.Like(u.Email, $"%{email}%"));

            if (createdFrom.HasValue)
                query = query.Where(u => u.Created >= DateTime.SpecifyKind(createdFrom.Value, DateTimeKind.Utc))
                             .OrderBy(u => u.Created);
            if (createdTo.HasValue)
                query = query.Where(u => u.Created <= DateTime.SpecifyKind(createdTo.Value, DateTimeKind.Utc))
             .OrderBy(u => u.Created);

            if (vigency.HasValue)
                query = query.Where(u => u.Vigency == vigency.Value);

            return await query.Take(100).ToListAsync();
        }
        public async Task<bool> DeleteUser(int id)
        {
            var user = await _context.Users.Where(userAux => userAux.Id.Equals(id)).FirstOrDefaultAsync();
            if (user != null)
            {
                _context.Users.Remove(user);
                if (await _context.SaveChangesAsync() > 0)
                {
                    return true;
                }
            }

            return false;
        }
        public async Task<int> InsertUser(User user)
        {
            await _context.Users.AddAsync(user);
            return await _context.SaveChangesAsync();
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
                response.Address = user.Address;
                response.Updated = user.Updated;

                if (await _context.SaveChangesAsync() > 0)
                    return true;

            }
            return false;
        }

    }
}
