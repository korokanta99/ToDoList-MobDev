public class TodoItem
{
    public int item_id { get; set; }
    public string item_name { get; set; }
    public string item_description { get; set; }
    public string status { get; set; }
    public int user_id { get; set; }
    public DateTime timemodified { get; set; } // optional, but good to have for sorting/history
}
