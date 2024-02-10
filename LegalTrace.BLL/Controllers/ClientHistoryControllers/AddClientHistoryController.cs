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
            DateTime utcNow = DateTime.UtcNow;
            var clientHistoryCreator = new ClientHistoryController(_context);
            var clientHistoryCreate = new ClientHistory()
            {
                ClientId = clientHistory.ClientId,
                Title = clientHistory.Title,
                Description = clientHistory.Description,
                EventDate = clientHistory.EventDate,
                Created = DateTime.SpecifyKind(utcNow, DateTimeKind.Utc),
                Updated = DateTime.SpecifyKind(utcNow, DateTimeKind.Utc),
            };
            return await clientHistoryCreator.InsertClientHistory(clientHistoryCreate);
        }
    }
}
