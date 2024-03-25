using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Drive.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System.Net.Http;
using System.Net.Mime;


namespace LegalTrace.GoogleDrive
{
    public class GoogleDriveLibrary : IGoogleDriveLibrary
    {
        private readonly DriveService service;
        public GoogleDriveLibrary(string secret, string GoogleAppName)
        {
            service = InitializeDriveService(secret, GoogleAppName);

        }

        private DriveService InitializeDriveService(string secret, string GoogleAppName)
        {
            try
            {
                using (var stream = new FileStream(secret, FileMode.Open, FileAccess.Read))
                {
                    var credential = GoogleCredential.FromStream(stream).CreateScoped(DriveService.Scope.Drive);


                    // Create Drive API service
                    var service = new DriveService(new BaseClientService.Initializer()
                    {
                        HttpClientInitializer = credential,
                        ApplicationName = GoogleAppName,
                    });

                    return service;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new DriveService();
            }
        }

        public async Task<string> CreateFile(string fileName, MemoryStream streamContent, string contentType, string parentFolderID = "")
        {


            var fileMetadata = new Google.Apis.Drive.v3.Data.File
            {
                Name = fileName
            };

            if (parentFolderID != "")
            {
                fileMetadata.Parents = new List<string> { parentFolderID };
            }
            //else
            //{
            //    string folderName = "Archivos Generales";

            //    // Define the request for searching the folder
            //    var folderRequest = service.Files.List();
            //    folderRequest.Q = $"mimeType='application/vnd.google-apps.folder' and name='{folderName}' and trashed=false";
            //    folderRequest.Fields = "files(id, name)";

            //    // Execute the request
            //    var folderResult = await folderRequest.ExecuteAsync();

            //    // Get the ID of the folder
            //    parentFolderID = folderResult.Files[0].Id;
            //    fileMetadata.Parents = new List<string> { parentFolderID };

            //}
            streamContent.Position = 0;
            var request = service.Files.Create(fileMetadata, streamContent, contentType);
            var response = await request.UploadAsync();
            if (response.Status == Google.Apis.Upload.UploadStatus.Completed)
                return request.ResponseBody.Id;
            return "";
        }


        public async Task<bool> EditFile(string fileId, string newName, MemoryStream streamContent, string extension)
        {
            // Create a new File object with the new name.
            var newFile = new Google.Apis.Drive.v3.Data.File();
            newFile.Name = newName;

            // Create a dictionary to map extensions to MIME types.
            var mimeTypes = new Dictionary<string, string>
    {
        { ".pdf", "application/pdf" },
        { ".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" },
        { ".docx", "application/vnd.openxmlformats-officedocument.wordprocessingml.document" },
        { ".pptx", "application/vnd.openxmlformats-officedocument.presentationml.presentation" },
        { ".txt", "text/plain" },
        { ".png", "image/png" },
        { ".PNG", "image/png" },
        { ".jpeg", "image/jpeg" },
    };

            // Get the MIME type corresponding to the file extension.
            var mimeType = mimeTypes[extension];

            // Create the request to update the file.
            var request = service.Files.Update(newFile, fileId, streamContent, mimeType);

            // Execute the request.
            await request.UploadAsync();

            // If everything went well, return true.
            return true;
        }




        public async Task<string> CreateFolder(string folderName)
        {
            var folderMetadata = new Google.Apis.Drive.v3.Data.File
            {
                Name = folderName,
                MimeType = "application/vnd.google-apps.folder"
            };

            var request = service.Files.Create(folderMetadata);
            request.Fields = "id";
            try
            {
                var folder = await request.ExecuteAsync();
                return folder.Id;
            }
            catch
            {
                return null;
            }
        }

        public async Task<FileList> ListFiles()
        {
            var request = service.Files.List();
            var result = await request.ExecuteAsync();

            return result;
        }
        public async Task<bool> DeleteFile(string fileId)
        {
            try
            {
                // Intenta eliminar el archivo
                await service.Files.Delete(fileId).ExecuteAsync();
                Console.WriteLine($"Archivo con ID '{fileId}' eliminado.");
                return true;
            }
            catch (Exception e)
            {
                // Maneja el error
                Console.WriteLine($"Error al eliminar el archivo: {e.Message}");
                return false;
            }
        }


        public async Task<MemoryStream> DownloadFile(string id)
        {
            var request = service.Files.Get(id);
            var stream = new MemoryStream();
            var filestring = await request.DownloadAsync(stream);
            return stream;
        }


        public async Task<string> GetFileIdByName(string fileName, string folderId)
        {
            try
            {
                // Define los parámetros de la solicitud
                var request = service.Files.List();
                request.Q = $"name='{fileName}' and '{folderId}' in parents";
                request.Fields = "files(id, name)";

                // Ejecuta la solicitud
                var result = await request.ExecuteAsync();

                // Si el archivo se encuentra, devuelve su ID
                if (result.Files.Count > 0)
                {
                    return result.Files[0].Id;
                }

                // Si el archivo no se encuentra, devuelve null
                return null;
            
            }catch (Exception ex){
                Console.WriteLine(ex.Message);

                // Devuelve null si ocurre una excepción.
                return null;
            }
            
        }

        public async Task<string> GetFolderIdByName(string folderName)
        {

            // Define los parámetros de la solicitud
            var request = service.Files.List();
            request.Q = $"mimeType='application/vnd.google-apps.folder' and name='{folderName}'";
            request.Fields = "files(id, name)";

            // Ejecuta la solicitud
            var result = await request.ExecuteAsync();

            // Si la carpeta se encuentra, devuelve su ID
            if (result.Files.Count > 0)
            {
                return result.Files[0].Id;
            }

            // Si la carpeta no se encuentra, devuelve null
            return null;
        }


        public async Task<bool> CheckConnection()
        {
            try
            {
                // Define request parameters
                FilesResource.ListRequest listRequest = service.Files.List();
                listRequest.PageSize = 1; // We only need to retrieve one file

                // List files
                var files = await listRequest.ExecuteAsync();

                // If the code reaches this point without throwing an exception, the connection is valid
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false; // If an exception was thrown, the connection is not valid
            }
        }





    }
}