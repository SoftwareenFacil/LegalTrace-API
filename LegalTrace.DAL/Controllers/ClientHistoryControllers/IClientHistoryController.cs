using LegalTrace.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegalTrace.DAL.Controllers.ClientHistoryControllers
{
    public interface IClientHistoryController
    {
        Task<ClientHistory?> GetClientHistoryById(int id);
        Task<List<ClientHistory>> GetClientHistoryBy(int id);
        Task<int> InsertClientHistory(ClientHistory history);
        Task<bool> UpdateClientHistory(ClientHistory updatedHistory);
        Task<bool> DeleteClientHistory(int id);
    }
}
