using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegalTrace.BLL.Models.ClientHistoryDTO
{
    public class ClientHistoryDTO
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime EventDate { get; set; }
        public bool Vigency { get; set; }
    }
}
