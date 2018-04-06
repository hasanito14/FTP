using System.Text;

namespace FTP.DataStorage
{
    public class Store
    {
        public void DownloadFiles()
        {

        }

        private string createSQL(string sql)
        {
            StringBuilder sb = new StringBuilder();

            return sb.ToString();
        }

        public Store(string dbPath)
        {
            DBInitiate.Initiate(dbPath);


        }
        public Store()
        {

        }


    }
}
