using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipes.Model
{
    public class Ingredient
    {
        public int Id { get; set; }
        public string IngredientName { get; set; }
        public Unit Unit { get; set; }
    }
}
