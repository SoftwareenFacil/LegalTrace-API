using LegalTrace.BLL.Models.ClientHistoryDTO;
using LegalTrace.DAL.Context;
using LegalTrace.DAL.Controllers.ClientHistoryControllers;

namespace LegalTrace.BLL.Controllers.ClientHistoryControllers
{
    public class GetClientHistoryController
    {
        private AppDbContext _context;
        public GetClientHistoryController(AppDbContext _dbContext)
        {
            _context = _dbContext;
        }

        public async Task<List<ClientHistoryDTO>> GetClientHistoryById(int id)
        {
            var clientHistoryController = new ClientHistoryController(_context);
            var clientHistories = await clientHistoryController.GetClientHistoryBy(id);
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
                    Vigency = row.Vigency
                }));
                return result;
            }

            return new List<ClientHistoryDTO>();
        }
    }
}
