﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LegalTrace.DAL.Models
{
    [Table("clients")]
    public class Client
    {
        [Key,Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public int Phone { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string TaxId { get; set; }
        [Required]
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        [Required]
        public bool Vigency { get; set; }
        public List<UserTask> UserTasks { get; set; }
        public List<Credential> Credentials { get; set; }
        public List<ClientHistory> History { get; set; }

        public List<Charge> Charges { get; set; } 
    }
}
