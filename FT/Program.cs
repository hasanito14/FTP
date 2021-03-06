﻿using FT.SFTPUtility;
using FT.Helper.Model;

namespace FT.DataServicesSQL
{
    class Program
    {

        static void Main(string[] args)
        {
            FileModel file = new FileModel();

            //      Load the files from SFTP server
            var sftpService = new SFTPService();
            var files = sftpService.Execute();


            //      Save file name in database 

            //Using SQLite
            //var storeService = new DataServices("C:\\ProgramData\\FTPApp\\FTPApp.sqlite");
            //storeService.SaveFileInfos(files);

            //Using SQL
            var service = new DataServices();
            service.SaveFileInfos(files);

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
