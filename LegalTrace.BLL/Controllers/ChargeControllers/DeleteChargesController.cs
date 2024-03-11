using LegalTrace.DAL.Context;
using LegalTrace.DAL.Controllers.ChargeControllers;

namespace LegalTrace.BLL.Controllers.ChargeControllers
{
    public class DeleteChargesController
    {
        private AppDbContext _context;
        public DeleteChargesController(AppDbContext _dbContext)
        {
            _context = _dbContext;
        }
        public async Task<bool> DeleteChargeById(int id)
        {
            var chargeController = new ChargeController(_context);
            var exist = await chargeController.GetChargeById(id);
            if (exist == null)
            {
                return false;
            }

            return await chargeController.DeleteCharge(id);
        }
    }
}
