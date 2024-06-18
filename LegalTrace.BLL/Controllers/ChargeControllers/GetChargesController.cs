using LegalTrace.BLL.Models.ChargeDTO;
using LegalTrace.DAL.Context;
using LegalTrace.DAL.Controllers.ChargeControllers;

namespace LegalTrace.BLL.Controllers.ChargeControllers
{
    public class GetChargesController
    {
        private AppDbContext _context;
        public GetChargesController(AppDbContext _dbContext)
        {
            _context = _dbContext;
        }
        public async Task<List<ChargeDTO>> GetChargeBy(int? id, int? clientId, DateTime? date, string? title, int? amount, int? type)
        {
            var chargeController = new ChargeController(_context);
            var charges = await chargeController.GetChargeBy(id, clientId,date,title,amount,type);
            if (charges.Count() > 0)
            {
                List<ChargeDTO> result = new List<ChargeDTO>();
                charges.ForEach(row => result.Add(new ChargeDTO()
                {
                    Id = row.Id,
                    ClientId = row.ClientId,
                    Title = row.Title,
                    Description = row.Description,
                    Amount = row.Amount,
                    Type = row.ChargeType.ToString(),
                    Created = row.Created,
                    FileLink = row.FileLink
                }));
                return result;
            }

            return new List<ChargeDTO>();
        }

        public async Task<ChargeDTO?> GetChargeById(int id)
        {
            var chargeController = new ChargeController(_context);
            var charge = await chargeController.GetChargeById(id);
            if (charge != null)
            {
                return new ChargeDTO()
                {
                    Id = charge.Id,
                    ClientId = charge.ClientId,
                    Title = charge.Title,
                    Description = charge.Description,
                    Amount = charge.Amount,
                    Created = charge.Created,
                    FileLink = charge.FileLink
                };
            }
            return null;
        }
    }
}
