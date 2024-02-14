using LegalTrace.BLL.Models.ClientDTO;
using LegalTrace.DAL.Context;
using LegalTrace.DAL.Controllers.ClientControllers;
using LegalTrace.DAL.Models;

namespace LegalTrace.BLL.Controllers.ClientControllers
{
    public class AddClientsController
    {
        private AppDbContext _context;
        public AddClientsController(AppDbContext _dbContext)
        {
            _context = _dbContext;
        }

        public async Task<int> AddClient(ClientInsertDTO client)
        {
            var clientController = new ClientController(_context);
            if (await clientController.GetClientByEmail(client.Email) != null)
                return -1;

            DateTime utcNow = DateTime.UtcNow;
            var clientCreate = new Client()
            {
                Name = client.Name,
                Email = client.Email,
                Phone = client.Phone,
                TaxId = client.TaxId,
                Address = client.Address,
                Created = DateTime.SpecifyKind(utcNow, DateTimeKind.Utc),
                Updated = DateTime.SpecifyKind(utcNow, DateTimeKind.Utc),
                Vigency = true
            };
            return await clientController.InsertClient(clientCreate);
        }
    }
}
