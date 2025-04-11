using MySql.Data.MySqlClient;

namespace MauiApp1;

public partial class SigninPage : ContentPage
{
    public SigninPage()
    {
        InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);
        NavigationPage.SetHasBackButton(this, false);

    }

    public void Tap_Register(object sender, TappedEventArgs e)
    {
        Navigation.PushAsync(new SignupPage());
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        string emailOrUsername = emailEntry.Text?.Trim();
        string password = passwordEntry.Text;

        if (string.IsNullOrWhiteSpace(emailOrUsername) || string.IsNullOrWhiteSpace(password))
        {
            await DisplayAlert("Error", "Please fill in both fields.", "OK");
            return;
        }

        string connectionString = "server=10.0.2.2;port=3306;database=todolist;uid=root;password=;";


        try
        {
            using MySqlConnection conn = new MySqlConnection(connectionString);
            await conn.OpenAsync();

            string query = @"SELECT * FROM user 
                             WHERE (Email = @input OR UserName = @input) 
                             AND Password = @password";

            using MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@input", emailOrUsername);
            cmd.Parameters.AddWithValue("@password", password);

            using MySqlDataReader reader = (MySqlDataReader)await cmd.ExecuteReaderAsync();

            if (reader.HasRows)
            {
                await DisplayAlert("Success", "Login successful!", "OK");
                await Navigation.PushAsync(new AppShell());
            }
            else
            {
                await DisplayAlert("Failed", "Invalid email/username or password.", "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Database Error", ex.Message, "OK");
        }
    }


    private async void TestDatabaseConnection()
    {
        string connectionString = "server=localhost;port=3306;database=todolist;uid=root;password=;";

        try
        {
            using MySqlConnection connection = new MySqlConnection(connectionString);
            await connection.OpenAsync();
            await DisplayAlert("Success", "Connected to MySQL!", "OK");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "OK");
        }
    }
}
