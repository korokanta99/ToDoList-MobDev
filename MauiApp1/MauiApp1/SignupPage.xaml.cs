using MySql.Data.MySqlClient;

namespace MauiApp1;

public partial class SignupPage : ContentPage
{
    public SignupPage()
    {
        InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);
        NavigationPage.SetHasBackButton(this, false);
    }

    private async void RegisterButton_Clicked(object sender, EventArgs e)
    {
        string username = usernameEntry.Text?.Trim();
        string email = emailEntry.Text?.Trim();
        string password = passwordEntry.Text?.Trim();
        string confirmPassword = confirmPasswordEntry.Text?.Trim();

        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(email) ||
            string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(confirmPassword))
        {
            await DisplayAlert("Error", "Please fill all fields", "OK");
            return;
        }

        if (password != confirmPassword)
        {
            await DisplayAlert("Error", "Passwords do not match", "OK");
            return;
        }

        string connectionString = "server=10.0.2.2;port=3306;database=todolist;uid=root;password=;";
        try
        {
            using MySqlConnection conn = new MySqlConnection(connectionString);
            await conn.OpenAsync();

            string query = "INSERT INTO user (UserName, Email, Password) VALUES (@username, @email, @password)";
            using MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@username", username);
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@password", password); 

            int rowsAffected = await cmd.ExecuteNonQueryAsync();
            if (rowsAffected > 0)
            {
                await DisplayAlert("Success", "Account created successfully!", "OK");
                await Navigation.PopAsync(); 
            }
            else
            {
                await DisplayAlert("Error", "Registration failed", "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "OK");
        }
    }
}
