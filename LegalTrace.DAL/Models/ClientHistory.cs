using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LegalTrace.DAL.Models
{
    [Table("clientHistory")]
    public class ClientHistory
    {
        [Key, Required]
        public int Id { get; set; }
        [Required]
        public int ClientId { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public DateTime EventDate { get; set; }
        [Required]
        public DateTime Created { get; set; }
        [Required]
        public DateTime Updated { get; set; }
    }
}
