using LegalTrace.PDF.Models;
using DinkToPdf.Contracts;
using LegalTrace.PDF.Controllers;
using LegalTrace.BLL.Controllers.UserControllers;
using LegalTrace.DAL.Context;

namespace LegalTrace.BLL.Controllers.PdfControllers
{
    public class GetPdfController
    {
        private readonly IConverter _convert;
        private readonly AppDbContext _context;

        public GetPdfController(IConverter convert, AppDbContext dbContext)
        {
            _convert = convert;
            _context = dbContext;
        }
        public async Task<string?> GetUserPdfPath(int? id, string? name, string? email, DateTime? created, bool? vigency)
        {
            var userGetter = new GetUsersController(_context);
            var users = await userGetter.GetUsersBy(id, name, email, created, vigency);
            if (users.Count() > 0)
            {
                List<UserDTO> pdfUsers = new List<UserDTO>();
                foreach (var user in users)
                {
                    pdfUsers.Add(new UserDTO
                    {
                        Id = user.Id,
                        Name = user.Name,
                        Email = user.Email,
                        Phone = user.Phone,
                        Address = user.Address,
                        SuperAdmin = user.SuperAdmin,
                        Created = user.Created,
                        Vigency = user.Vigency
                    });
                }
                var pdfGetter = new GetPdf(_convert);
                var pdfPath = await pdfGetter.GetPdfPath(pdfUsers);
                return pdfPath;
            }
            return null;
        }
    }
}
