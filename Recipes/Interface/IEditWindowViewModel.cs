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

        string SelectedIngForUpdate { get; set; }

        ObservableCollection<string> IngredientList { get; set; }

        int SelectedIngredientListIndex { get; set; }

        string ExcIngQuantityForUpdate { get; set; }

        ObservableCollection<string> UnitList { get; set; }

        string ExcSelectedUnitForUpdate { get; set; }

        string SelectedIngForAdd { get; set; }

        string ExcIngQuantityForAdd { get; set; }

        string ExcSelectedUnitForAdd { get; set; }

        string NewIngName { get; set; }

        string NewIngQuantity { get; set; }

        string NewSelectedUnit { get; set; }

        ICommand UpdateIngCom { get; }

        ICommand AddIngridientCom { get; }
        ICommand DeleteIngridientCom { get; }
        ICommand UpdateReceipeCom { get; }
        ICommand CancelUpdateCom { get; }
    }
}
