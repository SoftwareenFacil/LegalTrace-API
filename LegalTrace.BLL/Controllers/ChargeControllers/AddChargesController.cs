using LegalTrace.BLL.Models.ChargeDTO;
using LegalTrace.DAL.Context;
using LegalTrace.DAL.Controllers.ChargeControllers;
using LegalTrace.GoogleDrive;
using LegalTrace.DAL.Models;
using LegalTrace.GoogleDrive;
using Microsoft.VisualBasic;
using System.Text;

namespace LegalTrace.BLL.Controllers.ChargeControllers
{
    public class AddChargesController
    {
        private AppDbContext _context;
        private IGoogleDriveLibrary _library;
        public AddChargesController(AppDbContext _dbContext, string secretDriveLoc, string GoogleAppName)
        {
            _context = _dbContext;
            _library = new GoogleDriveLibrary(secretDriveLoc, GoogleAppName);
        }
        public async Task<int> AddCharge(ChargeInsertDTO charge)
        {
            string FileLink = "";
            if (charge.fileString != null)                
                FileLink = await _library.CreateFile(charge.fileName, _library.TransformStringToMemoryStream(charge.fileString), charge.fileType);

            if (!string.IsNullOrEmpty(charge.Title) && !string.IsNullOrEmpty(charge.Description) && !string.IsNullOrEmpty(FileLink) && charge.Amount > 0)
            {
                var chargeController = new ChargeController(_context);
                DateTime utcNow = DateTime.UtcNow;
                var chargeCreate = new Charge()
                {
                    ClientId = charge.ClientId,
                    Title = charge.Title,
                    Description = charge.Description,
                    Date = DateTime.SpecifyKind(charge.Date, DateTimeKind.Utc),
                    Amount = charge.Amount,
                    FileLink = FileLink,
                    ChargeType = ((int)charge.chargeType >= 3) ? 0 : charge.chargeType,
                    Created = DateTime.SpecifyKind(utcNow, DateTimeKind.Utc),
                    Updated = DateTime.SpecifyKind(utcNow, DateTimeKind.Utc)
                };
                return await chargeController.InsertCharge(chargeCreate);
            }
            return 0;
        }
    }
}
