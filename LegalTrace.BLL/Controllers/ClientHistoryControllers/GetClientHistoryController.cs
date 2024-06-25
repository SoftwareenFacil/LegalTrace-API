using LegalTrace.DAL.Context;
using LegalTrace.DAL.Controllers.ClientHistoryControllers;
using LegalTrace.PDF.Models;
using System.Data;

namespace LegalTrace.BLL.Controllers.ClientHistoryControllers
{
    public class GetClientHistoryController
    {
        private AppDbContext _context;
        public GetClientHistoryController(AppDbContext _dbContext)
        {
            _context = _dbContext;
        }

        public async Task<List<ClientHistoryDTO>> GetClientHistory(int id, int clientid, DateTime? createdFrom, DateTime? createdTo)
        {
            var clientHistoryController = new ClientHistoryController(_context);
            var clientHistories = await clientHistoryController.GetClientHistoryBy(id, clientid, createdFrom, createdTo);
            if (clientHistories.Count() > 0)
            {
                List<ClientHistoryDTO> result = new List<ClientHistoryDTO>();
                clientHistories.ForEach(row => result.Add(new ClientHistoryDTO()
                {
                    Id = row.Id,
                    ClientId = row.ClientId,
                    Title = row.Title,
                    Description = row.Description,
                    EventDate = row.EventDate,
                    Created = row.Created,
                    Vigency = row.Vigency
                }));
                return result;
            }

            return new List<ClientHistoryDTO>();
        }
    }
}
