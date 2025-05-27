using MauiApp1.Models;
using MauiApp1.Services;
using System.Text.Json;
using MauiApp1;
using System.Threading.Tasks;


namespace MauiApp1
{


    public partial class TodoList : ContentPage, IRefreshablePage
    {
        private readonly ApiService _apiService;
        private int _userId => SessionService.Instance.UserId;

        private readonly CompleteList _completeList;

        public async Task ReloadTasksAsync() => LoadTasks();

        public TodoList()
        {
            InitializeComponent();
            _apiService = new ApiService();
            NavigationPage.SetHasNavigationBar(this, false);
            NavigationPage.SetHasBackButton(this, false);
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
                            status = "active",
                            timemodified = DateTime.Now.ToString()
                        });
                    }

                    taskLV.ItemsSource = tasks;
                }
            }
            catch(Exception ex)
            {
                await DisplayAlert("Error", $"Failed to load tasks. \n {ex.Message}", "OK");
            }
        }

        private async void OnAddTaskClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddTask(this));
        }

        private async void taskLV_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var task = (TodoItem)e.Item;
            //await Navigation.PushAsync(new EditTask(this, _completeList, task));
            await Navigation.PushAsync(new EditTask(this, task));
        }

        private async void OnDeleteTapped(object sender, TappedEventArgs e)
        {
            var image = (Image)sender;
            var task = (TodoItem)image.BindingContext;

            bool confirm = await DisplayAlert("Delete", $"Delete task '{task.item_name}'?", "Yes", "No");
            if (confirm)
            {
                var response = await _apiService.DeleteTaskAsync(task.item_id);
                var result = JsonSerializer.Deserialize<JsonElement>(response);

                if (result.GetProperty("status").GetInt32() == 200)
                {
                    LoadTasks();
                }
                else
                {
                    await DisplayAlert("Error", "Failed to delete task.", "OK");
                }
            }
        }

        private async void OnStatusToggleTapped(object sender, EventArgs e)
        {
            var image = (Image)sender;
            var task = (TodoItem)image.BindingContext;

            string newStatus = task.status == "active" ? "inactive" : "active";

            var response = await _apiService.ToggleTaskStatusAsync(task.item_id, newStatus);
            var result = JsonSerializer.Deserialize<JsonElement>(response);

            if (result.GetProperty("status").GetInt32() == 200)
            {
                LoadTasks();
            }
            else
            {
                await DisplayAlert("Error", "Failed to update status.", "OK");
            }
        }

        private async void OnUserIconTapped(object sender, EventArgs e)
        {
            userPopup.Opacity = 0;
            userPopup.IsVisible = true;
            await userPopup.FadeTo(1, 150);
        }

        private async void OnUserPopupBackgroundTapped(object sender, EventArgs e)
        {
            await userPopup.FadeTo(0, 150);
            userPopup.IsVisible = false;
        }

        private async void OnSignOutClicked(object sender, EventArgs e)
        {
            bool confirm = await DisplayAlert("Sign Out", "Are you sure you want to sign out?", "Yes", "No");
            if (!confirm) return;

            SessionService.Instance.Clear();

            Application.Current.MainPage = new NavigationPage(new SigninPage());
        }


    }

}