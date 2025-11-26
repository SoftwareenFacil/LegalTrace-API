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
            _httpClient.DefaultRequestHeaders.Add("Content-Type", "application/json");

            _fromEmail = fromEmail;
        }

        public async Task<bool> SendEmailAsync(string to, string subject, string htmlBody)
        {
            var payload = new
            {
                from = new { email = _fromEmail },
                to = new[] { new { email = to } },
                subject = subject,
                html = htmlBody
            };

            var response = await _httpClient.PostAsJsonAsync("email", payload);
            return response.IsSuccessStatusCode;
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