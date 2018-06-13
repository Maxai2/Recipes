using System.Collections.ObjectModel;
using System.Windows.Input;
using Autofac;
using Recipes.Common;
using Recipes.Interface;
using Recipes.Model;

namespace Recipes.ViewModel
{
    public class MainWindowViewModel : IMainWindowViewModel
    {
        public MainWindowViewModel(IMainWindow view)
        {
            View = view;
            View.BindDataContext(this);
        }

        public ICommand addReceipe;
        public ICommand AddReceipe
        {
            get
            {
                if (addReceipe is null)
                {
                    addReceipe = new RelayCommand(
                        (param) =>
                    {
                        var addVm = App.Container.Resolve<IAddWindowViewModel>();
                        addVm.View.ShowDialog();
                    });
                }

                return addReceipe;
            }
        }

        public ICommand RemoveReceipe => throw new System.NotImplementedException();

        public ICommand EditReceipe => throw new System.NotImplementedException();

        public ObservableCollection<Receipe> ReceipeList { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

        public IMainWindow View { get; private set; }
    }
}
