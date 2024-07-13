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
        private double _cellHeight;
        public SharpLibrary(string logoLoc)
        {
            font = new XFont("Verdana", 10, XFontStyle.Regular);
            titleFont = new XFont("Verdana", 16, XFontStyle.Bold);
            columnTitleFont = new XFont("Verdana", 12, XFontStyle.Bold);
            subTitleFont = new XFont("Verdana", 11, XFontStyle.Bold);
            _logo = XImage.FromFile(logoLoc);
            _separation = 40;
            _cellHeight = 30;
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
            foreach (var client in Clients)
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

            string title = "Reporte Cliente ID: " + clientid;
            yPoint = drawTitle(yPoint, gfx, page, title);
            yPoint = drawLine(gfx, page, yPoint);

            string subtitle = "Fecha: " + month.ToString("MM-yyyy");
            yPoint = drawSubTitle(yPoint, gfx, page, subtitle);

            //ClientHistory
            subtitle = "Registro Historial Cliente";
            yPoint = drawSubTitle(yPoint, gfx, page, subtitle);
            var drawndata = new List<ClientHistoryDrawnData>();
            foreach (var data in clienthistory)
                drawndata.Add(new ClientHistoryDrawnData(data));
            yPoint = drawDatawithTable(gfx, page, yPoint, drawndata);


            //ClientTasks
            subtitle = "Registro Tareas Cliente";
            yPoint = drawSubTitle(yPoint, gfx, page, subtitle);
            var drawndata2 = new List<ClientTaskDrawnData>();
            foreach (var data in clientTasks)
                drawndata2.Add(new ClientTaskDrawnData(data));
            yPoint = drawDatawithTable(gfx, page, yPoint, drawndata2);

            //ClientTasks
            subtitle = "Registro Cobros Cliente";
            yPoint = drawSubTitle(yPoint, gfx, page, subtitle);
            var drawndata3 = new List<ClientChargeDrawnData>();
            foreach (var data in clientCharges)
                drawndata3.Add(new ClientChargeDrawnData(data));
            drawDatawithTable(gfx, page, yPoint, drawndata3);

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
            if (data == null || data.Count == 0) return yPos;

            // Get the properties of the objects to determine the columns
            var properties = typeof(T).GetProperties();
            int columns = properties.Length;

            // Calculate column widths
            double pageWidth = page.Width - 2 * _separation;
            double firstColumnWidth = pageWidth * 0.1;
            double middleColumnWidth = pageWidth * 0.2;
            double lastColumnWidth = pageWidth - firstColumnWidth - (middleColumnWidth * (columns - 2));

            double[] columnWidths = new double[columns];
            columnWidths[0] = firstColumnWidth;
            columnWidths[columns - 1] = lastColumnWidth;
            for (int i = 1; i < columns - 1; i++)
            {
                columnWidths[i] = middleColumnWidth;
            }

            // Draw the table grid, including the title row
            for (int row = 0; row <= data.Count; row++) // Including title row
            {
                double yOffset = yPos + row * _cellHeight;
                gfx.DrawLine(XPens.DarkGray, _separation, yOffset, _separation + pageWidth, yOffset);
            }

            double xOffset = _separation;
            for (int col = 0; col < columns; col++)
            {
                gfx.DrawLine(XPens.DarkGray, xOffset, yPos, xOffset, yPos + (data.Count + 1) * _cellHeight); // +1 for the title row
                xOffset += columnWidths[col];
            }
            gfx.DrawLine(XPens.DarkGray, xOffset, yPos, xOffset, yPos + (data.Count + 1) * _cellHeight); // Final vertical line

            // Fill the title row
            xOffset = _separation;
            for (int col = 0; col < columns; col++)
            {
                string title = properties[col].Name;
                XRect titleRect = new XRect(xOffset, yPos, columnWidths[col], _cellHeight);
                gfx.DrawString(title, columnTitleFont, XBrushes.Black, titleRect, XStringFormats.Center);
                xOffset += columnWidths[col];
            }

            // Fill the cells with data
            for (int row = 0; row < data.Count; row++)
            {
                var item = data[row];
                xOffset = _separation;
                for (int col = 0; col < columns; col++)
                {
                    string text = properties[col].GetValue(item)?.ToString() ?? string.Empty;
                    XRect cellRect = new XRect(xOffset, yPos + (row + 1) * _cellHeight, columnWidths[col], _cellHeight);
                    gfx.DrawString(text, font, XBrushes.Black, cellRect, XStringFormats.Center);
                    xOffset += columnWidths[col];
                }
            }

            // Return the new y-position after the table
            return yPos + (data.Count + 1) * _cellHeight;
        }

    }
}

