using Recipes.Interface;
using System.Windows;
using System.Windows.Input;

namespace Recipes.View
{
    /// <summary>
    /// Interaction logic for AddWindow.xaml
    /// </summary>
    public partial class AddWindow : Window, IAddWindow
    {
        public AddWindow()
        {
            InitializeComponent();

            Owner = Application.Current.MainWindow;
        }

        public void BindDataContext(IAddWindowViewModel context)
        {
            DataContext = context;
        }

        public void ShowAlert(string text, string caption)
        {
            MessageBox.Show(text, caption);
        }

        private void ListBox_MouseDown(object sender, MouseButtonEventArgs e)
        {
            IngredientList.SelectedIndex = -1;
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}
