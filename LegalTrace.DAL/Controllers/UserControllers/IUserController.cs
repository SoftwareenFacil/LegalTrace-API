using LegalTrace.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegalTrace.DAL.Controllers.UserControllers
{
    public interface IUserController
    {
        Task<User> GetUserById(int id);
        Task<User> GetUserByEmail(string email);
        Task<List<User>> GetUserBy(int? id, string? name, string? email, DateTime? created, bool? vigency);
        Task<bool> DeleteUser(int id);
        Task<int> InsertUser(User user);
        Task<bool> UpdateUser(User user);
    }
}
