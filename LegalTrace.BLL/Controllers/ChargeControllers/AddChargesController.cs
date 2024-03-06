using LegalTrace.BLL.Models.ChargeDTO;
using LegalTrace.DAL.Context;
using LegalTrace.DAL.Controllers.ChargeControllers;
using LegalTrace.DAL.Models;

namespace LegalTrace.BLL.Controllers.ChargeControllers
{
    public class AddChargesController
    {
        private AppDbContext _context;
        public AddChargesController(AppDbContext _dbContext)
        {
            _context = _dbContext;
        }
        public async Task<int> AddCharge(ChargeInsertDTO charge)
        {
            if (!string.IsNullOrEmpty(charge.Title) && !string.IsNullOrEmpty(charge.Description) && !string.IsNullOrEmpty(charge.FileLink) && charge.Amount > 0)
            {
                var chargeController = new ChargeController(_context);
                DateTime utcNow = DateTime.UtcNow;
                var chargeCreate = new Charge()
                {
                    ClientId = charge.ClientId,
                    Title = charge.Title,
                    Description = charge.Description,
                    Date = charge.Date,
                    Amount = charge.Amount,
                    FileLink = charge.FileLink,
                    Created = DateTime.SpecifyKind(utcNow, DateTimeKind.Utc),
                    Updated = DateTime.SpecifyKind(utcNow, DateTimeKind.Utc)
                };
                return await chargeController.InsertCharge(chargeCreate);
            }
            return 0;
        }
    }
}
