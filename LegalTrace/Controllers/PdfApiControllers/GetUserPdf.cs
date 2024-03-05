using DinkToPdf.Contracts;
using LegalTrace.BLL.Controllers.UserControllers;
using LegalTrace.BLL.Controllers.PdfControllers;
using LegalTrace.Controllers.Services;
using LegalTrace.DAL.Context;
using LegalTrace.Models;
using Microsoft.AspNetCore.Mvc;

namespace LegalTrace.Controllers.PdfApiControllers
{
    public class GetUserPdf : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IConverter _convert;
        private readonly ResponseService _responseService;
        public GetUserPdf(IConverter convert, AppDbContext dbContext)
        {
            _convert = convert;
            _context = dbContext;
            _responseService = new ResponseService();
        }

        public async Task<IActionResult> GetUserPdfBy(int? id, string? name, string? email, DateTime? created, bool? vigency)
        {
            var pathGetter = new GetPdfController(_convert,_context);
            var usersPdf = await pathGetter.GetUserPdfPath(id, name, email, created, vigency);
            if (usersPdf != null)
            {
                if (!System.IO.File.Exists(usersPdf))
                {
                    return _responseService.CreateResponse(ApiResponse<object>.NotFoundResponse(404, "The Pdf File is not found")); 
                }
                var fileBytes = await System.IO.File.ReadAllBytesAsync(usersPdf);
                return File(fileBytes, "application/pdf", "Users.pdf");
            }
            return _responseService.CreateResponse(ApiResponse<object>.NotFoundResponse(404, "There are no users with these parameters"));
        }
    }
}
