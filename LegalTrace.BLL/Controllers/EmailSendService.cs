using LegalTrace.DAL.Context;
using LegalTrace.DAL.Models;
using LegalTrace.DAL.Repository;
using LegalTrace.SMTP;
using LegalTrace.SMTP.Models;
using System;
using System.Globalization;

namespace LegalTrace.BLL.Controllers
{
    public class EmailSendService
    {
        private readonly ChargeRepository _repository;
        private readonly MailParameters _mailParameters;
        public EmailSendService(AppDbContext _dbContext, MailParameters mailParameters)
        {
            _repository = new ChargeRepository(_dbContext);
            _mailParameters = mailParameters;
        }

        public async Task<bool> SendPaymentReminderAsync(int chargeId)
        {
            //Extract the charge from DB
            var charge = await _repository.GetChargeById(chargeId);
            if (charge == null)
                return false;
            var mailModel = MapFromCharge(charge);
            var htmlBody = PendingChargeEmailBuilder.Build(mailModel.ChargeInfo,mailModel.MailInfo,mailModel.AccountInfo,mailModel.SenderInfo);
            var emailService = new MailerSendEmailService(_mailParameters.fromAddress, _mailParameters.apiKey);        
            return await emailService.SendEmailAsync(charge.Client.Email, mailModel.ChargeInfo.chargeTitle, htmlBody);
        }
        public async Task<string> GetPaymentReminderFormatAsync(int chargeId)
        {
            var charge = await _repository.GetChargeById(chargeId);
            if (charge == null)
                return "";
            var mailModel = MapFromCharge(charge);
            return PendingChargeEmailBuilder.Build(mailModel.ChargeInfo, mailModel.MailInfo, mailModel.AccountInfo, mailModel.SenderInfo);
        }

        private MailModel MapFromCharge(Charge charge)
        {
            var accountdata = new AccountData
            {
                AccountName = "LegalTrace",
                AccountNumber = "123456",
                AccountType = "Corriente",
                BankName = "Santander",
                Rut = "12.345.678-9"

            };
            var chargeData = new ChargeData
            {
                chargeTitle = charge.Title,
                chargeType = charge.ChargeType.ToString(),
                companyName = charge.Client.Name,
                description = charge.Description,
                emissionDate = charge.Created.ToLongDateString(),
                mainHeading = "Notificacion de Cargo Pendiente",
                totalAmount = FormatAmount(charge.Amount)

            };
            var mailData = new MailData
            {
                MainBody = "Le informamos que se ha generado un nuevo cargo en su cuenta. A continuación, encontrará los detalles:",
                MainHeading = "Notificacion de Cargo Pendiente"
            };
            var senderData = new SenderData
            {
                SystemFullName = "LegalTrace",
                SystemShortName = "LT",
                TeamName = "LegalTrace"
            };
            var mailModel = new MailModel
            {
                AccountInfo = accountdata,
                ChargeInfo = chargeData,
                MailInfo = mailData,
                SenderInfo = senderData
            };
            return mailModel;
        }

        private static string FormatAmount(decimal amount)
        {
            CultureInfo cl = new CultureInfo("es-CL"); // Thousands separator = dot
            return "$" + amount.ToString("N0", cl);
        }

    }
}
