using Microsoft.AspNetCore.Mvc;
using LegalTrace.DAL.Context;
using LegalTrace.BLL.Models.UserDTO;

namespace LegalTrace.Controllers.UserApiControllers
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
        public async Task<IActionResult> GetUsers(int? id, string? name, string? email, DateTime? created, bool? vigency)
        {
            var userGetter = new GetUsers(_context);
            return await userGetter.GetBy(id,name,email,created,vigency);
        }

        [HttpPost]
        public async Task<IActionResult> InsertUser([FromBody] UserInsertDTO user)
        {
            var add = new InsertUser(_context);
            return await add.Insert(user);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody] UserEditDTO userEdited)
        {
            var updater = new UpdateUser(_context);
            return await updater.Update(userEdited);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUserVigency(int id)
        {
            var updater = new UpdateUser(_context);
            return await updater.UpdateVigency(id);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var deleter = new DeleteUser(_context);
            return await deleter.Delete(id);
        }
    }
}
