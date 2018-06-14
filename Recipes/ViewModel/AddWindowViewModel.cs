using Recipes.Interface;
using Recipes.Model;
using Recipes.Repository;
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
    public class AddWindowViewModel : IAddWindowViewModel
    {
        public string NewReceipeName { get; set; }
        public TimeSpan NewPrepareTime { get; set; }
        public string NewNote { get; set; }
        public string NewDescrip { get; set; }
        public string NewReceipeDescrip { get; set; }
        public ObservableCollection<Ingredient> IngredientList { get; set; }
        public string ExcIngQuantity { get; set; }
        public ObservableCollection<Unit> ExcUnitList { get; set; }
        public string NewIngName { get; set; }
        public string NewIngQuantity { get; set; }
        public ObservableCollection<Unit> NewUnitList { get; set; }
        public ObservableCollection<Ingredient> SelRecIngList { get; set; }

        public IAddWindow View { get; private set; }

        DataService ds;

        public AddWindowViewModel(IAddWindow view)
        {
            View = view;
            view.BindDataContext(this);

            ds = new DataService();
            
            IngredientList = new ObservableCollection<Ingredient>();

            foreach (var item in ds.GetIng())
            {
                IngredientList.Add(item);
            }
        }





        public ICommand AddIngridientCom { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }




        public ICommand AddReceipeCom { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public ICommand CancelAddingCom { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
