using MauiApp1.Services;
using System.Text.Json;

namespace MauiApp1;

public partial class SigninPage : ContentPage
{
    private readonly ApiService _apiService;

    public SigninPage()
    {
        InitializeComponent();
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
                string name = $"{userData.GetProperty("fname").GetString()} {userData.GetProperty("lname").GetString()}";

                await DisplayAlert("Welcome", $"Hello, {name}!", "OK");

           
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
}
