using LegalTrace.PDF.Models;
using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;


namespace LegalTrace.PDF.Controllers
{
    public class SharpLibrary
    {
        public string GeneratePdfNoMovementsReport(List<ClientDTO> Clients, string TemporalFolder)
        {
            PdfDocument document = new PdfDocument();
            document.Info.Title = "Reporte Cliente";

            // Create an empty page
            PdfPage page = document.AddPage();
            XGraphics gfx = XGraphics.FromPdfPage(page);
            var FileName = "Clientes_Sin_Movimientos_" + DateTime.Now.ToString("dd-MM-yyyy") + ".pdf";
            XFont font = new XFont("Verdana", 11, XFontStyle.Regular);
            XFont titleFont = new XFont("Verdana", 16, XFontStyle.Bold);
            XFont columnTitleFont = new XFont("Verdana", 12, XFontStyle.Bold);

            int yPoint = 40;
            string title = "Reporte Clientes Sin Movimientos - Fecha: " + DateTime.Now.ToShortDateString();
            gfx.DrawString(title, titleFont, XBrushes.Black, new XRect(20, yPoint, page.Width, page.Height), XStringFormats.TopLeft);
            yPoint += 40;

            string titleLine = "ID -   Nombre   - Telefono - RUT";
            gfx.DrawString(titleLine, columnTitleFont, XBrushes.Black, new XRect(20, yPoint, page.Width, page.Height), XStringFormats.TopLeft);
            yPoint += 30;
            foreach (var client in Clients)
            {
                string clientLine = $"{client.Id.ToString("000")} - {TrimOrPad(client.Name, 10)} - {TrimOrPad(client.Phone.ToString(), 10)} - {TrimOrPad(client.TaxId, 10):C}";
                gfx.DrawString(clientLine, font, XBrushes.Black, new XRect(20, yPoint, page.Width, page.Height), XStringFormats.TopLeft);
                yPoint += 20;
            }

            // Save the document
            string FilePath = Path.Combine(TemporalFolder, FileName);
            document.Save(FilePath);
            return FilePath;
        }
        public string GeneratePdfClientReport(List<ClientHistoryDTO> clienthistory, List<UserTaskDTO> clientTasks, List<ChargeDTO> clientCharges,int clientid, DateTime month, string TemporalFolder)
        {
            // Create a new PDF document
            PdfDocument document = new PdfDocument();
            document.Info.Title = "Reporte Cliente";

            // Create an empty page
            PdfPage page = document.AddPage();
            XGraphics gfx = XGraphics.FromPdfPage(page);
            var FileName = "Cliente_" + clientid.ToString("000") + "_" + month.ToString("dd-MM-yyyy") + "_Reporte.pdf";
            XFont font = new XFont("Verdana", 11, XFontStyle.Regular);
            XFont titleFont = new XFont("Verdana", 16, XFontStyle.Bold);
            XFont subTitleFont = new XFont("Verdana", 14, XFontStyle.Bold);
            XFont columnTitleFont = new XFont("Verdana", 12, XFontStyle.Bold);
            // Draw the Title
            int yPoint = 40;
            string title = "Reporte Cliente ID: " + clientid + " - Fecha: " + month.ToString("MM-yyyy");
            gfx.DrawString(title, titleFont, XBrushes.Black, new XRect(20, yPoint, page.Width, page.Height), XStringFormats.TopLeft);
            yPoint += 40;

            //Subtitle 1 and content
            string subtitle = "Registro Historial Cliente";
            gfx.DrawString(subtitle, subTitleFont, XBrushes.Black, new XRect(20, yPoint, page.Width, page.Height), XStringFormats.TopLeft);
            yPoint += 30;
            string titleLine = "ID -   Fecha   -       Titulo       -     Descripcion";
            gfx.DrawString(titleLine, columnTitleFont, XBrushes.Black, new XRect(20, yPoint, page.Width, page.Height), XStringFormats.TopLeft);
            yPoint += 20;
            foreach (var history in clienthistory)
            {
                string productLine = $"{history.Id.ToString("00")} - {TrimOrPad(history.Created.ToShortDateString(), 10)} - {TrimOrPad(history.Title, 20)} - {TrimOrPad(history.Description, 40):C}";
                gfx.DrawString(productLine, font, XBrushes.Black, new XRect(20, yPoint, page.Width, page.Height), XStringFormats.TopLeft);
                yPoint += 20;
            }
            // Draw a dotted line
            DrawDottedLine(gfx, 20, yPoint + 15, page.Width - 40);
            yPoint += 20;
            //Subtitle 2 and content UserTask
            subtitle = "Registro Tareas Cliente";
            gfx.DrawString(subtitle, subTitleFont, XBrushes.Black, new XRect(20, yPoint, page.Width, page.Height), XStringFormats.TopLeft);
            yPoint += 30;
            titleLine = "ID -   Fecha   -         Titulo         - Usuario -    Descripcion";
            gfx.DrawString(titleLine, columnTitleFont, XBrushes.Black, new XRect(20, yPoint, page.Width, page.Height), XStringFormats.TopLeft);
            yPoint += 20;
            foreach (var Task in clientTasks)
            {
                string productLine = $"{Task.Id.ToString("00")} - {TrimOrPad(Task.Created.ToShortDateString(), 10)} - {TrimOrPad(Task.Title, 20)} - {Task.UserId.ToString("00")} - {TrimOrPad(Task.Description, 35):C}";
                gfx.DrawString(productLine, font, XBrushes.Black, new XRect(20, yPoint, page.Width, page.Height), XStringFormats.TopLeft);
                yPoint += 20;
            }
            // Draw a dotted line
            DrawDottedLine(gfx, 20, yPoint + 15, page.Width - 40);
            yPoint += 20;
            //Subtitle 3 and content Charges
            subtitle = "Registro Cobros Cliente";
            gfx.DrawString(subtitle, subTitleFont, XBrushes.Black, new XRect(20, yPoint, page.Width, page.Height), XStringFormats.TopLeft);
            yPoint += 30;
            titleLine = "ID -   Fecha   -        Titulo        -   Monto   -    Descripcion";
            gfx.DrawString(titleLine, columnTitleFont, XBrushes.Black, new XRect(20, yPoint, page.Width, page.Height), XStringFormats.TopLeft);
            yPoint += 20;
            foreach (var Charge in clientCharges)
            {
                string productLine = $"{Charge.Id.ToString("00")} - {TrimOrPad(Charge.Created.ToShortDateString(), 10)} - {TrimOrPad(Charge.Title, 20)} - ${TrimOrPad(Charge.Amount.ToString(), 10)} - {TrimOrPad(Charge.Description, 35):C}";
                gfx.DrawString(productLine, font, XBrushes.Black, new XRect(20, yPoint, page.Width, page.Height), XStringFormats.TopLeft);
                yPoint += 20;
            }

            // Save the document
            string FilePath = Path.Combine(TemporalFolder, FileName);
            document.Save(FilePath);
            return FilePath;
        }
        private string TrimOrPad(string value, int length)
        {
            if (value.Length > length)
            {
                return value.Substring(0, length);
            }
            else
            {
                int spacesToAdd = length - value.Length;
                int padLeft = spacesToAdd / 2;
                int padRight = spacesToAdd - padLeft;
                return value.PadLeft(value.Length + padLeft).PadRight(length);
            }
        }
        private void DrawDottedLine(XGraphics gfx, double x1, double y1, double x2)
        {
            double lineLength = 2; // length of a single dot
            double gapLength = 2; // length of the gap between dots

            double totalLength = x2 - x1;
            double currentPosition = x1;

            while (currentPosition < x2)
            {
                gfx.DrawLine(XPens.Black, currentPosition, y1, currentPosition + lineLength, y1);
                currentPosition += lineLength + gapLength;
            }
        }
    }
}

