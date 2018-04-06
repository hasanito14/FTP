using System.Data.SQLite;

namespace FTP.DataStorage.Helper
{
    public static class DBConnection
    {
        public static readonly SQLiteConnection connection = new SQLiteConnection("Data Source=C:\\ProgramData\\FTPApp\\FTPApp.sqlite;Version=3;");
    }
}
