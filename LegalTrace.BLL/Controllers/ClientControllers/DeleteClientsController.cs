using LegalTrace.DAL.Context;
using LegalTrace.DAL.Controllers.ClientControllers;

namespace LegalTrace.BLL.Controllers.ClientControllers
{
    public class DeleteClientsController
    {
        private AppDbContext _context;
        public DeleteClientsController(AppDbContext _dbContext)
        {
            _context = _dbContext;
        }

        public async Task<bool> DeleteClientById(int id)
        {
            var clientController = new ClientController(_context);
            var exist = await clientController.GetClientById(id);
            if (exist == null)
            {
                return false;
            }

            return await clientController.DeleteClient(id);
        }
    }
}
