
namespace LegalTrace.GoogleDrive.Models
{
    public class GoogleFileDTO
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string FileString { get; set; }

    }
    public class GoogleServiceAccountJson
    {
        public string JsonContent { get; }
        public bool IsFromConfig { get; }

        public string FolderId { get; }
        public GoogleServiceAccountJson(string jsonContent, string folderId, bool? isFromConfig = true)
        {
            JsonContent = jsonContent;
            IsFromConfig = (bool)isFromConfig;
            FolderId = folderId;
        }
    }
}
