namespace LegalTrace.BLL.Models.UserTaskDTO
{
    public class UserTaskEditDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public string Type { get; set; }
        public int UserId { get; set; }
        public int ClientId { get; set; }
    }
}