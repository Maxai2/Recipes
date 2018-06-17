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

        public ReceipeIngridient SelectedIngredientListItem { get; set; }

        public ObservableCollection<Ingredient> IngredientList { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

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
