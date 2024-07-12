using LegalTrace.PDF.Models;
using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;


namespace LegalTrace.PDF.Controllers
{
    public class SharpLibrary
    {
        XFont font;
        XFont titleFont;
        XFont columnTitleFont;
        XFont subTitleFont;
        XImage _logo;
        double _separation;
        public SharpLibrary(string logoLoc)
        {
            font = new XFont("Verdana", 11, XFontStyle.Regular);
            titleFont = new XFont("Verdana", 16, XFontStyle.Bold);
            columnTitleFont = new XFont("Verdana", 12, XFontStyle.Bold);
            subTitleFont = new XFont("Verdana", 11, XFontStyle.Bold);
            _logo = XImage.FromFile(logoLoc);
            _separation = 40;
        }
        public MemoryStream GeneratePdfNoMovementsReport(List<ClientDTO> Clients)
        {
            var retStream = new MemoryStream();

            var document = InitializePDF("Reporte Clientes");
            // Create an empty page
            PdfPage page = document.AddPage();
            XGraphics gfx = XGraphics.FromPdfPage(page);

            double yPoint = drawLogo(gfx, page);

            string title = "Reporte Clientes Sin Movimientos";
            yPoint = drawTitle(yPoint, gfx, page, title);

            yPoint = drawLine(gfx, page, yPoint);

            string subtitle = "Fecha:" + DateTime.Now.ToString("dd-mm-yyyy");
            yPoint = drawSubTitle(yPoint, gfx, page, subtitle);

            //Pasar lista a formato de lo que se dibujara
            List<ClientesDrawnData> drawndata = new List<ClientesDrawnData>();
            foreach(var client in  Clients)
                drawndata.Add(new ClientesDrawnData(client));
            

            yPoint = drawDatawithTable(gfx, page, yPoint, drawndata);

            document.Save(retStream, false);
            return retStream;
        }
        public MemoryStream GeneratePdfClientReport(List<ClientHistoryDTO> clienthistory, List<UserTaskDTO> clientTasks, List<ChargeDTO> clientCharges, int clientid, DateTime month)
        {
            var retStream = new MemoryStream();

            var document = InitializePDF("Reporte Cliente");
            // Create an empty page
            PdfPage page = document.AddPage();
            XGraphics gfx = XGraphics.FromPdfPage(page);
            double yPoint = drawLogo(gfx, page);

            string title = "Reporte Cliente ID: " + clientid + " - Fecha: " + month.ToString("MM-yyyy");
            yPoint = drawTitle(yPoint, gfx, page, title);

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

            document.Save(retStream, false);
            return retStream;
        }
        private PdfDocument InitializePDF(string title)
        {
            PdfDocument document = new PdfDocument();
            document.Info.Title = title;
            return document;
        }
        private double drawTitle(double yPoint, XGraphics gfx, PdfPage page, string title)
        {
            // Draw the Title
            gfx.DrawString(title, titleFont, XBrushes.Gray, new XRect(20, yPoint, page.Width, page.Height), XStringFormats.TopLeft);
            return yPoint;
        }
        private double drawLogo(XGraphics gfx, PdfPage page)
        {
            // Calcular el tamaño del logo
            double pageWidth = page.Width;
            double logoWidth = pageWidth / 6; // Aproximadamente 1/6 del ancho de la página
            double scaleFactor = logoWidth / _logo.PixelWidth;
            double logoHeight = _logo.PixelHeight * scaleFactor;

            // Calcular la posición para centrar el logo horizontalmente
            double xPosition = (pageWidth - logoWidth) / 2;

            // Dibujar la imagen en el PDF
            gfx.DrawImage(_logo, xPosition, _separation, logoWidth, logoHeight);
            return (2 * _separation) + logoHeight;
        }

        private double drawSubTitle(double yPoint, XGraphics gfx, PdfPage page, string subtitle)
        {
            gfx.DrawString(subtitle, subTitleFont, XBrushes.Gray, new XRect(20, yPoint, page.Width, page.Height), XStringFormats.TopLeft);
            return yPoint + (_separation / 2);
        }

        private double drawLine(XGraphics gfx, PdfPage page, double yPos)
        {
            // Calculate margins
            double margin = page.Width * 0.05;

            // Calculate start and end points for the line
            double startX = margin;
            double endX = page.Width - margin;

            // Draw the line
            gfx.DrawLine(XPens.DarkGray, startX, yPos + _separation / 2, endX, yPos + _separation / 2);

            return yPos + (_separation / 2);
        }
        private double drawDatawithTable<T>(XGraphics gfx, PdfPage page, double yPos, List<T> data)
        {
            if (data == null || data.Count == 0) throw new ArgumentException("Data list cannot be null or empty");

            // Get the properties of the objects to determine the columns
            var properties = typeof(T).GetProperties();
            int columns = properties.Length;
            double cellWidth = (page.Width - 2 * _separation) / columns;
            double cellHeight = 40;

            // Draw the table grid, including the title row
            for (int row = 0; row <= data.Count; row++) // Including title row
            {
                double yOffset = yPos + row * cellHeight;
                gfx.DrawLine(XPens.DarkGray, _separation, yOffset, _separation + columns * cellWidth, yOffset);
            }
            // Draw the bottom line of the table
            gfx.DrawLine(XPens.DarkGray, _separation, yPos + (data.Count + 1) * cellHeight, _separation + columns * cellWidth, yPos + (data.Count + 1) * cellHeight);

            for (int col = 0; col <= columns; col++)
            {
                double xOffset = _separation + col * cellWidth;
                gfx.DrawLine(XPens.DarkGray, xOffset, yPos, xOffset, yPos + (data.Count + 1) * cellHeight); // +1 for the title row
            }

            // Fill the title row
            for (int col = 0; col < columns; col++)
            {
                string title = properties[col].Name;
                XRect titleRect = new XRect(_separation + col * cellWidth, yPos, cellWidth, cellHeight);
                gfx.DrawString(title, columnTitleFont, XBrushes.Black, titleRect, XStringFormats.Center);
            }

            // Fill the cells with data
            for (int row = 0; row < data.Count; row++)
            {
                var item = data[row];
                for (int col = 0; col < columns; col++)
                {
                    string text = properties[col].GetValue(item)?.ToString() ?? string.Empty;
                    XRect cellRect = new XRect(_separation + col * cellWidth, yPos + (row + 1) * cellHeight, cellWidth, cellHeight);
                    gfx.DrawString(text, font, XBrushes.Black, cellRect, XStringFormats.Center);
                }
            }

            // Return the new y-position after the table
            return yPos + (data.Count + 1) * cellHeight;
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

        private class ClientesDrawnData {

            public string ID {get; set;}
            public string Nombre { get; set;}
            public string Telefono { get; set;}
            public string RUT { get; set;}
            
            public ClientesDrawnData(ClientDTO cliente)
            {
                ID = cliente.Id.ToString("00");
                Nombre = cliente.Name;
                Telefono = cliente.Phone.ToString("0-0000-0000");
                RUT = cliente.TaxId;
            }
        }
    }
}

