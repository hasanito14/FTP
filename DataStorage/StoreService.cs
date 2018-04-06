using FTP.DataStorage.Helper;
using GoogleDriveDownloader.Model;
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
                using (var transaction = connection.BeginTransaction())
                {

                    foreach (var file in files)
                    {
                        cmd.Parameters.AddWithValue("@Name", file.Name);
                        cmd.Parameters.AddWithValue("@FileId", file.Id);
                        cmd.Parameters.AddWithValue("@Status", 1);
                        cmd.Parameters.AddWithValue("@LastModified", DateTime.UtcNow);
                        cmd.ExecuteNonQuery();
                    }

                    transaction.Commit();
                    saved = true;
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                ;
            }

            return saved;

        }
        /*
         * 
         *                     "(ID INTEGER PRIMARY KEY ASC AUTOINCREMENT," +
                    "NAME varchar(100) NOT NULL, " +
                    "FileId varchar(50) NOT NULL," +
                    "Status INT default 0," +
                    "LastModified TEXT NOT NULL)";
                    static int AddUsers(IEnumerable<User> users)
{
    var results = new List<int>();
    string sqlInsertUsers = @"INSERT INTO [Users] ([Name]) VALUES (@Name);";
  
    using (var cn = new SQLiteConnection(_connectionString))
    {
        cn.Open();
        using(var transaction = cn.BeginTransaction())
        {
            using (var cmd = cn.CreateCommand())
            {
               cmd.CommandText = sqlInsertUsers;
               cmd.Parameters.AddWithValue("@Name", "UserName");
  
               foreach (var user in users)
               {
                   cmd.Parameters["@Name"] = user.Name;
                   results.Add(cmd.ExecuteNonQuery());
               }
            }
            transaction.Commit();
        }
    }
    return results.Sum();
}
*/


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
