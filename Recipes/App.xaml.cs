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
            builder.RegisterType<MainWindow>().As<IMainWindow>().SingleInstance();
            builder.RegisterType<AddWindow>().As<IAddWindow>().SingleInstance();
            builder.RegisterType<EditWindow>().As<IEditWindow>();
            #endregion
            #region ViewModel
            builder.RegisterType<MainWindowViewModel>().As<IMainWindowViewModel>().SingleInstance();
            builder.RegisterType<AddWindowViewModel>().As<IAddWindowViewModel>().SingleInstance();
            builder.RegisterType<EditWindowViewModel>().As<IEditWindowViewModel>();
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
