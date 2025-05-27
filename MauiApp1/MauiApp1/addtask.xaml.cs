using MauiApp1.Services;
using System.Text.Json;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1;

public partial class AddTask : ContentPage
{
    private readonly ApiService _apiService;

    private readonly TodoList _todoListPage;

	public AddTask(TodoList todoList)
	{
		InitializeComponent();
        _apiService = new ApiService();
        _todoListPage = todoList;
        NavigationPage.SetHasNavigationBar(this, false);
        NavigationPage.SetHasBackButton(this, true);
    }

    private async void OnAddTaskClicked(object sender, EventArgs e)
    {
        string title = taskTitleEntry.Text;
        string description = taskDescEditor.Text;
        string status = "active";
        string timemodified = DateTime.Now.ToString();


        if (string.IsNullOrWhiteSpace(title))
        {
            await DisplayAlert("Validation", "Please enter a task title.", "OK");
            return;
        }

        string response = await _apiService.AddTaskAsync(title, description, SessionService.Instance.UserId, status, timemodified);

        var result = JsonSerializer.Deserialize<JsonElement>(response);

        if (result.GetProperty("status").GetInt32() == 200)
        {
            await DisplayAlert("Success", "Task added successfully.", "OK");
            await _todoListPage.ReloadTasksAsync();
            await Navigation.PopAsync();
        }
        else
        {
            await DisplayAlert("Error", "Failed to add task.", "OK");
        }


    }

    private async void OnClickedCancel(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
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

        await Navigation.PopToRootAsync();

        Application.Current.MainPage = new NavigationPage(new SigninPage());
    }



}