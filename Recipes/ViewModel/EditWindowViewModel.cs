using Recipes.Interface;
using Recipes.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipes.ViewModel
{
    public class EditWindowViewModel : IEditWindowViewModel
    {
        public IEditWindow View { get; private set; }
        public Receipe receipe { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public EditWindowViewModel(IEditWindow view)
        {
            View = view;
            View.BindDataContext(this);
        }
    }
}
