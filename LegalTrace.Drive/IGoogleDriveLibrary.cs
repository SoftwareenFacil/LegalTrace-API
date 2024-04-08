using Google.Apis.Drive.v3;
using Google.Apis.Drive.v3.Data;

namespace LegalTrace.GoogleDrive
{
    public interface IGoogleDriveLibrary
    {
        Task<string> CreateFile(string fileName, MemoryStream content, string contentType, string parentFolderID = "");
        Task <bool> DeleteFile(string fileID);
        Task<string> CreateFolder(string folderName);
        Task<FileList> ListFiles();

        Task<MemoryStream> DownloadFile(string id);

        Task<string> GetFileIdByName(string fileName, string folderName);
        Task<string> GetFolderIdByName(string folderName);
        Task<bool> EditFile(string fileId, string newName, MemoryStream streamContent, string extension);

        Task<bool> CheckConnection();

        MemoryStream TransformStringToMemoryStream(string filestring);
        string TransformMemoryStreamToString(MemoryStream stream);

    }
}