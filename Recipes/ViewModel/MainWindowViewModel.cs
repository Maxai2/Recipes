using System.Collections.ObjectModel;
using System.Windows.Input;
using Autofac;
using Recipes.Common;
using Recipes.Interface;
using Recipes.Model;
using Recipes.Repository;
using Recipes.Services;

namespace Recipes.ViewModel
{
    public class MainWindowViewModel : IMainWindowViewModel
    {
        DataService ds;

        public ObservableCollection<Receipe> ReceipeList { get; set; }
        public ObservableCollection<ReceipeIngridient> ReceipeIngridientList { get; set; }
        public ObservableCollection<ReceipeIngridient> ReceipeIngridientListAll { get; set; }

        public MainWindowViewModel(IMainWindow view)
        {
            View = view;
            View.BindDataContext(this);

            ds = new DataService();

            ReceipeList = new ObservableCollection<Receipe>();

            foreach (var item in ds.GetReceipe())
            {
                ReceipeList.Add(item);
            }

            ReceipeIngridientList = new ObservableCollection<ReceipeIngridient>();

            ReceipeIngridientListAll = new ObservableCollection<ReceipeIngridient>();

            foreach (var item in ds.GetRecIng())
            {
                ReceipeIngridientListAll.Add(item);
            }
        }

        private Receipe selectedReceipe;
        public Receipe SelectedReceipe
        {
            get => selectedReceipe;
            set
            {
                if (value == null)
                    return;

                selectedReceipe = value;

                ReceipeIngridientList.Clear();
                
                foreach (var item in ReceipeIngridientListAll)
                {
                    if (selectedReceipe.Id == item.ReceipeId)
                    {
                        ReceipeIngridientList.Add(item);
                    }
                }
            }
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

        public IMainWindow View { get; private set; }
    }
}
