using Recipes.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipes.ViewModel
{
    public class AddWindowViewModel : IAddWindowViewModel
    {
        public AddWindowViewModel(IAddWindow view)
        {
            View = view;
            view.BindDataContext(this);
        }

        public IAddWindow View { get; private set; }
    }
}
