using MauiApp1.Models;
using MauiApp1.Services;

namespace MauiApp1;

public partial class SignupPage : ContentPage
{
    private readonly ApiService _apiService;

    public SignupPage()
    {
        InitializeComponent();
        _apiService = new ApiService();
    }

    private async void OnRegisterClicked(object sender, EventArgs e)
    {
        var model = new SignUpModel
        {
            first_name = entryFirstName.Text,
            last_name = entryLastName.Text,
            email = entryEmail.Text,
            password = entryPassword.Text,
            confirm_password = entryConfirmPassword.Text
        };

        if (string.IsNullOrWhiteSpace(model.first_name) ||
            string.IsNullOrWhiteSpace(model.last_name) ||
            string.IsNullOrWhiteSpace(model.email) ||
            string.IsNullOrWhiteSpace(model.password))
        {
            await DisplayAlert("Error", "Please fill in all fields.", "OK");
            return;
        }

        if (model.password != model.confirm_password)
        {
            await DisplayAlert("Error", "Passwords do not match.", "OK");
            return;
        }

        var response = await _apiService.SignUpAsync(model);
        await DisplayAlert("API Response", response, "OK");
    }
}
