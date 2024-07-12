using LegalTrace.DAL.Models;
using LegalTrace.PDF.Controllers;
using LegalTrace.PDF.Models;


namespace LegalTrace.BLL.Controllers.PdfControllers
{
    public class PDFReportsController
    {
        private string _logoLoc;
        public PDFReportsController(string logoLoc)
        {
            _logoLoc = logoLoc;
        }
        public MemoryStream drawClientHistoryReport(List<ClientHistoryDTO> ClientHistory, List<UserTaskDTO> clientTasks, List<ChargeDTO> clientCharges, int clientid, DateTime month)
        {
            var library = new SharpLibrary(_logoLoc);
            return library.GeneratePdfClientReport(ClientHistory, clientTasks, clientCharges, clientid, month);
        }

        public MemoryStream drawClientswithNoMovementsReport(List<ClientDTO> Clients)
        {
            var library = new SharpLibrary(_logoLoc);
            return library.GeneratePdfNoMovementsReport(Clients);
        }
    }
}
