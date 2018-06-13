using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipes.Interface
{
    public interface IAddWindow
    {
        void BindDataContext(IAddWindowViewModel context);
        bool? ShowDialog();
    }
}
