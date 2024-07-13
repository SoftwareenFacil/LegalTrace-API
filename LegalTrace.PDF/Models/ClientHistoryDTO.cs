
namespace LegalTrace.PDF.Models
{
    public class ClientHistoryDTO
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime EventDate { get; set; }
        public DateTime Created { get; set; }
        public bool Vigency { get; set; }
    }
}
