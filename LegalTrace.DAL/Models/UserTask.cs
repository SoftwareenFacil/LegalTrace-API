using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LegalTrace.DAL.Models
{
    [Table("userTasks")]
    public class UserTask
    {
        [Key, Required]
        public int Id { get; set; }
        [Required] 
        public int ClientId { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        public bool Repeatable { get; set; }
        [Required]
        public bool Vigency { get; set; }
        [Required]
        public DateTime Created { get; set; }
        [Required]
        public DateTime Updated { get; set; }
        [Required]
        public bool Finished { get; set; }
        [Required]
        public DateTime FinishedDate { get; set; }
    }
}
