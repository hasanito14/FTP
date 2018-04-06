using FTP.DataStorage.Helper;
using FTP.Helper.Helper;
using FTP.Helper.Model;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Text;

namespace FTP.DataStorage
{
    public class StoreService
    {
        public void DownloadFiles(IList<string> files)
        {

        }

        public bool DownloadFile(string fileName, string fileID)
        {
            bool download = false;

            return download;

        }

        public bool SaveFiles(List<FileModel> files)
        {
            bool saved = false;

            string sql = @"INSERT INTO [Files] ([Name],[FileId],[Status],[LastModified]) VALUES (@Name, @FileId, @Status, @LastModified);";
            var connection = DBConnection.connection;
            try
            {
                SQLiteCommand cmd = new SQLiteCommand(sql);
                cmd.Connection = connection;
                connection.Open();

                foreach (var file in files)
                {
                    SaveFile(file, cmd, connection);
                }

                connection.Close();
            }
            catch (Exception ex)
            {
                Logger.log.Error("Files information couldnt be saved in DB", ex);
            }

            return saved;

        }

        public bool SaveFile(FileModel file, SQLiteCommand cmd, SQLiteConnection connection)
        {
            try
            {
                using (var transaction = connection.BeginTransaction())
                {
                    cmd.Parameters.AddWithValue("@Name", file.Name);
                    cmd.Parameters.AddWithValue("@FileId", file.Id);
                    cmd.Parameters.AddWithValue("@Status", 1);
                    cmd.Parameters.AddWithValue("@LastModified", DateTime.UtcNow);
                    cmd.ExecuteNonQuery();

                    transaction.Commit();
                }
            }
            catch (Exception ex)
            {
                Logger.log.Error(file.Id + ": File information couldnt be saved in DB");
            }


            return true;
        }

        private string CreateSQL(string sql)
        {
            StringBuilder sb = new StringBuilder();

            return sb.ToString();
        }

        public StoreService(string dbPath)
        {
            DBInitiate.Initiate(dbPath);


        }
        public StoreService()
        {

        }


    }
}
