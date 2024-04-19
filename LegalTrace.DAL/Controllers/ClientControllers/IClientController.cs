using LegalTrace.DAL.Models;

namespace LegalTrace.DAL.Controllers.ClientControllers
{
    public interface IClientController
    {
        Task<Client> GetClientById(int id);
        Task<Client?> GetClientByEmail(string email);
        Task<List<Client>> GetClientBy(int? id, string? name, string? email, string? taxId, DateTime? createdFrom, DateTime? createdTo, bool? vigency);
        Task<bool> DeleteClient(int id);
        Task<int> InsertClient(Client client);
        Task<bool> UpdateClient(Client client);
    }
}
