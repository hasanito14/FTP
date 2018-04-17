using FT.DataServiceSQLite.Helper;
using FT.Helper.Helper;
using FT.Helper.Model;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Text;

namespace FT.DataServiceSQLite
{
    public class DataServices
    {
        public void GetFileInfos(IList<string> files)
        {

        }

        public bool GetFileInfo(string fileName, string fileID)
        {
            bool download = false;

            return download;

        }


        public bool SaveFileInfos(List<FileModel> files)
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
                    SaveFileInfo(file, cmd, connection);
                }

                connection.Close();
            }
            catch (Exception ex)
            {
                Logger.log.Error("Files information couldnt be saved in DB", ex);
            }

            return saved;

        }

        public bool SaveFileInfo(FileModel file, SQLiteCommand cmd, SQLiteConnection connection)
        {
            try
            {
                using (var transaction = connection.BeginTransaction())
                {
                    cmd.Parameters.AddWithValue("@Name", file.Name);
                    cmd.Parameters.AddWithValue("@FileId", file.FileID);
                    cmd.Parameters.AddWithValue("@Status", 1);
                    cmd.Parameters.AddWithValue("@LastModified", DateTime.UtcNow);
                    cmd.ExecuteNonQuery();

                    transaction.Commit();
                }
            }
            catch (Exception)
            {
                Logger.log.Error(file.FileID + ": File information couldnt be saved in DB");
            }


            return true;
        }

        private string CreateSQL(string sql)
        {
            StringBuilder sb = new StringBuilder();

            return sb.ToString();
        }

        public DataServices(string dbPath)
        {
            DBInitiate.Initiate(dbPath);


        }
        public DataServices()
        {

        }


    }
}
