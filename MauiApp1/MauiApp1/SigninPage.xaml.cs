using MauiApp1.Services;
using System.Text.Json;

namespace MauiApp1;

public partial class SigninPage : ContentPage
{
    private readonly ApiService _apiService;

    public SigninPage()
    {
        InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);
        NavigationPage.SetHasBackButton(this, false);
        _apiService = new ApiService();
    }

    private async void OnLoginClicked(object sender, EventArgs e)
    {
        string email = entryEmail.Text;
        string password = entryPassword.Text;

        if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
        {
            await DisplayAlert("Error", "Please enter both email and password.", "OK");
            return;
        }

        string jsonResponse = await _apiService.SignInAsync(email, password);

        try
        {
            var result = JsonSerializer.Deserialize<JsonElement>(jsonResponse);
            int status = result.GetProperty("status").GetInt32();

            if (status == 200)
            {
                var userData = result.GetProperty("data");
                int userId = userData.GetProperty("id").GetInt32();
                string fname = userData.GetProperty("fname").GetString();
                string lname = userData.GetProperty("lname").GetString();

                SessionService.Instance.UserId = userId;
                SessionService.Instance.FName = fname;
                SessionService.Instance.LName = lname;
                SessionService.Instance.Email = userData.GetProperty("email").GetString();

                Application.Current.MainPage = new AppShell();
                await Shell.Current.GoToAsync("//CompleteList");
            }
            else
            {
                string message = result.GetProperty("message").GetString();
                await DisplayAlert("Login Failed", message, "OK");
            }
        }
        catch
        {
            await DisplayAlert("Error", "Invalid server response.", "OK");
        }
    }


    private async void Tap_Register(object sender, TappedEventArgs e)
    {
        Navigation.PushAsync(new SignupPage());
    }


}
