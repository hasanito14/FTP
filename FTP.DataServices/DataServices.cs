using Dapper;
using FT.Helper.Helper;
using FT.Helper.Model;
using FTP.DataServicesSQL.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace FTP.DataServicesSQL
{
    public class DataServices
    {
        public DateTime LastModified = DateTime.UtcNow;
        public DateTime CreatedOn = DateTime.UtcNow;

        public DataServices()
        {

        }

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

            string sql = @"INSERT INTO[FileInfo](ID,FileID, Name, Extention,CreatedOn,LastModified,Status,Size)VALUES(@ID,@FileID,@Name,@Extention, @CreatedOn, @LastModified,@Status,@Size);";

            IDbConnection db = DBConnection.connection;

            try
            {
                db.Open();

                foreach (var file in files)
                {
                    file.CreatedOn = CreatedOn;
                    file.LastModified = LastModified;
                    file.ID = Guid.NewGuid();
                    SaveFileInfo(file, sql, db);
                }

                db.Close();
            }
            catch (Exception ex)
            {
                Logger.log.Error("Files information couldnt be saved in DB", ex);
            }

            return saved;

        }

        public bool SaveFileInfo(FileModel file, string sql, IDbConnection connection)
        {
            try
            {
                using (var transaction = connection.BeginTransaction())
                {
                    connection.Execute(sql, new
                    {
                        file.ID,
                        file.FileID,
                        file.Name,
                        file.Extention,
                        file.CreatedOn,
                        file.LastModified,
                        file.Status,
                        file.Size
                    }, transaction: transaction);
                    transaction.Commit();
                }
            }
            catch (Exception ex)
            {
                Logger.log.Error(file.Name + ": File information couldnt be saved in DB" + '\n', ex);
            }

            return true;
        }

        private string CreateSQL(FileModel file)
        {
            StringBuilder sb = new StringBuilder();

            return sb.ToString();
        }

    }
}
