using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegalTrace.SMTP.Models
{
    public class Payload
    {
        public FromToEmail from { get; set; }
        public FromToEmail[] to { get; set; }
        public string subject { get; set; }
        public string? html { get; set; }
        public string? text { get; set; }
    }

    public class FromToEmail
    {
        public string email { get; set; }
    }
}
