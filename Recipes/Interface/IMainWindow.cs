namespace Recipes.Interface
{
    public interface IMainWindow
    {
        void BindDataContext(IMainWindowViewModel context);
        void ShowAlert(string text, string caption);
        bool? ShowDialog();

    }
}
