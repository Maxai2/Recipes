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
        UnitDapper unitDapper;

        //----------------------------------------------------------------------

        public DataService()
        {
            ingDapper = new IngDapper();
            recIngDapper = new RecIngDapper();
            recipeDapper = new RecipeDapper();
            unitDapper = new UnitDapper();
        }

        //----------------------------------------------------------------------

        public int GetLastIdByIng()
        {
            return ingDapper.GetLastId();
        }

        //----------------------------------------------------------------------

        public int GetLastIdByRecipe()
        {
            return recipeDapper.GetLastId();
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

        public ICollection<Unit> GetUnits()
        {
            return unitDapper.GetUnits();
        }

        //----------------------------------------------------------------------

        public void InsertIngredient(string ingredientName, int unitId)
        {
            ingDapper.InsertIngredient(ingredientName, unitId);
        }

        //----------------------------------------------------------------------

        public void InsertReceipe(Receipe receipe)
        {
            recipeDapper.InsertReceipe(receipe);
        }

        //----------------------------------------------------------------------

        public void InsertRecIng(int ingredientId, int receipeId, float quantity)
        {
            recIngDapper.InsertRecIng(ingredientId, receipeId, quantity);
        }

        //----------------------------------------------------------------------

        public void DeleteReceipe(int id)
        {
            recipeDapper.DeleteReceipe(id);
        }

        //----------------------------------------------------------------------

        public void DeleteRecIng(int receipeId)
        {
            recIngDapper.DeleteRecIng(receipeId);
        }

        //----------------------------------------------------------------------

        public int GetUnitIdByName(string unitName)
        {
            return unitDapper.GetUnitIdByName(unitName);
        }

        //----------------------------------------------------------------------

        public void UpdateReceipe(Receipe receipe)
        {
            recipeDapper.UpdateReceipe(receipe);
        }
    }
}
