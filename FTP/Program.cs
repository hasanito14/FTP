using FTP.DataStorage;
using FTP.SFTP;
namespace FTP
{
    class Program
    {

        static void Main(string[] args)
        {
            /* FOR GOOGLE DRIVE------
             * 
            var service = DriveServices.GetAuthenticaticated("credentials/client_secret.json", "KalamKhan.1260");

            var storeService = new StoreService("C:\\ProgramData\\FTPApp\\FTPApp.sqlite");

            var files = DriveServices.GetFiles(service, 20);

            storeService.SaveFiles(files);

            if (files != null && files.Count > 0)
            {
                foreach (var file in files)
                {
                    Console.WriteLine("{0} ({1})", file.Name, file.Id);
                    storeService.DownloadFile(file.Name, file.Id);

                }
            }
            else
            {
                Console.WriteLine("No files found.");
            }
            Console.Read();
            
             */

            //      Load the files from SFTP server
            var sftpService = new SFTPService();
            var files = sftpService.Execute();


            //      Save file name in database 
            var storeService = new StoreService("C:\\ProgramData\\FTPApp\\FTPApp.sqlite");
            storeService.SaveFiles(files);

            //      Get the filesname from DB
            //----
            //

            //      Download the file
            var result = sftpService.ExecuteDownLoad(files);

            //      Change the status in DB


            //      Create admin dash info
        }
    }
}
