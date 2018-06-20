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
//------------------------------------------------------------------------------
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

            UnitList = new ObservableCollection<string>();

            foreach (var item in ds.GetUnits())
            {
                UnitList.Add(item.UnitName);
            }
        }
        //------------------------------------------------------------------------------
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

        private string excIngQuantityForUpdate;
        public string ExcIngQuantityForUpdate
        {
            get => excIngQuantityForUpdate;
            set
            {
                excIngQuantityForUpdate = value;
                base.OnPropertyChanged();
            }
        }

        private string excSelectedUnitForUpdate;
        public string ExcSelectedUnitForUpdate
        {
            get => excSelectedUnitForUpdate;
            set
            {
                excSelectedUnitForUpdate = value;
                base.OnPropertyChanged();
            }
        }

        private ReceipeIngridient selectedIngredientListItem;
        public ReceipeIngridient SelectedIngredientListItem
        {
            get => selectedIngredientListItem;
            set
            {
                if (value == null)
                    return;

                selectedIngredientListItem = value;

                SelectedIngForUpdate = selectedIngredientListItem.Ingredient;
                ExcIngQuantityForUpdate = selectedIngredientListItem.Quantity.ToString();
                ExcSelectedUnitForUpdate = selectedIngredientListItem.Unit;

                base.OnPropertyChanged();
            }
        }

        public ObservableCollection<string> IngredientList { get; set; }

        public ObservableCollection<string> UnitList { get; set; }

        //------------------------------------------------------------------------------
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
                            var deleteId = RecIngList.FirstOrDefault(f => f.IngredientId == SelectedIngredientListItem.IngredientId).IngredientId;

                            RecIngList.RemoveAt(deleteId);

                            base.OnPropertyChanged();

                            ReceipeIngridient temp = new ReceipeIngridient()
                            {
                                Ingredient = SelectedIngForUpdate,
                                Quantity = Convert.ToSingle(ExcIngQuantityForUpdate),
                                Unit = ExcSelectedUnitForUpdate
                            };

                            RecIngList.Add(temp);

                            SelectedIngForUpdate = null;
                            ExcIngQuantityForUpdate = "";
                            ExcSelectedUnitForUpdate = null;
                        });
                }
                return updateIngCom;
            }
        }
        //------------------------------------------------------------------------------

        private int selectedIngredientListIndex;
        public int SelectedIngredientListIndex
        {
            get => selectedIngredientListIndex;
            set
            {
                selectedIngredientListIndex = value;
                base.OnPropertyChanged();
            }
        }


        private string selectedIngForAdd;
        public string SelectedIngForAdd
        {
            get => selectedIngForAdd;
            set
            {
                selectedIngForAdd = value;
                base.OnPropertyChanged();
            }
        }

        private string excIngQuantityForAdd;
        public string ExcIngQuantityForAdd
        {
            get => excIngQuantityForAdd;
            set
            {
                excIngQuantityForAdd = value;
                base.OnPropertyChanged();
            }
        }

        private string excSelectedUnitForAdd;
        public string ExcSelectedUnitForAdd
        {
            get => excSelectedUnitForAdd;
            set
            {
                excSelectedUnitForAdd = value;
                base.OnPropertyChanged();
            }
        }

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

        private string newSelectedUnit;
        public string NewSelectedUnit
        {
            get => newSelectedUnit;
            set
            {
                newSelectedUnit = value;
                base.OnPropertyChanged();
            }
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
                                foreach (var item in RecIngList)
                                {
                                    if (item.Ingredient == SelectedIngForAdd)
                                    {
                                        return;
                                    }
                                }

                                RecIngList.Add(new ReceipeIngridient
                                {
                                    Ingredient = SelectedIngForAdd,
                                    Quantity = Convert.ToSingle(ExcIngQuantityForAdd),
                                    Unit = ExcSelectedUnitForAdd
                                });

                                SelectedIngForAdd = null;
                                ExcIngQuantityForAdd = "";
                                ExcSelectedUnitForAdd = null;
                            }
                            else
                            {
                                foreach (var item in RecIngList)
                                {
                                    if (item.Ingredient == NewIngName)
                                    {
                                        return;
                                    }
                                }

                                foreach (var item in IngredientList)
                                {
                                    if (item == NewIngName)
                                    {
                                        MessageBox($"'{NewIngName}' такой ингридиент существует, выберите из существующих.", "");
                                        return;
                                    }
                                }

                                var UnitId = ds.GetUnitIdByName(NewSelectedUnit);

                                ds.InsertIngredient(NewIngName, UnitId);

                                IngredientList.Add(NewIngName);

                                RecIngList.Add(new ReceipeIngridient
                                {
                                    Ingredient = NewIngName,
                                    Quantity = Convert.ToSingle(NewIngQuantity),
                                    Unit = NewSelectedUnit
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
                                if (SelectedIngForAdd != null && ExcIngQuantityForAdd != "" && ExcSelectedUnitForAdd != null)
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
                            var deleteId = RecIngList.FirstOrDefault(f => f.IngredientId == SelectedIngredientListItem.IngredientId).IngredientId;

                            RecIngList.RemoveAt(deleteId);
                        },
                        (param) =>
                        {
                            if (SelectedIngredientListIndex == -1)
                                return false;
                            else
                                return true;
                        });
                }
                return deleteIngridientCom;
            }
        }

        //------------------------------------------------------------------------------

        private ICommand updateReceipeCom;
        public ICommand UpdateReceipeCom
        {
            get
            {
                if (updateReceipeCom is null)
                {
                    updateReceipeCom = new RelayCommand(
                        (param) =>
                        {
                            ds.UpdateReceipe(Receipe);

                            ds.DeleteRecIng(Receipe.Id);

                            foreach (var item in RecIngList)
                            {
                                ds.InsertRecIng(item.IngredientId, Receipe.Id, item.Quantity);
                            }

                            MessageBox("Рецепт обновлен!", "");

                            Close();
                        },
                        (param) =>
                        {
                            if (Receipe.Title != "" && Receipe.PrepareTime != null && Receipe.Descrip != "" && RecIngList.Count > 0)
                                return true;

                            return false;
                        });
                }

                return updateReceipeCom;
            }
        }

        //------------------------------------------------------------------------------

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

        //------------------------------------------------------------------------------
        void MessageBox(string text, string caption)
        {
            this.View.ShowAlert(text, caption);
        }
        //------------------------------------------------------------------------------
        void Close()
        {
            this.View.Hide();
        }
    }
}
//------------------------------------------------------------------------------