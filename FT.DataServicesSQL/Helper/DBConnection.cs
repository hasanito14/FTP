using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace FT.DataServicesSQL.Helper
{
    public static class DBConnection
    {
        public static readonly IDbConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["TestFTP"].ConnectionString);
    }
}
