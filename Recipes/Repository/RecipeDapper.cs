using Dapper;
using Dapper.FastCrud;
using Recipes.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;

namespace Recipes.Repository
{
    public class RecipeDapper
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
            return Connection.Query<int>("SELECT MAX(Id) FROM Receipes").Single();
        }

        //------------------------------------------------------------

        public ICollection<Receipe> GetReceipe()
        {
            return Connection.Query<Receipe>("SELECT * FROM Receipes").AsList();
        }

        //------------------------------------------------------------

        public void InsertReceipe(Receipe receipe)
        {
            resetIdentity("Receipes", GetLastId());

            Connection.Query<Receipe>("INSERT INTO Receipes(Descrip, Note, PrepareTime, Title) VALUES(@Descrip, @Note, @PrepareTime, @Title)", new { Descrip = receipe.Descrip, Note = receipe.Note, PrepareTime = receipe.PrepareTime, Title = receipe.Title});
        }

        //------------------------------------------------------------

        public void DeleteReceipe(int id)
        {
            Connection.Query("DELETE FROM Receipes WHERE Id = @Id", new { Id = id });
        }

        //------------------------------------------------------------

        public Receipe GetReceipeById(int id)
        {
            return Connection.Get<Receipe>(new Receipe { Id = id });
        }

        //------------------------------------------------------------

        public void UpdateReceipe(int id)
        {
            Connection.Update<Receipe>(new Receipe { Id = id });
        }

        //------------------------------------------------------------

    }
}
