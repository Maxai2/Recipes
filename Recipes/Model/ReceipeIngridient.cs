using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipes.Model
{
    public class ReceipeIngridient
    {
        public int ReceipeId { get; set; }
        public int IngredientId { get; set; }
        public string Ingredient { get; set; }
        public string Unit { get; set; }

        public float Quantity { get; set; }
    }
}
