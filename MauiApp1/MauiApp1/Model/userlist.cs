using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;


namespace MauiApp1
{
    public class userlist: INotifyPropertyChanged
    {
        public userlist()
        {

        }
        private int _id { get; set; }
        private string _username { get; set; }
        private string _email{ get; set; }
        private string _password { get; set; }

        public int id
        {
            get { return _id; }
            set { _id = value; OnPropertyChanged(nameof(id)); }
        }
        public string username
        {
            get { return _username; }
            set { _username = value; OnPropertyChanged(nameof(username)); }
        }
        public string email
        {
            get { return _email; }
            set { _email = value; OnPropertyChanged(nameof(email)); }
        }
        
        public string password
        {
            get { return _password; }
            set { _password = value; OnPropertyChanged(nameof(password)); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}