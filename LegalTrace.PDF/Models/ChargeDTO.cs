namespace LegalTrace.PDF.Models
{
    public class ChargeDTO
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Amount { get; set; }
        public string Type { get; set; }
        public DateTime Created { get; set; }
        public string FileLink { get; set; }
    }
}
