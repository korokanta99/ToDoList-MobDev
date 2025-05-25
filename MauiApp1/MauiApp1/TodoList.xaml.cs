using MauiApp1.Models;
using MauiApp1.Services;
using System.Text.Json;
using MauiApp1;


namespace MauiApp1
{
    public partial class TodoList : ContentPage
    {
        private readonly ApiService _apiService;
        private readonly int _userId;

        public TodoList(int userId)
        {
            InitializeComponent();
            _userId = userId;
            _apiService = new ApiService();

            LoadTasks(); // Fetch tasks on load
        }

        private async void LoadTasks()
        {
            string jsonResponse = await _apiService.GetTasksAsync("active", _userId);

            try
            {
                var result = JsonSerializer.Deserialize<JsonElement>(jsonResponse);
                if (result.GetProperty("status").GetInt32() == 200)
                {
                    var data = result.GetProperty("data");

                    List<TodoItem> tasks = new();

                    foreach (JsonProperty item in data.EnumerateObject())
                    {
                        var task = item.Value;
                        tasks.Add(new TodoItem
                        {
                            item_id = task.GetProperty("item_id").GetInt32(),
                            item_name = task.GetProperty("item_name").GetString(),
                            item_description = task.GetProperty("item_description").GetString(),
                            status = task.GetProperty("status").GetString(),
                            timemodified = task.GetProperty("timemodified").GetDateTime()
                        });
                    }

                    taskLV.ItemsSource = tasks;
                }
            }
            catch
            {
                await DisplayAlert("Error", "Failed to load tasks.", "OK");
            }
        }
    }

}