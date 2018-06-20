using System.Collections;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Autofac;
using Recipes.Common;
using Recipes.Interface;
using Recipes.Model;
using Recipes.Repository;
using Recipes.Services;

namespace Recipes.ViewModel
{
    public class MainWindowViewModel : NotifiableObject, IMainWindowViewModel
    {
        DataService ds;

        public ObservableCollection<Receipe> ReceipeList { get; set; }
        public ObservableCollection<ReceipeIngridient> ReceipeIngridientList { get; set; }
        public ObservableCollection<ReceipeIngridient> ReceipeIngridientListAll { get; set; }
        public ObservableCollection<string> OrderList { get; set; }


        public MainWindowViewModel(IMainWindow view)
        {
            View = view;
            View.BindDataContext(this);

            ds = new DataService();

            OrderList = new ObservableCollection<string>() { "По алфавиту", "Против алфавита", "По времени", "Против времени" };

            ReceipeList = new ObservableCollection<Receipe>();

            SelOrder = "По алфавиту";

            ReceipeIngridientList = new ObservableCollection<ReceipeIngridient>();

            ReceipeIngridientListAll = new ObservableCollection<ReceipeIngridient>();

            foreach (var item in ds.GetRecIng())
            {
                ReceipeIngridientListAll.Add(item);
            }
        }

        private string selOrder;
        public string SelOrder
        {
            get => selOrder;
            set
            {
                selOrder = value;

                ReceipeList.Clear();

                GetReceipeWithModif(selOrder);

                base.OnPropertyChanged();
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

                base.OnPropertyChanged();
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

                        ReceipeList.Clear();

                        GetReceipeWithModif(SelOrder);

                        ReceipeIngridientListAll.Clear();

                        foreach (var item in ds.GetRecIng())
                        {
                            ReceipeIngridientListAll.Add(item);
                        }
                    });
                }

                return addReceipe;
            }
        }

        private ICommand removeReceipe;
        public ICommand RemoveReceipe
        {
            get
            {
                if (removeReceipe is null)
                {
                    removeReceipe = new RelayCommand(
                        (param) =>
                        {
                            ds.DeleteRecIng(selectedReceipe.Id);

                            ds.DeleteReceipe(selectedReceipe.Id);

                            ReceipeList.Clear();

                            foreach (var item in ds.GetReceipe())
                            {
                                ReceipeList.Add(item);
                            }

                            ReceipeIngridientListAll.Clear();

                            foreach (var item in ds.GetRecIng())
                            {
                                ReceipeIngridientListAll.Add(item);
                            }
                        });
                }

                return removeReceipe;
            }
        }

        private ICommand editReceipe;
        public ICommand EditReceipe
        {
            get
            {
                if (editReceipe is null)
                {
                    editReceipe = new RelayCommand(
                        (param) =>
                        {
                            var editVm = App.Container.Resolve<IEditWindowViewModel>();
                            editVm.View.ShowDialog();
                        });
                }

                return editReceipe;
            }
        }

        public IMainWindow View { get; private set; }

        void MessageBox(string text, string caption)
        {
            var msWindow = App.Container.Resolve<IMainWindow>();
            msWindow.ShowAlert(text, caption);
        }

        void GetReceipeWithModif(string searchMode)
        {
            //foreach (var item in ds.GetReceipe())
            //{
            //    Receipe receipe = new Receipe();

            //    receipe.Id = item.Id;
            //    receipe.Note = item.Note;
            //    receipe.PrepareTime = item.PrepareTime;
            //    receipe.Title = item.Title;
            //    item.Descrip = item.Descrip.Replace("\\n\r\n", "\n\r");

            //    ReceipeList.Add(item);
            //}

            switch (searchMode)
            {
                case "По алфавиту":
                    {
                        foreach (var item in ds.GetReceipe().OrderBy(f => f.Title))
                        {
                            Receipe receipe = new Receipe();

                            receipe.Id = item.Id;
                            receipe.Note = item.Note;
                            receipe.PrepareTime = item.PrepareTime;
                            receipe.Title = item.Title;
                            item.Descrip = item.Descrip.Replace("\\n\r\n", "\n\r");

                            ReceipeList.Add(item);
                        }

                        break;
                    }
                case "Против алфавита":
                    {
                        foreach (var item in ds.GetReceipe().OrderByDescending(f => f.Title))
                        {
                            Receipe receipe = new Receipe();

                            receipe.Id = item.Id;
                            receipe.Note = item.Note;
                            receipe.PrepareTime = item.PrepareTime;
                            receipe.Title = item.Title;
                            item.Descrip = item.Descrip.Replace("\\n\r\n", "\n\r");

                            ReceipeList.Add(item);
                        }

                        break;
                    }
                case "По времени":
                    {
                        foreach (var item in ds.GetReceipe().OrderBy(f => f.PrepareTime))
                        {
                            Receipe receipe = new Receipe();

                            receipe.Id = item.Id;
                            receipe.Note = item.Note;
                            receipe.PrepareTime = item.PrepareTime;
                            receipe.Title = item.Title;
                            item.Descrip = item.Descrip.Replace("\\n\r\n", "\n\r");

                            ReceipeList.Add(item);
                        }

                        break;
                    }
                case "Против времени":
                    {
                        foreach (var item in ds.GetReceipe().OrderByDescending(f => f.PrepareTime))
                        {
                            Receipe receipe = new Receipe();

                            receipe.Id = item.Id;
                            receipe.Note = item.Note;
                            receipe.PrepareTime = item.PrepareTime;
                            receipe.Title = item.Title;
                            item.Descrip = item.Descrip.Replace("\\n\r\n", "\n\r");

                            ReceipeList.Add(item);
                        }

                        break;
                    }
            }
        }
    }
}
