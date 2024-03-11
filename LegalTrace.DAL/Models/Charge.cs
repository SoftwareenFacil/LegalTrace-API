using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LegalTrace.DAL.Models
{
    [Table("charges")]
    public class Charge
    {
        [Key, Required]
        public int Id { get; set; }
        [Required]
        public int ClientId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public int Amount { get; set; }
        [Required]
        public DateTime Created { get; set; }
        [Required]
        public DateTime Updated { get; set; }
        [Required]
        public string FileLink { get; set; }
    }
}
