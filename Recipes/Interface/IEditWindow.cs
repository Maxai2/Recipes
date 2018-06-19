using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipes.Interface
{
    public interface IEditWindow
    {
        void BindDataContext(IEditWindowViewModel context);
        void ShowAlert(string text, string caption);
        void Hide();
        bool? ShowDialog();
    }
}
