using Recipes.Model;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Recipes.Interface
{
    public interface IMainWindowViewModel
    {
        ICommand AddReceipe { get; }
        ICommand RemoveReceipe { get; }
        ICommand EditReceipe { get; }

        ObservableCollection<Receipe> ReceipeList { get; set; }

        IMainWindow View { get; }
    }
}
