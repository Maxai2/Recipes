using Recipes.Common;
using System;

namespace Recipes.Model
{
    public class Receipe : NotifiableObject
    {
        private int id;
        public int Id
        {
            get { return id; }
            set { id = value; base.OnPropertyChanged(); }
        }

        private string title;
        public string Title
        {
            get { return title; }
            set { title = value; base.OnPropertyChanged(); }
        }
        
        private TimeSpan prepareTime;
        public TimeSpan PrepareTime
        {
            get { return prepareTime; }
            set { prepareTime = value; base.OnPropertyChanged(); }
        }

        private string descrip;
        public string Descrip
        {
            get { return descrip; }
            set { descrip = value; base.OnPropertyChanged(); }
        }

        private string note;
        public string Note
        {
            get { return note; }
            set { note = value; base.OnPropertyChanged(); }
        }
    }
}
