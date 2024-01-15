using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegalTrace.DAL.Models
{
    [Table("standardTask")]
    public class StandardTask
    {
        [Key, Required]
        public int Id { get; set; }
        [Required]
        public int ClientId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        public bool Repeatable { get; set; }
        [Required]
        public DateTime Created { get; set; }
        [Required]
        public DateTime Updated { get; set; }
        [Required]
        public int DaysToFinish { get; set; }
    }
}
