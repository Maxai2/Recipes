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

        ICommand AddIngridientCom { get; }
        ICommand DeleteIngridientCom { get; }
        ICommand UpdateReceipeCom { get; }
        ICommand CancelUpdateCom { get; }
    }
}
