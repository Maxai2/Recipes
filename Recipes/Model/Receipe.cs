using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipes.Model
{
    public class Receipe
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public TimeSpan PrepareTime { get; set; }
        public string Descrip { get; set; }
        public string Note { get; set; }
    }
}
