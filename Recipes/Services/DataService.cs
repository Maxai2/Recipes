using Recipes.Model;
using Recipes.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipes.Services
{
    public class DataService
    {
        IngDapper ingDapper;
        RecIngDapper recIngDapper;
        RecipeDapper recipeDapper;

        //----------------------------------------------------------------------

        public DataService()
        {
            ingDapper = new IngDapper();
            recIngDapper = new RecIngDapper();
            recipeDapper = new RecipeDapper();
        }

        //----------------------------------------------------------------------

        public ICollection<ReceipeIngridient> GetRecIng()
        {
            return recIngDapper.GetRecIng();
        }

        //----------------------------------------------------------------------

        public ICollection<Ingredient> GetIng()
        {
            return ingDapper.GetIng();
        }

        //----------------------------------------------------------------------

        public ICollection<Receipe> GetReceipe()
        {
            return recipeDapper.GetReceipe();
        }

        //----------------------------------------------------------------------

    }
}
