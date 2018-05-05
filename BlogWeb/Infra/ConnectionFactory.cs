using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace BlogWeb.Infra
{
    public class ConnectionFactory
    {

        public static IDbConnection CriaConexao()
        {
            string stringConexao = ConfigurationManager.ConnectionStrings["blog"].ConnectionString;
            IDbConnection connection = new SqlConnection(stringConexao);
            connection.Open();
            return connection;
        }

    }
}