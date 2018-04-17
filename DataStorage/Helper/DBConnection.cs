using System.Data.SQLite;

namespace FT.DataServiceSQLite.Helper
{
    public static class DBConnection
    {
        public static readonly SQLiteConnection connection = new SQLiteConnection("Data Source=C:\\ProgramData\\FTApp\\FTApp.sqlite;Version=3;");
    }
}
