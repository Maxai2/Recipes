using Recipes.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Recipes.Interface
{
    public interface IAddWindowViewModel
    {
        string NewReceipeName { get; set; }

        TimeSpan NewPrepareTime { get; set; }

        string NewNote { get; set; }
        string NewDescrip { get; set; }

        ObservableCollection<Ingredient> SelRecIngList { get; set; }

        ObservableCollection<Ingredient> IngredientList { get; set; }

        string ExcIngQuantity { get; set; }

        ObservableCollection<Unit> ExcUnitList { get; set; }

        string NewIngName { get; set; }

        string NewIngQuantity { get; set; }

        ObservableCollection<Unit> NewUnitList { get; set; }

        ICommand AddIngridientCom { get; set; }
        ICommand AddReceipeCom { get; set; }
        ICommand CancelAddingCom { get; set; }

        IAddWindow View { get; }
    }
}
