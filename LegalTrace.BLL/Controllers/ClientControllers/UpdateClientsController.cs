﻿using LegalTrace.BLL.Models.ClientDTO;
using LegalTrace.DAL.Context;
using LegalTrace.DAL.Controllers.ClientControllers;

namespace LegalTrace.BLL.Controllers.ClientControllers
{
    public class UpdateClientsController
    {
        private AppDbContext _context;
        public UpdateClientsController(AppDbContext _dbContext)
        {
            _context = _dbContext;
        }

        public async Task<int> UpdateClient(ClientEditDTO clientEdited)
        {
            if (string.IsNullOrWhiteSpace(clientEdited.Name) && string.IsNullOrWhiteSpace(clientEdited.Email) && clientEdited.Phone == 0 && string.IsNullOrWhiteSpace(clientEdited.Address) && string.IsNullOrWhiteSpace(clientEdited.TaxId))
                return 400;

            var clientController = new ClientController(_context);
            var client = await clientController.GetClientById(clientEdited.Id);
            if (client != null)
            {
                client.Name = !string.IsNullOrEmpty(clientEdited.Name) ? clientEdited.Name : client.Name;
                client.Email = !string.IsNullOrEmpty(clientEdited.Email) ? clientEdited.Email : client.Email;
                client.Phone = clientEdited.Phone > 0 ? clientEdited.Phone : client.Phone;
                client.Address = !string.IsNullOrEmpty(clientEdited.Address) ? clientEdited.Address : client.Address;
                client.TaxId = !string.IsNullOrEmpty(clientEdited.TaxId) ? clientEdited.TaxId : client.TaxId;

                DateTime utcNow = DateTime.UtcNow;
                client.Updated = DateTime.SpecifyKind(utcNow, DateTimeKind.Utc);

                var isUpdated = await clientController.UpdateClient(client);
                if (!isUpdated) 
                    return 400;
                return 200;
            }
            return 404;
        }

        public async Task<int> UpdateClientVigency(int id)
        {
            var clientController = new ClientController(_context);
            var client = await clientController.GetClientById(id);
            if (client != null)
            {
                if (client.Vigency)
                {
                    client.Vigency = false;
                }
                else { client.Vigency = true; }

                var isUpdated = await clientController.UpdateClient(client);
                if (isUpdated)
                    return 200;
                return 500;
            }
            return 404;
        }
    }
}
