using LegalTrace.BLL.Models.ChargeDTO;
using LegalTrace.DAL.Context;
using LegalTrace.DAL.Controllers.ChargeControllers;
using LegalTrace.DAL.Controllers.ClientControllers;
using LegalTrace.DAL.Models;

namespace LegalTrace.BLL.Controllers.ChargeControllers
{
    public class UpdateChargesController
    {
        private AppDbContext _context;
        public UpdateChargesController(AppDbContext _dbContext)
        {
            _context = _dbContext;
        }
        public async Task<int> UpdateCharge(ChargeEditDTO chargeEdited)
        {
            if (chargeEdited.Amount <= 0 && string.IsNullOrWhiteSpace(chargeEdited.Title) && string.IsNullOrWhiteSpace(chargeEdited.Description) && chargeEdited.Date > DateTime.Now && string.IsNullOrWhiteSpace(chargeEdited.FileLink) && chargeEdited.Amount == 0 && chargeEdited.ClientId == 0)
                return 400;

            var chargeController = new ChargeController(_context);
            var charge = await chargeController.GetChargeById(chargeEdited.Id);
            if (charge != null)
            {
                if (chargeEdited.ClientId > 0)
                {
                    var clientController = new ClientController(_context);
                    var client = await clientController.GetClientById(chargeEdited.ClientId);
                    if (client == null)
                        return -1;
                    charge.ClientId = chargeEdited.ClientId;
                }

                if(chargeEdited.chargeType != null)
                {
                    charge.ChargeType = (ChargeType)((int)chargeEdited.chargeType >= 3 ? 0 : chargeEdited.chargeType);
                }

                charge.Title = !string.IsNullOrEmpty(chargeEdited.Title) ? chargeEdited.Title : charge.Title;
                charge.Description = !string.IsNullOrEmpty(chargeEdited.Description) ? chargeEdited.Description : charge.Description;
                charge.Amount = chargeEdited.Amount > 0 ? chargeEdited.Amount : charge.Amount;
                charge.FileLink = !string.IsNullOrEmpty(chargeEdited.FileLink) ? chargeEdited.FileLink : charge.FileLink;
                DateTime utcNow = DateTime.UtcNow;
                charge.Date = (chargeEdited.Date < utcNow) ? chargeEdited.Date : charge.Date;
                charge.Updated = DateTime.SpecifyKind(utcNow, DateTimeKind.Utc);

                var isUpdated = await chargeController.UpdateCharge(charge);
                if (!isUpdated)
                    return 400;
                return 200;
            }
            return 404;
        }
    }
}
