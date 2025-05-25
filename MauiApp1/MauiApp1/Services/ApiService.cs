using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using MauiApp1.Models;

namespace MauiApp1.Services
{
    public class ApiService
    {
        private readonly HttpClient _client;

        public ApiService()
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri("https://todo-list.dcism.org")
            };
        }

        public async Task<string> SignUpAsync(SignUpModel model)
        {
            var json = JsonSerializer.Serialize(model);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("/signup_action.php", content);
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> SignInAsync(string email, string password)
        {
            var url = $"/signin_action.php?email={Uri.EscapeDataString(email)}&password={Uri.EscapeDataString(password)}";
            var response = await _client.GetAsync(url);
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> GetTasksAsync(string status, int userId)
        {
            var url = $"/getItems_action.php?status={Uri.EscapeDataString(status)}&user_id={userId}";
            var response = await _client.GetAsync(url);
            return await response.Content.ReadAsStringAsync();
        }

    }
}
