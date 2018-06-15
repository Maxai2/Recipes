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

        ObservableCollection<ReceipeIngridient> SelRecIngList { get; set; }

        bool ExecRadButVal { get; set; }

        ObservableCollection<Ingredient> IngredientList { get; set; }

        int SelectedIngredientList { get; set; }

        Ingredient SelectedIng { get; set; }

        string ExcIngQuantity { get; set; }

        ObservableCollection<Unit> ExcUnitList { get; set; }

        Unit ExcSelectedUnit { get; set; }

        string NewIngName { get; set; }

        string NewIngQuantity { get; set; }

        ObservableCollection<Unit> NewUnitList { get; set; }

        Unit NewSelectedUnit { get; set; }

        ICommand AddIngridientCom { get; }
        ICommand DeleteIngridientCom { get; }

        ICommand AddReceipeCom { get; set; }
        ICommand CancelAddingCom { get; set; }

        IAddWindow View { get; }
    }
}
