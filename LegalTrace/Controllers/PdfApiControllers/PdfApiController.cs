using DinkToPdf.Contracts;
using LegalTrace.DAL.Context;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace LegalTrace.Controllers.PdfApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PdfApiController : ControllerBase
    {
        private AppDbContext _context;
        private string _logoLoc;

        public PdfApiController(AppDbContext dbContext, IConfiguration configuration)
        {
            _context = dbContext;
            _logoLoc = configuration["LogoLocation"];
        }
        [HttpGet]
        [Route("MovementsFromClient")]
        public async Task<IActionResult> GetMonthlyMovementsFromClient(DateTime month, int id)
        {
            var userPdfGetter = new PdfBLLController(_context, _logoLoc);
            return await userPdfGetter.GetMonthlyMovementsFromClient(id, month);
        }
        [HttpGet]
        [Route("ClientWithNoMovements")]
        public async Task<IActionResult> GetClientsWithNoMovementsInMonth(DateTime month)
        {
            var userPdfGetter = new PdfBLLController(_context, _logoLoc);
            return await userPdfGetter.GetClientsWithNoMovementsInMonth(month);
        }
    }
}
