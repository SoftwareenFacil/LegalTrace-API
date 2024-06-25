using LegalTrace.DAL.Models;

namespace LegalTrace.DAL.Controllers.ChargeControllers
{
    public interface IChargeController
    {
        Task<Charge> GetChargeById(int id);
        Task<List<Charge>> GetChargeBy(int? id, int? clientId, DateTime? date, DateTime? dateTo, string? title, int? amount, int? type);
        Task<bool> DeleteCharge(int id);
        Task<int> InsertCharge(Charge charge);
        Task<bool> UpdateCharge(Charge charge);
    }
}
