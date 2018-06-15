using Recipes.Interface;
using System.Windows;

namespace Recipes.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IMainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public void BindDataContext(IMainWindowViewModel context)
        {
            DataContext = context;
        }

        public void ShowAlert(string text, string caption)
        {
            MessageBox.Show(text, caption);
        }

        private void RecipeList_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            RecipeList.SelectedIndex = -1;
        }
    }
}