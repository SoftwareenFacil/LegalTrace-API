using LegalTrace.DAL.Context;
using LegalTrace.DAL.Controllers.ClientHistoryControllers;

namespace LegalTrace.BLL.Controllers.ClientHistoryControllers
{
    public class DeleteClientHistoryController
    {
        private AppDbContext _context;
        public DeleteClientHistoryController(AppDbContext _dbContext)
        {
            _context = _dbContext;
        }

        public async Task<bool> DeleteClientHistoryById(int id)
        {
            var clientHistoryController = new ClientHistoryController(_context);
            var exist = await clientHistoryController.GetClientHistoryById(id);
            if (exist == null)
            {
                return false;
            }

            return await clientHistoryController.DeleteClientHistory(id);
        }
    }
}
