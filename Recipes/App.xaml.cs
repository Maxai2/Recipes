using Autofac;
using Recipes.Interface;
using Recipes.View;
using Recipes.ViewModel;
using System.Windows;

namespace Recipes
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IContainer Container { get; private set; }

        public App()
        {
            var builder = new ContainerBuilder();

            #region View
            builder.RegisterType<MainWindow>().As<IMainWindow>();
            builder.RegisterType<AddWindow>().As<IAddWindow>();
            #endregion
            #region ViewModel
            builder.RegisterType<MainWindowViewModel>().As<IMainWindowViewModel>();
            builder.RegisterType<AddWindowViewModel>().As<IAddWindowViewModel>();
            #endregion

            Container = builder.Build();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var viewModel = Container.Resolve<IMainWindowViewModel>();
            var mainView = viewModel.View;
            this.MainWindow = mainView as Window;
            this.MainWindow.Show();
        }
    }
}
