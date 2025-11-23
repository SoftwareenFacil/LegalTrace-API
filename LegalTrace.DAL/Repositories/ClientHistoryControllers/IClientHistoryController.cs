using LegalTrace.DAL.Models;

namespace LegalTrace.DAL.Controllers.ClientHistoryControllers
{
    public interface IClientHistoryController
    {
        Task<ClientHistory?> GetClientHistoryById(int id);
        Task<List<ClientHistory>> GetClientHistoryBy(int id,int clientid, DateTime? createdFrom, DateTime? createdTo);
        Task<int> InsertClientHistory(ClientHistory history);
        Task<bool> UpdateClientHistory(ClientHistory updatedHistory);
        Task<bool> DeleteClientHistory(int id);
    }
}
