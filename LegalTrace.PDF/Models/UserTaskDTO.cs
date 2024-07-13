
namespace LegalTrace.PDF.Models
{
    public class UserTaskDTO
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public bool Finished { get; set; }
        public bool Vigency { get; set; }
        public bool Repeatable { get;set; }
        public DateTime Created { get; set; }
        public DateTime DueDate { get; set; }
    }
}
