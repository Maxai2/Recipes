using Dapper;
using Recipes.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipes.Repository
{
    public class IngDapper
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

        //------------------------------------------------------------

        public ICollection<Ingredient> GetIng()
        {
            return Connection.Query<Ingredient>("SELECT Ingredients.Id, Ingredient AS IngredientName, Units.Unit FROM Ingredients JOIN Units ON Ingredients.UnitId = Units.Id").AsList();
        }

        //------------------------------------------------------------

        //public Receipe GetReceipeById(int id)
        //{
        //    return Connection.Get<Receipe>(new Receipe { Id = id });
        //}

        ////------------------------------------------------------------

        //public void DeleteReceipe(int id)
        //{
        //    Connection.Delete<Receipe>(new Receipe { Id = id });
        //}

        ////------------------------------------------------------------

        //public void UpdateReceipe(int id)
        //{
        //    Connection.Update<Receipe>(new Receipe { Id = id });
        //}

        ////------------------------------------------------------------

        //public void InsertReceipe(Receipe receipe)
        //{
        //    Connection.Insert<Receipe>(receipe);
        //}

        ////------------------------------------------------------------

    }
}
