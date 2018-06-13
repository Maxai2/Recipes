using Dapper;
using Recipes.Model;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Recipes.Repository
{
    public class RecipeDupper
    {
        private IDbConnection connection = null;
        private IDbConnection Connection
        {
            get
            {
                if (connection == null || connection.State != ConnectionState.Open)
                {
                    ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings["connectionString"];

                    string _connectionString = settings.ConnectionString;

                    connection = new SqlConnection(_connectionString);
                }
                return connection;
            }
        }

        //private DbConnection _connection;
        //private DbProviderFactory _factory;

        ////------------------------------------------------------------

        //public RecipeDupper()
        //{
        //    ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings["connectionString"];

        //    _connectionString = settings.ConnectionString;

        //    _factory = DbProviderFactories.GetFactory(settings.ProviderName);
        //}

        ////------------------------------------------------------------

        //public bool OpenConnection()
        //{
        //    try
        //    {
        //        _connection = _factory.CreateConnection();
        //        _connection.ConnectionString = _connectionString;
        //        _connection.Open();
        //        return true;
        //    }
        //    catch (DbException)
        //    {
        //        return false;
        //    }
        //}
        ////----------------------------------------------------------------------
        //public void CloseConnection()
        //{
        //    if (_connection != null)
        //        _connection.Close();
        //}

        //------------------------------------------------------------

        public ICollection<Receipe> GetRexeipe()
        {
            return Connection.Query<>
        }

        //------------------------------------------------------------

    }
}
