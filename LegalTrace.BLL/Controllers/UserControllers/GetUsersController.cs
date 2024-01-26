﻿using LegalTrace.DAL.Context;
using LegalTrace.BLL.Models.UserDTO;
using LegalTrace.DAL.Controllers.UserControllers;

namespace LegalTrace.BLL.Controllers.UserControllers
{
    public class GetUsersController
    {
        private AppDbContext _context;
        public GetUsersController(AppDbContext _dbContext)
        {
            _context = _dbContext;
        }

        public async Task<List<UserDTO>> GetUsersBy(int? id, string? name, string? email, DateTime? created, bool? vigency)
        {
            var userGetter = new UserGetBy(_context);
            var users = await userGetter.GetUserBy(id, name, email, created, vigency);
            if (users.Count() > 0)
            {
                List<UserDTO> result = new List<UserDTO>();
                users.ForEach(row => result.Add(new UserDTO()
                {
                    Id = row.Id,
                    Name = row.Name,
                    Email = row.Email,
                    Phone = row.Phone,
                    Address = row.Address,
                    SuperAdmin = row.SuperAdmin
                }));
                return result;
            }
            return new List<UserDTO>();
        }
    }
}
