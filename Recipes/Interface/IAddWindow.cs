namespace Recipes.Interface
{
    public interface IAddWindow
    {
        void BindDataContext(IAddWindowViewModel context);
        void ShowAlert(string text, string caption);
        void Hide();
        bool? ShowDialog();
    }
}
