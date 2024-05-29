using LegalTrace.BLL.Models.ClientHistoryDTO;
using LegalTrace.DAL.Context;
using LegalTrace.DAL.Controllers.ClientHistoryControllers;
using LegalTrace.DAL.Models;

namespace LegalTrace.BLL.Controllers.ClientHistoryControllers
{
    public class AddClientHistoryController
    {
        private AppDbContext _context;
        public AddClientHistoryController(AppDbContext _dbContext)
        {
            _context = _dbContext;
        }

        public async Task<int> AddClientHistory(ClientHistoryInsertDTO clientHistory)
        {
            if (clientHistory.EventDate <= DateTime.Now && !string.IsNullOrEmpty(clientHistory.Title) && !string.IsNullOrEmpty(clientHistory.Description))
            {

                DateTime utcNow = DateTime.UtcNow;
                var clientHistoryController = new ClientHistoryController(_context);
                var clientHistoryCreate = new ClientHistory()
                {
                    ClientId = clientHistory.ClientId,
                    Title = clientHistory.Title,
                    Description = clientHistory.Description,
                    EventDate = DateTime.SpecifyKind(clientHistory.EventDate, DateTimeKind.Utc),
                    Created = DateTime.SpecifyKind(utcNow, DateTimeKind.Utc),
                    Updated = DateTime.SpecifyKind(utcNow, DateTimeKind.Utc),
                    Vigency = true
                };
                return await clientHistoryController.InsertClientHistory(clientHistoryCreate);
            }

            return 0;
        }
    }
}
