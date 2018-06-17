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
    public interface IEditWindowViewModel
    {
        IEditWindow View { get; }

        Receipe Receipe { get; set; }

        ObservableCollection<ReceipeIngridient> RecIngList { get; set; }

        ReceipeIngridient SelectedIngredientListItem { get; set; }

        bool ExecRadButVal { get; set; }

        ObservableCollection<Ingredient>  IngredientList { get; set; }

        Ingredient SelectedIng { get; set; }

        string ExcIngQuantity { get; set; }

        ObservableCollection<Unit>  ExcUnitList { get; set; }

        Unit ExcSelectedUnit { get; set; }

        string NewIngName { get; set; }

        string NewIngQuantity { get; set; }

        ObservableCollection<Unit> NewUnitList { get; set; }

        Unit NewSelectedUnit { get; set; }

        ICommand AddIngridientCom { get; }

        ICommand DeleteIngridientCom { get; }

        ICommand UpdateReceipeCom { get; }

        ICommand CancelUpdateCom { get; }
    }
}
