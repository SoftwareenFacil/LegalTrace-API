using System.Text.RegularExpressions;

namespace LegalTrace.PDF.Models
{
    public class ClientesDrawnData
    {
        public string ID { get; set; }
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public string RUT { get; set; }

        public ClientesDrawnData(ClientDTO cliente)
        {
            ID = cliente.Id.ToString("00");
            Nombre = cliente.Name;
            Telefono = cliente.Phone.ToString("0-0000-0000");
            RUT = FormatRUT(cliente.TaxId);
        }
        public string FormatRUT(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                throw new ArgumentException("Input cannot be null or empty");

            // Remove any non-digit characters except for 'K' at the end
            input = Regex.Replace(input, @"[^\dkK]", "");

            if (input.Length < 2 || input.Length > 10)
                throw new ArgumentException("Input must have between 2 and 10 characters, including the verification digit");

            // Split into main part and check digit
            string mainPart = input.Substring(0, input.Length - 1);
            string checkDigit = input.Substring(input.Length - 1);

            // Add dots to the main part
            mainPart = Regex.Replace(mainPart, @"(\d)(?=(\d{3})+(?!\d))", "$1.");

            // Combine main part and check digit with a hyphen
            return $"{mainPart}-{checkDigit.ToUpper()}";
        }
    }
    public class ClientHistoryDrawnData
    {
        public string ID { get; set; }
        public string Fecha { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }

        public ClientHistoryDrawnData(ClientHistoryDTO clientHistory)
        {
            ID = clientHistory.Id.ToString("000");
            Fecha = clientHistory.Created.ToString("dd-MM-yyyy");
            Titulo = clientHistory.Title.Truncate(15);
            Descripcion = clientHistory.Description;
        }
    }
    public class ClientTaskDrawnData
    {
        public string ID { get; set; }
        public string Fecha { get; set; }
        public string Titulo { get; set; }

        public string Usuario { get; set; }
        public string Descripcion { get; set; }

        public ClientTaskDrawnData(UserTaskDTO userTask)
        {
            ID = userTask.Id.ToString("000");
            Fecha = userTask.Created.ToString("dd-MM-yyyy");
            Titulo = userTask.Title.Truncate(15);
            Usuario = userTask.UserId.ToString("00");
            Descripcion = userTask.Description.Truncate(20);
        }
    }
    public class ClientChargeDrawnData
    {
        public string ID { get; set; }
        public string Fecha { get; set; }
        public string Titulo { get; set; }
        public string Monto { get; set; }
        public string Descripcion { get; set; }

        public ClientChargeDrawnData(ChargeDTO clientCharge)
        {
            ID = clientCharge.Id.ToString("000");
            Fecha = clientCharge.Created.ToString("dd-MM-yyyy");
            Titulo = clientCharge.Title.Truncate(15);
            Monto = clientCharge.Amount.ToString("$##,#00,000");
            Descripcion = clientCharge.Description.Truncate(20);
        }
    }
    public static class StringExt
    {
        public static string Truncate(this string value, int maxLength)
        {
            if (string.IsNullOrEmpty(value)) return value;
            return value.Length <= maxLength ? value : value.Substring(0, maxLength);
        }
    }

}
