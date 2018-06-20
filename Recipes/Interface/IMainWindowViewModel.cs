using Recipes.Model;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Input;

namespace Recipes.Interface
{
    public interface IMainWindowViewModel
    {
        ICommand AddReceipe { get; }
        ICommand RemoveReceipe { get; }
        ICommand EditReceipe { get; }

        ObservableCollection<Receipe> ReceipeList { get; set; }

        ObservableCollection<ReceipeIngridient> ReceipeIngridientList { get; set; }

        ObservableCollection<ReceipeIngridient> ReceipeIngridientListAll { get; set; }

        Receipe SelectedReceipe { get; set; }

        ObservableCollection<string> OrderList { get; set; }

        string SelOrder { get; set; }

        IMainWindow View { get; }
    }
}
