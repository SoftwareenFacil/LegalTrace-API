using System.ComponentModel.DataAnnotations;

namespace LegalTrace.BLL.Models.UserTaskDTO
{
    public class UserTaskInsertDTO
    {
        public string Type { get; set; }
        public int ClientId { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
    }
}
