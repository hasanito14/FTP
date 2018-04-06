using FTP.Helper.Helper;
using FTP.Helper.Model;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace FTP.GoogleDriveService
{
    public static class DriveServices
    {
        public static DriveService GetAuthenticaticated(string clientSecret, string userName)
        {
            if (clientSecret == null)
            {
                Logger.log.Error("CLient Secret File is missing");
            }
            else if (userName == null)
            {
                Logger.log.Error("UserName is missing");
            }

            string[] Scopes = { DriveService.Scope.DriveReadonly };
            string ApplicationName = "Drive API .NET Quickstart";

            try
            {
                UserCredential credential;

                using (var stream =
                new FileStream(clientSecret, FileMode.Open, FileAccess.Read))
                {
                    string credPath = System.Environment.GetFolderPath(
                        System.Environment.SpecialFolder.Personal);
                    credPath = Path.Combine(credPath, ".credentials/drive-dotnet-quickstart.json");

                    credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                        GoogleClientSecrets.Load(stream).Secrets,
                        Scopes,
                        userName,
                        CancellationToken.None,
                        new FileDataStore(credPath, true)).Result;

                    Logger.log.Info("Credential file saved to: " + credPath);

                }
                return new DriveService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = credential,
                    ApplicationName = ApplicationName,
                });
            }
            catch (Exception ex)
            {

                Logger.log.Error("Drive Authentiacation Failed:" + ex);
                throw new Exception("Drive Authentiacation Failed:", ex);
            }

        }

        /*
        public static IList<Google.Apis.Drive.v3.Data.File> GetFiles(DriveService service, int pageSize)
        {

            FilesResource.ListRequest listRequest = service.Files.List();
            listRequest.PageSize = pageSize;
            listRequest.Fields = "nextPageToken, files(id, name)";
            IList<Google.Apis.Drive.v3.Data.File> files = listRequest.Execute().Files;

            return files;
        }*/

        public static List<FileModel> GetFiles(DriveService service, int pageSize)
        {
            List<FileModel> files = new List<FileModel>();
            FilesResource.ListRequest listRequest = service.Files.List();
            listRequest.PageSize = pageSize;
            listRequest.Fields = "nextPageToken, files(id, name)";
            IList<Google.Apis.Drive.v3.Data.File> gfiles = listRequest.Execute().Files;

            if (gfiles != null && gfiles.Count > 0)
            {
                foreach (var gfile in gfiles)
                {
                    FileModel file = new FileModel { Id = gfile.Id, Name = gfile.Name };

                    files.Add(file);
                }
            }

            return files;
        }


    }
}
