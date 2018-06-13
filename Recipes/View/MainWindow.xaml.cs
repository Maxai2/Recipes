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


    }
}
