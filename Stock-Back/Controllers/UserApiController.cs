﻿using Microsoft.AspNetCore.Mvc;
using Stock_Back.Controllers.UserApiControllers;
using Stock_Back.DAL.Context;
using Stock_Back.BLL.Models.DTO;

namespace Stock_Back.Controllers
{
    [SuperAdminRequired]
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UserApiController : ControllerBase
    {
        private AppDbContext _context;
        public UserApiController(AppDbContext dbContext)
        {
            _context = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers(int id)
        {
            var userGetter = new GetUsers(_context);
            return await userGetter.GetResponseUsers(id);
        }

        [HttpPost]
        public async Task<IActionResult> InsertUser([FromBody] UserInsertDTO user)
        {
            var add = new AddUser(_context);
            return await add.InsertUser(user);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody] UserEditDTO userEdited)
        {
            var updater = new UpdateUser(_context);
            return await updater.Update(userEdited);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var deleter = new DeleteUser(_context);
            return await deleter.Delete(id);
        }
    }
}
