using LegalTrace.DAL.Context;
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

        public async Task<List<UserDTO>> GetUsersBy(int? id, string? name, string? email, DateTime? createdFrom, DateTime? createdTo, bool? vigency)
        {
            var userController = new UserController(_context);
            var users = await userController.GetUserBy(id, name, email, createdFrom, createdTo, vigency);
            if (users.Count() > 0)
            {
                List<UserDTO> result = new List<UserDTO>();
                foreach (var row in users)
                {
                    
                    result.Add(new UserDTO()
                    {
                        Id = row.Id,
                        Name = row.Name,
                        Email = row.Email,
                        Phone = row.Phone,
                        Address = row.Address,
                        SuperAdmin = row.SuperAdmin,
                        Created = row.Created,
                        Vigency = row.Vigency
                    });
                }

                return result;
            }
            return new List<UserDTO>();
        }
    }
}
