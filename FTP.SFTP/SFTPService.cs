using FTP.Helper.Model;
using Renci.SshNet;
using System.Collections.Generic;
using System.Configuration;
using System.IO;

namespace FTP.SFTP
{
    public class SFTPService
    {
        private string _userName { get; set; }
        private string _password { get; set; }
        private string _SFTPServer { get; set; }
        private string _workingDirectory { get; set; }
        private string _extension { get; set; }
        private string _lcoalDirectory { get; set; }

        public SFTPService()
        {
            this._userName = ConfigurationManager.AppSettings["SFTPuserName"];
            this._password = ConfigurationManager.AppSettings["SFTPPassword"];
            this._SFTPServer = ConfigurationManager.AppSettings["SFTPServer"];
            this._workingDirectory = ConfigurationManager.AppSettings["SFTPWorkingDirectory"];
            this._extension = ConfigurationManager.AppSettings["SFTPExtension"];
            this._lcoalDirectory = ConfigurationManager.AppSettings["LocalDirectory"];

        }
        public SFTPService(string username, string password, string SFTPServer, string Directory, string ext, string LocalDirectory)
        {
            this._userName = username;
            this._password = password;
            this._SFTPServer = SFTPServer;
            this._workingDirectory = Directory;
            this._extension = ext;
            this._lcoalDirectory = LocalDirectory;
        }

        public List<FileModel> Execute()
        {
            List<FileModel> files = new List<FileModel>();
            var connectionInfo = new ConnectionInfo(_SFTPServer,
                                            _userName,
                                            new PasswordAuthenticationMethod(_userName, _password),
                                            new PrivateKeyAuthenticationMethod("rsa.key"));

            using (var client = new SftpClient(connectionInfo))
            {
                client.Connect();
                client.ChangeDirectory(_workingDirectory);
                files = SFTPListFiles(client, _extension);
                client.Disconnect();

            }

            return files;

        }

        private List<FileModel> SFTPListFiles(SftpClient client, string ext)
        {
            List<FileModel> files = new List<FileModel>();

            var SFTPFiles = client.ListDirectory(client.WorkingDirectory);

            foreach (var file in SFTPFiles)
            {
                var FileExt = Path.GetExtension(file.Name);

                if (!file.IsDirectory && ((string.IsNullOrEmpty(ext)) || (!string.IsNullOrEmpty(ext) && FileExt == ext)))
                {
                    files.Add(new FileModel { Name = file.Name, Extenstion = FileExt });
                }

            }
            return files;
        }

        public int ExecuteDownLoad(List<FileModel> files)
        {
            int result = 0;

            var connectionInfo = new ConnectionInfo(_SFTPServer,
                                            _userName,
                                            new PasswordAuthenticationMethod(_userName, _password),
                                            new PrivateKeyAuthenticationMethod("rsa.key"));

            using (var client = new SftpClient(connectionInfo))
            {
                client.Connect();
                client.ChangeDirectory(_workingDirectory);
                result = DownloadFiles(client, files);
                client.Disconnect();

            }

            return result;

        }

        private int DownloadFiles(SftpClient client, List<FileModel> files)
        {
            int count = 0;
            System.IO.FileInfo dir = new System.IO.FileInfo(_lcoalDirectory);
            dir.Directory.Create();

            foreach (var file in files)
            {
                if (DownloadFile(file, client))
                    count++;

            }
            return count;
        }

        private bool DownloadFile(FileModel file, SftpClient client)
        {
            bool result = false;

            using (Stream fileStream = File.OpenWrite(_lcoalDirectory + file.Name))
            {
                client.DownloadFile(client.WorkingDirectory + "/" + file.Name, fileStream);
                result = true;
            }

            return result;
        }

    }
}
