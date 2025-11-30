using LegalTrace.SMTP.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http.Json;
using System.Net.Mail;
using System.Threading.Tasks;

namespace LegalTrace.SMTP
{

    public class MailerSendEmailService
    {
        private readonly HttpClient _httpClient;
        private readonly string _fromEmail;

        public MailerSendEmailService(string fromEmail, string apiKey)
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://api.mailersend.com/v1/");
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");

            _fromEmail = fromEmail;
        }

        public async Task<bool> SendEmailAsync(string to, string subject, string htmlBody)
        {
            try
            {
                var payload = new Payload
                {
                    from = new FromToEmail { email = _fromEmail },
                    to = new FromToEmail[] { new FromToEmail { email = to } },
                    subject = subject,
                    html = htmlBody
                };

                var response = await _httpClient.PostAsJsonAsync("email", payload);
                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"MailerSend API Error: {response.StatusCode} - {errorContent}");
                }
                return response.IsSuccessStatusCode;
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> SendValidationEmailAsync()
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Add("X-Requested-With", "XMLHttpRequest");
                var payload = new Payload
                {
                    from = new FromToEmail { email = "from@softwareenfacil.com" },
                    to = new FromToEmail[] { new FromToEmail { email = "contacto.softwareenfacil@gmail.com" } },
                    subject = "Hello from MailerSend!",
                    text = "Greetings from the team, you got this message through MailerSend.",
                    html = "Greetings from the team, you got this message through MailerSend."
                };


                var response = await _httpClient.PostAsJsonAsync("email", payload);
                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"MailerSend API Error: {response.StatusCode} - {errorContent}");
                }
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task SendBulkEmailAsync(IEnumerable<string> recipients, string subject, string htmlBody)
        {
            var toList = recipients.Select(r => new { email = r });

            var payload = new
            {
                from = new { email = _fromEmail },
                to = toList,
                subject = subject,
                html = htmlBody
            };

            var response = await _httpClient.PostAsJsonAsync("email", payload);

            response.EnsureSuccessStatusCode();
        }
    }

}