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

        public async Task<(bool, bool)> UpdateClient(ClientEditDTO clientEdited)
        {
            bool isUpdated = false;
            bool isClient = false;
            if (string.IsNullOrWhiteSpace(clientEdited.Name) && string.IsNullOrWhiteSpace(clientEdited.Email) && clientEdited.Phone == 0 && string.IsNullOrWhiteSpace(clientEdited.Address) && string.IsNullOrWhiteSpace(clientEdited.TaxId))
                return (isUpdated, isClient);

            var clientVerify = new ClientGetById(_context);
            var clientUpdater = new ClientUpdate(_context);
            var client = await clientVerify.GetClientById(clientEdited.Id);
            if (client != null)
            {
                isClient = true;
                client.Name = !string.IsNullOrEmpty(clientEdited.Name) ? clientEdited.Name : client.Name;
                client.Email = !string.IsNullOrEmpty(clientEdited.Email) ? clientEdited.Email : client.Email;
                client.Phone = clientEdited.Phone > 0 ? clientEdited.Phone : client.Phone;
                client.Address = !string.IsNullOrEmpty(clientEdited.Address) ? clientEdited.Address : client.Address;
                client.TaxId = !string.IsNullOrEmpty(clientEdited.TaxId) ? clientEdited.TaxId : client.TaxId;

                DateTime utcNow = DateTime.UtcNow;
                client.Updated = DateTime.SpecifyKind(utcNow, DateTimeKind.Utc);

                isUpdated = await clientUpdater.UpdateClient(client);

                return (isUpdated, isClient);
            }
            return (isUpdated, isClient);
        }
    }
}
