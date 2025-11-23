using LegalTrace.BLL.Models.ChargeDTO;
using LegalTrace.DAL.Context;
using LegalTrace.DAL.Models;
using LegalTrace.DAL.Repository;
using LegalTrace.GoogleDrive;
using LegalTrace.GoogleDrive.Models;
using LegalTrace.PDF.Models;

namespace LegalTrace.BLL.Controllers
{
    public class ChargesController
    {
        private AppDbContext _context;
        private GoogleServiceAccountJson _googleServiceAccountJson;
        private string _googleAppName;
        public ChargesController(AppDbContext _dbContext, GoogleServiceAccountJson serviceAccountJson, string GoogleAppName)
        {
            _context = _dbContext;
            _googleServiceAccountJson = serviceAccountJson;
            _googleAppName = GoogleAppName;
        }
        public async Task<List<ChargeDTO>> GetChargeBy(int? id, int? clientId, DateTime? date, DateTime? dateTo, string? title, int? amount, int? type)
        {
            var chargeController = new ChargeRepository(_context);
            double? lowerLimit = null;
            double? upperLimit = null;
            if (amount.HasValue)
            {
                upperLimit = amount.Value + 1000;
                lowerLimit = amount.Value - 1000;
            }
            var charges = await chargeController.GetChargeBy(id, clientId, date, dateTo, title, type, lowerLimit, upperLimit);
            if (charges.Count() > 0)
            {
                List<ChargeDTO> result = new List<ChargeDTO>();
                charges.ForEach(row => result.Add(MapFromEntity(row)));
                return result;
            }

            return new List<ChargeDTO>();
        }

        public async Task<ChargeDTO?> GetChargeById(int id)
        {
            var chargeController = new ChargeRepository(_context);
            var charge = await chargeController.GetChargeById(id);
            if (charge != null)
                return MapFromEntity(charge);
            return null;
        }
        public async Task<int> UpdateCharge(ChargeEditDTO chargeEdited)
        {
            if (chargeEdited.Amount <= 0 && string.IsNullOrWhiteSpace(chargeEdited.Title) && string.IsNullOrWhiteSpace(chargeEdited.Description)
                && chargeEdited.Amount == 0 && chargeEdited.ClientId == 0)
                return 400;
            var (isFileBeingUploaded, isFileProperlyUploaded) = isFileBeingUploadedProperly(chargeEdited);
            if (!isFileProperlyUploaded)
                return 400;
            var chargeController = new ChargeRepository(_context);
            var charge = await chargeController.GetChargeById(chargeEdited.Id);
            if (charge != null)
            {
                if (chargeEdited.ClientId > 0)
                {
                    var clientController = new ClientRepository(_context);
                    var client = await clientController.GetClientById(chargeEdited.ClientId);
                    if (client == null)
                        return -1;
                    charge.ClientId = chargeEdited.ClientId;
                }

                if (chargeEdited.chargeType != null)
                    charge.ChargeType = (ChargeType)((int)chargeEdited.chargeType >= 3 ? 0 : chargeEdited.chargeType);

                //validate Correct uploading of file when editing it
                var isUploadSuccesful = true;
                if (isFileBeingUploaded && !string.IsNullOrEmpty(charge.FileLink))
                {
                    var library = new GoogleDriveLibrary(_googleServiceAccountJson, _googleAppName);
                    isUploadSuccesful = await library.EditFile(charge.FileLink, chargeEdited.fileName, library.TransformStringToMemoryStream(chargeEdited.fileString), chargeEdited.fileType);
                }
                else if(isFileBeingUploaded)
                {
                    var library = new GoogleDriveLibrary(_googleServiceAccountJson, _googleAppName);
                    charge.FileLink = await library.CreateFile(chargeEdited.fileName, library.TransformStringToMemoryStream(chargeEdited.fileString), chargeEdited.fileType, _googleServiceAccountJson.FolderId);
                    if (string.IsNullOrEmpty(charge.FileLink))
                        isUploadSuccesful = false;
                }
                if (!isUploadSuccesful)
                    return 500;

                charge.Title = !string.IsNullOrEmpty(chargeEdited.Title) ? chargeEdited.Title : charge.Title;
                charge.Description = !string.IsNullOrEmpty(chargeEdited.Description) ? chargeEdited.Description : charge.Description;
                charge.PaymentDate = chargeEdited.PaymentDate.HasValue ? chargeEdited.PaymentDate.Value : charge.PaymentDate;
                charge.Amount = chargeEdited.Amount > 0 ? chargeEdited.Amount : charge.Amount;
                charge.Updated = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc);

                var isUpdated = await chargeController.UpdateCharge(charge);
                if (!isUpdated)
                    return 400;
                return 200;
            }
            return 404;
        }

        private (bool, bool) isFileBeingUploadedProperly(ChargeEditDTO chargeEdited)
        {
            if (string.IsNullOrEmpty(chargeEdited.fileString) && (string.IsNullOrWhiteSpace(chargeEdited.fileName) && string.IsNullOrEmpty(chargeEdited.fileType)))
                return (false, true);
            if (string.IsNullOrWhiteSpace(chargeEdited.fileName) || string.IsNullOrEmpty(chargeEdited.fileType) || string.IsNullOrEmpty(chargeEdited.fileString))
                return (true, false);
            return (true, true);
        }
        public async Task<bool> DeleteChargeById(int id)
        {
            var chargeController = new ChargeRepository(_context);
            var exist = await chargeController.GetChargeById(id);
            if (exist == null)
            {
                return false;
            }

            return await chargeController.DeleteCharge(id);
            //TODO: remove file from google drive
        }
        public async Task<int> AddCharge(ChargeInsertDTO charge)
        {
            string FileLink = "";
            if (charge.fileString != null)
            {
                var library = new GoogleDriveLibrary(_googleServiceAccountJson, _googleAppName);
                FileLink = await library.CreateFile(charge.fileName, library.TransformStringToMemoryStream(charge.fileString), charge.fileType, _googleServiceAccountJson.FolderId);
            }
            if ((string.IsNullOrEmpty(FileLink) && charge.fileString != null))
                return 401;
            if (!string.IsNullOrEmpty(charge.Title) && !string.IsNullOrEmpty(charge.Description) &&  charge.Amount > 0)
            {
                var chargeController = new ChargeRepository(_context);

                return await chargeController.InsertCharge(MapFromInsertDTO(charge, FileLink));
            }
            return 0;
        }


        private Charge MapFromInsertDTO(ChargeInsertDTO insertDTO, string fileLink)
        {
            var chargeCreate = new Charge()
            {
                ClientId = insertDTO.ClientId,
                Title = insertDTO.Title,
                Description = insertDTO.Description,
                PaymentDate = insertDTO.PaymentDate,
                Amount = insertDTO.Amount,
                FileLink = fileLink,
                ChargeType = (int)insertDTO.chargeType >= 3 ? 0 : insertDTO.chargeType,
                Created = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc),
                Updated = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc)
            };
            return chargeCreate;
        }

        private ChargeDTO MapFromEntity(Charge entity)
        {
            var DTO = new ChargeDTO()
            {
                Id = entity.Id,
                ClientId = entity.ClientId,
                Title = entity.Title,
                Date = entity.PaymentDate,
                Type = entity.ChargeType.ToString(),
                Description = entity.Description,
                Amount = entity.Amount,
                Created = entity.Created,
                Updated = entity.Updated,
                FileLink = entity.FileLink
            };
            return DTO;
        }
    }
}
