using LegalTrace.DAL.Models;

namespace LegalTrace.BLL.Models.ChargeDTO
{
    public class ChargeEditDTO
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Amount { get; set; }
        public ChargeType? chargeType { get; set; }
        public string fileName { get; set; } = string.Empty;
        public string fileType { get; set; } = string.Empty;
        public string fileString { get; set; } = string.Empty;

    }
}
