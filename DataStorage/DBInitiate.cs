using FTP.DataStorage.Helper;
using System;
using System.Data.SQLite;

namespace FTP.DataStorage
{
    public static class DBInitiate
    {
        public static void Initiate(string dbPath)
        {
            if (!System.IO.File.Exists(dbPath))
            {
                var connection = DBConnection.connection;

                Console.WriteLine("Just entered to create Sync DB");
                System.IO.FileInfo file = new System.IO.FileInfo(dbPath);
                file.Directory.Create(); // If the directory already exists, this method does nothing.

                SQLiteConnection.CreateFile("C:\\ProgramData\\FTPApp\\FTPApp.sqlite");
                //"C:\\ProgramData\\FTPApp\\FTPApp.sqlite"

                connection.Open();

                string sql = "CREATE TABLE IF NOT EXISTS Files " +
                    "(ID INTEGER PRIMARY KEY ASC AUTOINCREMENT," +
                    "NAME varchar(100) NOT NULL UNIQUE, " +
                    "FileId varchar(50) NOT NULL UNIQUE," +
                    "Status INT default 0," +
                    "LastModified TEXT NOT NULL)";

                SQLiteCommand command = new SQLiteCommand(sql, connection);
                command.ExecuteNonQuery();
                connection.Close();

            }
        }
    }
}

