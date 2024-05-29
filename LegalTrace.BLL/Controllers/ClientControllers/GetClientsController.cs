using LegalTrace.BLL.Models.ClientDTO;
using LegalTrace.DAL.Context;
using LegalTrace.DAL.Controllers.ClientControllers;

namespace LegalTrace.BLL.Controllers.ClientControllers
{
    public class GetClientsController
    {
        private AppDbContext _context;
        public GetClientsController(AppDbContext _dbContext)
        {
            _context = _dbContext;
        }

        public async Task<List<ClientDTO>> GetClientBy(int? id, string? name, string? email, string? taxId, DateTime? createdFrom, DateTime? createdTo, bool? vigency)
        {
            var clientController = new ClientController(_context);
            var clients = await clientController.GetClientBy(id, name, email, taxId, createdFrom, createdTo, vigency);
            if (clients.Count() > 0)
            {
                List<ClientDTO> result = new List<ClientDTO>();
                clients.ForEach(row => result.Add(new ClientDTO()
                {
                    Id = row.Id,
                    Name = row.Name,
                    Email = row.Email,
                    TaxId = row.TaxId,
                    Phone = row.Phone,
                    Address = row.Address,
                    Created = row.Created,
                    Vigency = row.Vigency
                }));
                return result;
            }

            return new List<ClientDTO>();
        }

        public async Task<ClientDTO?> GetClientById(int id)
        {
            var clientController = new ClientController(_context);
            var client = await clientController.GetClientById(id);
            if (client != null)
            {
                return new ClientDTO()
                {
                    Id = client.Id,
                    Name = client.Name,
                    Email = client.Email,
                    TaxId = client.TaxId,
                    Phone = client.Phone,
                    Address = client.Address,
                    Created = client.Created,
                    Vigency = client.Vigency
                };
            }
            return null;
        }
    }
}