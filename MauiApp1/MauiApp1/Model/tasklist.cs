using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;


namespace MauiApp1
{
    public class tasklist: INotifyPropertyChanged
    {
        public tasklist()
        {

        }
        private int _id { get; set; }
        private string _taskname { get; set; }
        private string _description { get; set; }

        public int id
        {
            get { return _id; }
            set { _id = value; OnPropertyChanged(nameof(id)); }
        }
        public string taskname
        {
            get { return _taskname; }
            set { _taskname = value; OnPropertyChanged(nameof(taskname)); }
        }
        public string description
        {
            get { return _description; }
            set { _description = value; OnPropertyChanged(nameof(description)); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
