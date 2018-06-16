using Recipes.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Recipes.View
{
    /// <summary>
    /// Interaction logic for EditWindow.xaml
    /// </summary>
    public partial class EditWindow : Window, IEditWindow
    {
        public EditWindow()
        {
            InitializeComponent();

            Owner = Application.Current.MainWindow;
        }

        public void BindDataContext(IEditWindowViewModel context)
        {
            DataContext = context;
        }

        public void CloseAddWindow()
        {
            this.Close();
        }

        public void ShowAlert(string text, string caption)
        {
            MessageBox.Show(text, caption);
        }

        private void ListBox_MouseDown(object sender, MouseButtonEventArgs e)
        {
            IngredientList.SelectedIndex = -1;
        }
    }
}
