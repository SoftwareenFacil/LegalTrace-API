using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegalTrace.BLL.Models.ClientDTO
{
    public class ClientDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int Phone { get; set; }
        public string TaxId { get; set; }
        public string Address { get; set; } = string.Empty;
        public DateTime Created { get; set; }
        public bool Vigency { get; set; }
    }
}
