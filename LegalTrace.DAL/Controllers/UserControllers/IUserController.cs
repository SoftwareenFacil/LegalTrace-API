using LegalTrace.DAL.Models;

namespace LegalTrace.DAL.Controllers.UserControllers
{
    public interface IUserController
    {
        Task<User> GetUserById(int id);
        Task<User> GetUserByEmail(string email);
        Task<List<User>> GetUserBy(int? id, string? name, string? email, DateTime? createdFrom, DateTime? createdTo, bool? vigency);
        Task<bool> DeleteUser(int id);
        Task<int> InsertUser(User user);
        Task<bool> UpdateUser(User user);
    }
}
