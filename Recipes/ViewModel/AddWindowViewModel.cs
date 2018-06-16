using Autofac;
using Recipes.Common;
using Recipes.Interface;
using Recipes.Model;
using Recipes.Services;
using System;
using System.Collections.ObjectModel;
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
        public int SelectedIngredientList { get; set; } = -1;

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

        private string newIngName;
        public string NewIngName
        {
            get => newIngName;
            set
            {
                newIngName = value;
                base.OnPropertyChanged();
            }
        }

        private string newIngQuantity;
        public string NewIngQuantity
        {
            get => newIngQuantity;
            set
            {
                newIngQuantity = value;
                base.OnPropertyChanged();
            }
        }

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

        private Unit newSelectedUnit;
        public Unit NewSelectedUnit
        {
            get => newSelectedUnit;
            set
            {
                newSelectedUnit = value;
                base.OnPropertyChanged();
            }
        }

        public IAddWindow View { get; private set; }

        int NewReceipeId;

        DataService ds;

        //------------------------------------------------------------------------------

        public AddWindowViewModel(IAddWindow view)
        {
            View = view;
            view.BindDataContext(this);

            ds = new DataService();

            NewReceipeId = ds.GetLastIdByRecipe() + 1;

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
                                    Unit = ExcSelectedUnit.UnitName,
                                    IngredientId = SelectedIng.Id
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
                                    Unit = NewSelectedUnit.UnitName,
                                    IngredientId = ds.GetLastIdByIng()
                                });

                                NewIngName = "";
                                NewIngQuantity = "";
                                NewSelectedUnit = null;
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

        private ICommand addReceipeCom;
        public ICommand AddReceipeCom
        {
            get
            {
                if (addReceipeCom is null)
                {
                    addReceipeCom = new RelayCommand(
                        (param) =>
                        {
                            Receipe receipe = new Receipe()
                            {
                                Descrip = NewDescrip,
                                Id = NewReceipeId,
                                Note = NewNote,
                                Title = NewReceipeName,
                                PrepareTime = NewPrepareTime
                            };

                            ds.InsertReceipe(receipe);

                            foreach (var item in SelRecIngList)
                            {
                                ds.InsertRecIng(item.IngredientId, NewReceipeId, item.Quantity);
                            }

                            MessageBox("Рецепт добавлен!", "");

                            Close();
                        }, 
                        (param) =>
                        {
                            if (NewReceipeName != "" && NewPrepareTime != null && NewDescrip != "" && SelRecIngList.Count > 0)
                                return true;

                            return false;
                        });
                }
                return addReceipeCom;
            }
        }

        //------------------------------------------------------------------------------

        private ICommand cancelAddingCom;
        public ICommand CancelAddingCom
        {
            get
            {
                if (cancelAddingCom is null)
                {
                    cancelAddingCom = new RelayCommand(
                        (param) =>
                        {
                            Close();
                        });
                }

                return cancelAddingCom;
            }
        }

        //------------------------------------------------------------------------------

        void MessageBox(string text, string caption)
        {
            var msWindow = App.Container.Resolve<IAddWindow>();
            msWindow.ShowAlert(text, caption);
        }

        //------------------------------------------------------------------------------

        void Close()
        {
            var closeWin = App.Container.Resolve<IAddWindowViewModel>();
            closeWin.View.Close();
        }
    }
}
