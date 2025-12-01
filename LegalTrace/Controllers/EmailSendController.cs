using LegalTrace.BLL.Controllers;
using LegalTrace.BLL.Models;
using LegalTrace.Controllers.Services;
using LegalTrace.DAL.Context;
using LegalTrace.GoogleDrive.Models;
using LegalTrace.Models;
using LegalTrace.SMTP.Models;
using Microsoft.AspNetCore.Mvc;


namespace LegalTrace.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class EmailSendController : ControllerBase
    {
        private readonly EmailSendService _emailSendService;
        private readonly ResponseService _responseService;
        public EmailSendController(AppDbContext dbContext, MailParameters mailParams, GoogleServiceAccountJson gdrivesecurity)
        {
            _emailSendService = new EmailSendService(dbContext, mailParams);
            _responseService = new ResponseService();
        }

        [HttpPost]
        public async Task<IActionResult> SendPaymentReminder([FromBody] SendPaymentReminderDTO dto)
        {
            if (await _emailSendService.SendPaymentReminderAsync(dto.chargeId))
                return _responseService.CreateResponse(ApiResponse<object>.SuccessResponse(200, null, "Recordatorio de Pago para el cargo con ID: " + dto.chargeId + " enviado exitósamente"));
            return _responseService.CreateResponse(ApiResponse<object>.SuccessResponse(500, null, "Error al enviar Recordatorio de Pago para el cargo con ID: " + dto.chargeId));
        }
        [HttpGet]
        public async Task<IActionResult> GetPaymentReminderFormat(int chargeId)
        {
            var formattedResponse = await _emailSendService.GetPaymentReminderFormatAsync(chargeId);
            if(string.IsNullOrEmpty(formattedResponse))
                return _responseService.CreateResponse(ApiResponse<object>.SuccessResponse(500, null, "Error creando Recordatorio de Pago para el cargo con ID: " + chargeId));
            return _responseService.CreateResponse(ApiResponse<object>.SuccessResponse(200, formattedResponse, "Recordatorio de Pago para el cargo con ID: " + chargeId + " creado exitósamente"));

        }
    }
}