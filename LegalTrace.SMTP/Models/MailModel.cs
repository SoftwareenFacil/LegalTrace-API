using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegalTrace.SMTP.Models
{
    public class MailModel
    {
        public SenderData SenderInfo { get; set; }
        public MailData MailInfo { get; set; }
        public AccountData AccountInfo { get; set; }
        public ChargeData ChargeInfo { get; set; }
    }
    public class AccountData
    {
        public string AccountNumber { get; set; }
        public string AccountType { get; set; }
        public string BankName { get; set; }
        public string Rut { get; set; }
        public string AccountName { get; set; }
    }

    public class MailData
    {
        public string MainHeading { get; set; }
        public string MainBody { get; set; }

    }
    public class ChargeData
    {
        public string companyName { get; set; }
        public string mainHeading { get; set; }
        public string chargeTitle { get; set; }
        public string description { get; set; }
        public string chargeType { get; set; }
        public string emissionDate { get; set; }
        public string totalAmount { get; set; }
    }
    public class SenderData
    {
        public string TeamName { get; set; }
        public string SystemShortName { get; set; }
        public string SystemFullName { get; set; }
    }
}
