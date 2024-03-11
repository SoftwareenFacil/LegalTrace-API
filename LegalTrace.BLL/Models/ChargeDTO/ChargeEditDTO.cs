namespace LegalTrace.BLL.Models.ChargeDTO
{
    public class ChargeEditDTO
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public int Amount { get; set; }
        public string FileLink { get; set; } = string.Empty;
    }
}
