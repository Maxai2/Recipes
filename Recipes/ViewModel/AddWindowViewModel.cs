using Autofac;
using Recipes.Common;
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

//------------------------------------------------------------------------------
namespace Recipes.ViewModel

{
    public class AddWindowViewModel : NotifiableObject, IAddWindowViewModel
    {
        public string NewReceipeName { get; set; }
        public TimeSpan NewPrepareTime { get; set; }
        public string NewNote { get; set; }
        public string NewDescrip { get; set; }
        public string NewReceipeDescrip { get; set; }
        public ObservableCollection<Ingredient> IngredientList { get; set; }
        public int SelectedIngredientList { get; set; }

        private string excIngQuantity;
        public string ExcIngQuantity
        {
            get => excIngQuantity;
            set
            {
                excIngQuantity = value;
                base.OnPropertyChanged();
            }
        }

        public ObservableCollection<Unit> ExcUnitList { get; set; }
        public string NewIngName { get; set; }
        public string NewIngQuantity { get; set; }
        public ObservableCollection<Unit> NewUnitList { get; set; }
        public ObservableCollection<ReceipeIngridient> SelRecIngList { get; set; }
        public bool ExecRadButVal { get; set; } = true;

        private Ingredient selectedIng;
        public Ingredient SelectedIng
        {
            get => selectedIng;
            set
            {
                selectedIng = value;
                base.OnPropertyChanged();
            }
        }

        private Unit excSelectedUnit;
        public Unit ExcSelectedUnit
        {
            get => excSelectedUnit;
            set
            {
                excSelectedUnit = value;
                base.OnPropertyChanged();
            }
        }

        public Unit NewSelectedUnit { get; set; }

        public IAddWindow View { get; private set; }

        DataService ds;

        //------------------------------------------------------------------------------

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

            ExcUnitList = new ObservableCollection<Unit>();

            foreach (var item in ds.GetUnits())
            {
                ExcUnitList.Add(item);
            }

            NewUnitList = new ObservableCollection<Unit>();

            foreach (var item in ds.GetUnits())
            {
                NewUnitList.Add(item);
            }

            SelRecIngList = new ObservableCollection<ReceipeIngridient>();
        }

        //------------------------------------------------------------------------------

        public ICommand addIngridientCom;
        public ICommand AddIngridientCom
        {
            get
            {
                if (addIngridientCom is null)
                {
                    addIngridientCom = new RelayCommand(
                        (param) =>
                        {
                            if (ExecRadButVal)
                            {
                                foreach (var item in SelRecIngList)
                                {
                                    if (item.Ingredient == SelectedIng.IngredientName)
                                    {
                                        return;
                                    }
                                }

                                SelRecIngList.Add(new ReceipeIngridient
                                {
                                    Ingredient = SelectedIng.IngredientName,
                                    Quantity = Convert.ToSingle(ExcIngQuantity),
                                    Unit = ExcSelectedUnit.UnitName
                                });

                                SelectedIng = null;
                                ExcIngQuantity = "";
                                ExcSelectedUnit = null;
                            }
                            else
                            {
                                foreach (var item in SelRecIngList)
                                {
                                    if (item.Ingredient == NewIngName)
                                    {
                                        return;
                                    }
                                }

                                foreach (var item in IngredientList)
                                {
                                    if (item.IngredientName == NewIngName)
                                    {
                                        MessageBox($"'{NewIngName}' такой ингридиент существует, выберите из существующих.", "");
                                        return;
                                    }
                                }

                                ds.InsertIngredient(NewIngName, NewSelectedUnit.Id);

                                IngredientList.Add(new Ingredient() { Id = ds.GetLastIdByIng() + 1, IngredientName = NewIngName, Unit = NewSelectedUnit.UnitName });

                                SelRecIngList.Add(new ReceipeIngridient
                                {
                                    Ingredient = NewIngName,
                                    Quantity = Convert.ToSingle(NewIngQuantity),
                                    Unit = NewSelectedUnit.UnitName
                                });

                                //NewIngName = "";
                                //NewIngQuantity = "";
                                //NewSelectedUnit = null;
                            }
                        },
                        (param) =>
                        {
                            if (ExecRadButVal)
                            {
                                if (SelectedIng != null && ExcIngQuantity != "" && ExcSelectedUnit != null)
                                    return true;
                                else
                                    return false;
                            }
                            else
                            {
                                if (NewIngName != "" && NewIngQuantity != "" && NewSelectedUnit != null)
                                    return true;
                                else
                                    return false;
                            }
                        });
                }

                return addIngridientCom;
            }
        }

        //------------------------------------------------------------------------------

        public ICommand deleteIngridientCom;
        public ICommand DeleteIngridientCom
        {
            get
            {
                if (deleteIngridientCom is null)
                {
                    deleteIngridientCom = new RelayCommand(
                        (param) =>
                        {
                            SelRecIngList.RemoveAt(SelectedIngredientList);
                        },
                        (param) =>
                        {
                            if (SelectedIngredientList == -1)
                                return false;
                            else
                                return true;
                        });
                }
                return deleteIngridientCom;
            }
        }

        //------------------------------------------------------------------------------

        public ICommand AddReceipeCom { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        //------------------------------------------------------------------------------

        public ICommand CancelAddingCom { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        //------------------------------------------------------------------------------

        void MessageBox(string text, string caption)
        {
            var window = App.Container.Resolve<IAddWindow>();
            window.ShowAlert(text, caption);
        }

    }
}
