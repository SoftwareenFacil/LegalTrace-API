using LegalTrace.DAL.Models;
using LegalTrace.PDF.Controllers;
using LegalTrace.PDF.Models;


namespace LegalTrace.BLL.Controllers.PdfControllers
{
    public class PDFReportsController
    {
        public PDFReportsController() { }
        public string drawClientHistoryReport(List<ClientHistoryDTO> ClientHistory, List<UserTaskDTO> clientTasks, List<ChargeDTO> clientCharges, int clientid, string TemporalFolder)
        {
            var library = new SharpLibrary();
            return library.GeneratePdfClientReport(ClientHistory, clientTasks, clientCharges, clientid, TemporalFolder);
        }

        public string drawClientswithNoMovementsReport(List<ClientDTO> Clients, string TemporalFolder)
        {
            var library = new SharpLibrary();
            return library.GeneratePdfNoMovementsReport(Clients, TemporalFolder);
        }
    }
}
