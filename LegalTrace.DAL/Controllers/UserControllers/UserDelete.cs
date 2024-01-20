﻿using Microsoft.EntityFrameworkCore;
using LegalTrace.DAL.Context;

namespace LegalTrace.DAL.Controllers.UserControllers
{
    public class UserDelete
    {
        private AppDbContext _context;
        public UserDelete(AppDbContext _dbContext)
        {
            _context = _dbContext;
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
    }
}
