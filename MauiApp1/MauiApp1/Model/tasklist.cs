using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;
using Newtonsoft.Json;


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
        
        private string _status { get; set; }
        
        private int _userId { get; set; }
        
        [JsonProperty("item_id")]
        public int id
        {
            get { return _id; }
            set { _id = value; OnPropertyChanged(nameof(id)); }
        }
        
        [JsonProperty("item_name")]
        public string taskname
        {
            get { return _taskname; }
            set { _taskname = value; OnPropertyChanged(nameof(taskname)); }
        }
        
        [JsonProperty("item_description")]
        public string description
        {
            get { return _description; }
            set { _description = value; OnPropertyChanged(nameof(description)); }
        }
        
        [JsonProperty("status")]
        public string status
        {
            get { return _status; }
            set { _status = value; OnPropertyChanged(nameof(status)); }
        }
        
        [JsonPropertyName("user_id")]
        public int userId
        {
            get { return _userId; }
            set { _userId = value; OnPropertyChanged(nameof(userId)); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
