using Recipes.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipes.Model
{
    public class Ingredient : NotifiableObject
    {
        public int Id { get; set; }

        private string ingredientName;
        public string IngredientName
        {
            get => ingredientName;
            set
            {
                ingredientName = value;
                base.OnPropertyChanged();
            }
        }

        public string Unit { get; set; }
    }
}
