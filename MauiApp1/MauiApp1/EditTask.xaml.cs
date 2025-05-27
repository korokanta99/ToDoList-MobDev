using MauiApp1.Services;
using System.Text.Json;
using System.Threading.Tasks;

namespace MauiApp1;

public partial class EditTask : ContentPage
{
    private readonly ApiService _apiService;
    private readonly TodoItem _task;

    private readonly IRefreshablePage _sourcePage;

    public EditTask(IRefreshablePage sourcePage, TodoItem task)
	{
		InitializeComponent();
        _apiService = new ApiService();
        _task = task;
        _sourcePage = sourcePage;

        taskTitleEntry.Text = _task.item_name;
        taskDescEditor.Text = _task.item_description;

        StatusColor(_task.status);

        NavigationPage.SetHasNavigationBar(this, false);
        NavigationPage.SetHasBackButton(this, false);
    }

    private void StatusColor(string status)
    {
        if(status == "active")
        {
            completionToggleButton.BackgroundColor = Colors.LightGray;
        }
        else
        {
            completionToggleButton.BackgroundColor = Colors.Gold;
        }
    }

    private void OnClickedClear(object sender, EventArgs e)
    {
        taskTitleEntry.Text = string.Empty;
        taskDescEditor.Text = string.Empty;
    }
    
    private async void OnClickedCancel(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }

    private async void OnClickedSave(object sender, EventArgs e)
    {

        _task.item_name = taskTitleEntry.Text;
        _task.item_description = taskDescEditor.Text;

        _task.timemodified = DateTime.Now.ToString();


        if (taskTitleEntry.Text == string.Empty)
        {
            await DisplayAlert("Warning!", "Title must not be empty.", "OK");
        }
        else
        {
            var response = await _apiService.EditTaskAsync(
                _task.item_id,
                _task.item_name,
                _task.item_description,
                _task.timemodified
            );

            var result = JsonSerializer.Deserialize<JsonElement>(response);

            if (result.GetProperty("status").GetInt32() == 200)
            {
                await _sourcePage.ReloadTasksAsync();
                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Error", "Failed to Edit task.", "OK");
            }
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

        await Navigation.PopToRootAsync();

        Application.Current.MainPage = new NavigationPage(new SigninPage());
    }
}