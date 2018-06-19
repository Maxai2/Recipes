using Autofac;
using Recipes.Common;
using Recipes.Interface;
using Recipes.Model;
using Recipes.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Recipes.ViewModel
{
    public class EditWindowViewModel : NotifiableObject, IEditWindowViewModel
    {
        public EditWindowViewModel(IEditWindow view)
        {
            View = view;
            View.BindDataContext(this);

            ds = new DataService();

            var MainRes = App.Container.Resolve<IMainWindowViewModel>();

            Receipe = MainRes.SelectedReceipe;

            RecIngList = new ObservableCollection<ReceipeIngridient>();

            foreach (var item in MainRes.ReceipeIngridientList)
            {
                RecIngList.Add(item);
            }

            IngredientList = new ObservableCollection<string>();

            foreach (var item in ds.GetIng())
            {
                IngredientList.Add(item.IngredientName);
            }
        }

        DataService ds;

        public IEditWindow View { get; private set; }

        public Receipe Receipe { get; set; }

        public bool ExecRadButVal { get; set; } = true;

        public ObservableCollection<ReceipeIngridient> RecIngList { get; set; }

        private string selectedIngForUpdate;
        public string SelectedIngForUpdate
        {
            get => selectedIngForUpdate;
            set
            {
                selectedIngForUpdate = value;
                base.OnPropertyChanged();
            }
        }

        private ReceipeIngridient selectedIngredientListItem;
        public ReceipeIngridient SelectedIngredientListItem
        {
            get => selectedIngredientListItem;
            set
            {
                selectedIngredientListItem = value;

                selectedIngForUpdate = IngredientList.FirstOrDefault(f => f == selectedIngredientListItem.Ingredient);

                base.OnPropertyChanged();
            }
        }

        public ObservableCollection<string> IngredientList { get; set; }



        private ICommand updateIngCom;
        public ICommand UpdateIngCom
        {
            get
            {

                if (updateIngCom is null)
                {
                    updateIngCom = new RelayCommand(
                        (param) =>
                        {

                        });
                }

                return updateIngCom;
            }
        }

        public ICommand AddIngridientCom => throw new NotImplementedException();

        public ICommand DeleteIngridientCom => throw new NotImplementedException();

        public ICommand UpdateReceipeCom => throw new NotImplementedException();


        private ICommand cancelUpdateCom;
        public ICommand CancelUpdateCom
        {
            get
            {
                if (cancelUpdateCom is null)
                {
                    cancelUpdateCom = new RelayCommand(
                        (param) =>
                        {
                            Close();
                        });
                }

                return cancelUpdateCom;
            }
        }


        void MessageBox(string text, string caption)
        {
            var MsWin = App.Container.Resolve<IEditWindowViewModel>();
            MsWin.View.ShowAlert(text, caption);
        }

        void Close()
        {
            App.Container.Resolve<IEditWindowViewModel>().View.Hide();
        }
    }
}
