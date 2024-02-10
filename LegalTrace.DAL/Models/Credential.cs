using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LegalTrace.DAL.Models
{
    [Table("credentials")]
    public class Credential
    {
        [Key, Required]
        public int Id { get; set; }
        [Required]
        public int ClientId { get; set; }
        [Required]
        public bool Vigency { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string KeyValue { get; set; }
        [Required]
        public DateTime Created { get; set; }
        [Required]
        public DateTime Updated { get; set; }

        public Client Client { get; set; }

    }
}
