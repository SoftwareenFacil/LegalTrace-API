namespace LegalTrace.BLL.Models.UserTaskDTO
{
    public class UserTaskEditDTO
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime DueDate { get; set; }
        public string Type { get; set; } = string.Empty;
        public int UserId { get; set; }
        public int ClientId { get; set; }
    }
}