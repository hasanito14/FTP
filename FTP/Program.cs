﻿using FTP.DataStorage;
using FTP.GoogleDriveDownloader;
using System;
namespace FTP
{
    class Program
    {

        static void Main(string[] args)
        {


            var service = DriveServices.GetAuthenticaticated("credentials/client_secret.json", "KalamKhan.1260");
            var files = DriveServices.GetFiles(service, 20);

            if (files != null && files.Count > 0)
            {
                foreach (var file in files)
                {
                    Console.WriteLine("{0} ({1})", file.Name, file.Id);
                }
            }
            else
            {
                Console.WriteLine("No files found.");
            }

            var storage = new Store("C:\\ProgramData\\FTPApp\\FTPApp.sqlite");

            Console.Read();
        }
    }
}
