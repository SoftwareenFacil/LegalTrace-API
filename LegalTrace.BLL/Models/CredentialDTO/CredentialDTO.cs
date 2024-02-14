using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegalTrace.BLL.Models.CredentialDTO
{
    public class CredentialDTO
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public bool Vigency { get; set; }
        public string Title { get; set; }
        public string Username { get; set; }
        public string KeyValue { get; set; }
        public DateTime Created { get; set; }
    }
}
