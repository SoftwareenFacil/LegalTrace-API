using DinkToPdf;
using DinkToPdf.Contracts;
using LegalTrace.PDF.Models;
using LegalTrace.PDF.Helpers;

namespace LegalTrace.PDF.Controllers
{
    public class GetPdf
    {
        private readonly IConverter _convert;

        public GetPdf(IConverter convert)
        {
            _convert = convert;
        }

        public async Task<string?> GetPdfPath(List<UserDTO> users)
        {
            string exportPath = Path.Combine(Directory.GetCurrentDirectory(), "Exports");
            if (!Directory.Exists(exportPath))
            {
                Directory.CreateDirectory(exportPath);
            }
            string fileName = "Users.pdf";
            var glb = new GlobalSettings
            {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Landscape,
                PaperSize = PaperKind.A4,
                Margins = new MarginSettings()
                {
                    Bottom = 20,
                    Left = 20,
                    Right = 20,
                    Top = 30
                },
                DocumentTitle = "Users",
                Out = Path.Combine(Directory.GetCurrentDirectory(), "Exports", fileName)
            };
            var objectSettings = new ObjectSettings
            {
                PagesCount = true,
                HtmlContent = UserHelper.ToHtmlFile(users),
                WebSettings = { DefaultEncoding = "utf-8", UserStyleSheet = null }
            };



            var pdf = new HtmlToPdfDocument
            {
                GlobalSettings = glb,
                Objects = { objectSettings }
            };

            var pdfFile = await Task.Run(() => _convert.Convert(pdf));

            // Especifica la ruta del archivo PDF generado
            var filePath = glb.Out;

            return filePath;
        }       

    }
}
