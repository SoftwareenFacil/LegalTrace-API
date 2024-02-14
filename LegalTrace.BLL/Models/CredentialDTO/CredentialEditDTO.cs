using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegalTrace.BLL.Models.CredentialDTO
{
    public class CredentialEditDTO
    {
        public int Id { get; set; }
        public bool Vigency { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string KeyValue { get; set; } = string.Empty;
    }
}
