using DinkToPdf.Contracts;
using LegalTrace.DAL.Context;
using Microsoft.AspNetCore.Mvc;

namespace LegalTrace.Controllers.PdfApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PdfApiController : ControllerBase
    {
        private readonly IConverter _convert;
        private AppDbContext _context;

        public PdfApiController(IConverter convert, AppDbContext dbContext)
        {
            _convert = convert;
            _context = dbContext;
        }
        [HttpGet]
        public async Task<IActionResult> GeneratePdf(int? id, string? name, string? email, DateTime? created, bool? vigency)
        {
            var userPdfGetter = new GetUserPdf(_convert, _context);
            return await userPdfGetter.GetUserPdfBy(id, name, email, created, vigency);
        }
    }
}
