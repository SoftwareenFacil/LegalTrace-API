using LegalTrace.BLL.Models.ClientHistoryDTO;
using LegalTrace.DAL.Context;
using LegalTrace.DAL.Controllers.ClientHistoryControllers;


namespace LegalTrace.BLL.Controllers.ClientHistoryControllers
{
    public class UpdateClientHistoryController
    {
        private AppDbContext _context;
        public UpdateClientHistoryController(AppDbContext _dbContext)
        {
            _context = _dbContext;
        }

        public async Task<int> UpdateClientHistory(ClientHistoryEditDTO clientHistoryEdited)
        {
            if(clientHistoryEdited.Id > 0)
            {
                if (string.IsNullOrWhiteSpace(clientHistoryEdited.Title) && string.IsNullOrWhiteSpace(clientHistoryEdited.Description))
                    return 400;

                var clientHistoryController = new ClientHistoryController(_context);
                var clientHistory = await clientHistoryController.GetClientHistoryById(clientHistoryEdited.Id);
                if (clientHistory != null)
                {
                    clientHistory.Title = !string.IsNullOrEmpty(clientHistoryEdited.Title) ? clientHistoryEdited.Title : clientHistory.Title;
                    clientHistory.Description = !string.IsNullOrEmpty(clientHistoryEdited.Description) ? clientHistoryEdited.Description : clientHistory.Description;
                    clientHistory.EventDate = (clientHistoryEdited.EventDate > DateTime.Now) ? clientHistoryEdited.EventDate : clientHistory.EventDate;
                    DateTime utcNow = DateTime.UtcNow;
                    clientHistory.Updated = DateTime.SpecifyKind(utcNow, DateTimeKind.Utc);

                    var isUpdated = await clientHistoryController.UpdateClientHistory(clientHistory);
                    if (!isUpdated)
                        return 400;
                    return 200;
                }
                return 404;
            }
            return 400;
        }
    }
}
