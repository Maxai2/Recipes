using Recipes.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipes.Interface
{
    public interface IEditWindowViewModel
    {
        IEditWindow View { get; }

        Receipe receipe { get; set; }
    }
}
