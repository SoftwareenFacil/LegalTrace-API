namespace LegalTrace.BLL.Models.ChargeDTO
{
    public class ChargeInsertDTO
    {
        public int ClientId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public int Amount { get; set; }
        public string FileLink { get; set; }
    }
}
