﻿using Stock_Back.BLL.Models.ClientDTO;
using Stock_Back.DAL.Context;
using Stock_Back.DAL.Controllers.ClientControllers;

namespace Stock_Back.BLL.Controllers.ClientControllers
{
    public class UpdateClientsController
    {
        private AppDbContext _context;
        public UpdateClientsController(AppDbContext _dbContext)
        {
            _context = _dbContext;
        }

        public async Task<(bool, bool)> UpdateClient(ClientDTO clientEdited)
        {
            bool isUpdated = false;
            bool isClient = false;
            if (string.IsNullOrWhiteSpace(clientEdited.Name) && string.IsNullOrWhiteSpace(clientEdited.Email) && clientEdited.Phone == 0)
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
                isUpdated = await clientUpdater.UpdateClient(client);

                return (isUpdated, isClient);
            }
            return (isUpdated, isClient);
        }
    }
}
