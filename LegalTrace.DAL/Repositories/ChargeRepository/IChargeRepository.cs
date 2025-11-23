using LegalTrace.DAL.Models;

namespace LegalTrace.DAL.Repository
{
    public interface IChargeRepository
    {
        Task<Charge> GetChargeById(int id);
        Task<List<Charge>> GetChargeBy(int? id, int? clientId, DateTime? date, DateTime? dateTo, string? title, int? type, double? lowerLimit, double? upperLimit);
        Task<bool> DeleteCharge(int id);
        Task<int> InsertCharge(Charge charge);
        Task<bool> UpdateCharge(Charge charge);
    }
}
