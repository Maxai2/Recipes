using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using Dapper.FastCrud;
using Recipes.Model;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipes.Repository
{
    public class RecIngDapper
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

        void resetIdentity(string tableName, int resetId)
        {
            Connection.Query("DBCC CHECKIDENT(@TableName, RESEED, @ResetId)", new { TableName = tableName, ResetId = resetId });
        }

        //------------------------------------------------------------

        public int GetLastId()
        {
            return Connection.Query<int>("SELECT MAX(Id) FROM Ingredients").Single();
        }

        //------------------------------------------------------------

        public ICollection<ReceipeIngridient> GetRecIng()
        {
            return Connection.Query<ReceipeIngridient>("SELECT ReceipeId, Ingredients.Ingredient, Quantity, Units.Unit FROM ReceipesIngredients JOIN Ingredients ON ReceipesIngredients.IngredientId = Ingredients.Id JOIN Units ON Ingredients.UnitId = Units.Id").AsList();
        }

        //------------------------------------------------------------

        public void InsertRecIng(int ingredientId, int receipeId, float quantity)
        {
            resetIdentity("ReceipesIngredients", GetLastId());

            Connection.Query("INSERT INTO ReceipesIngredients(IngredientId, ReceipeId, Quantity) VALUES(@IngredientId, @ReceipeId, @Quantity)", new { IngredientId = ingredientId, ReceipeId = receipeId, Quantity = quantity });
        }

        //------------------------------------------------------------

        public void DeleteRecIng(int receipeId)
        {
            Connection.Query("DELETE FROM ReceipesIngredients WHERE ReceipeId = @ReceipeId", new { ReceipeId = receipeId });
        }

        //------------------------------------------------------------

    }
}
