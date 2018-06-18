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
        DataService ds;

        public IEditWindow View { get; private set; }

        public Receipe Receipe { get; set; }

        public bool ExecRadButVal { get; set; } = true;

        public ObservableCollection<ReceipeIngridient> RecIngList { get; set; }

        private ReceipeIngridient selectedIngredientListItem;
        public ReceipeIngridient SelectedIngredientListItem
        {
            get => selectedIngredientListItem;
            set
            {
                selectedIngredientListItem = value;
                selectedIngForUpdate = IngredientList.FirstOrDefault(f => f.IngredientName == selectedIngredientListItem.Ingredient);

                //selectedIngForUpdate.IngredientName = selectedIngredientListItem.Ingredient;
                //sece

                base.OnPropertyChanged();
            }
        }

        public ObservableCollection<Ingredient> IngredientList { get; set; }

        private Ingredient selectedIngForUpdate;
        public Ingredient SelectedIngForUpdate
        {
            get => selectedIngForUpdate;
            set
            {
                selectedIngForUpdate = value;
                base.OnPropertyChanged();
            }
        }


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

            selectedIngForUpdate = new Ingredient();

            selectedIngredientListItem = new ReceipeIngridient();

            IngredientList = new ObservableCollection<Ingredient>();

            foreach (var item in ds.GetIng())
            {
                IngredientList.Add(item);
            }
        }

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

        public Ingredient SelectedIng { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string ExcIngQuantity { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public ObservableCollection<Unit> ExcUnitList { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Unit ExcSelectedUnit { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string NewIngName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string NewIngQuantity { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public ObservableCollection<Unit> NewUnitList { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Unit NewSelectedUnit { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public ICommand AddIngridientCom => throw new NotImplementedException();

        public ICommand DeleteIngridientCom => throw new NotImplementedException();

        public ICommand UpdateReceipeCom => throw new NotImplementedException();

        public ICommand CancelUpdateCom => throw new NotImplementedException();


        void MessageBox(string text, string caption)
        {
            var MsWin = App.Container.Resolve<IEditWindowViewModel>();
            MsWin.View.ShowAlert(text, caption);
        }

    }
}
