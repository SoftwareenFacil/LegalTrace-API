using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegalTrace.BLL.Models.CredentialDTO
{
    public class CredentialInsertDTO
    {
        public int ClientId { get; set; }
        public string Title { get; set; }
        public string Username { get; set; }
        public string KeyValue { get; set; }
    }
}
