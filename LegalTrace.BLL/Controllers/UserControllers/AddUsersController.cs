﻿using Microsoft.AspNetCore.Http.HttpResults;
using LegalTrace.BLL.Models.UserDTO;
using LegalTrace.DAL.Context;
using LegalTrace.DAL.Controllers.UserControllers;
using LegalTrace.DAL.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Numerics;

namespace LegalTrace.BLL.Controllers.UserControllers
{
    public class AddUsersController
    {
        private AppDbContext _context;
        public AddUsersController(AppDbContext _dbContext)
        {
            _context = _dbContext;
        }

        public async Task<int> AddUser(UserInsertDTO user)
        {

            var userSample = new UserGetByEmail(_context);
            if (await userSample.GetUserByEmail(user.Email) != null)
                return -1;

            DateTime utcNow = DateTime.UtcNow;
            TimeZoneInfo chileTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Pacific SA Standard Time");
            DateTime chileTime = TimeZoneInfo.ConvertTimeFromUtc(utcNow, chileTimeZone);

            var userCreator = new UserPost(_context);
            var hasher = new Hasher();
            var userCreate = new User()
            {
                Name = user.Name,
                Email = user.Email,
                Password = hasher.HashPassword(user.Password),
                Phone = user.Phone,
                Created = DateTime.SpecifyKind(chileTime, DateTimeKind.Utc),
                Updated = DateTime.SpecifyKind(chileTime, DateTimeKind.Utc),
                Vigency = true
            };

            
            return await userCreator.InsertUser(userCreate);


        }
    }
}
