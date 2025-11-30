using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegalTrace.SMTP.Models
{
    public class MailParameters
    {
        public string apiKey {  get; set; }
        public string fromAddress { get; set; }
        public string fromName { get; set; }
    }
}
