namespace MauiApp1.Services;
using MauiApp1;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

public class TaskService
{
    private readonly HttpClient _httpClient;
    private const string BaseUrl = "https://todo-list.dcism.org/";

    public TaskService()
    {
        _httpClient = new HttpClient();
        _httpClient.DefaultRequestHeaders.Accept.Clear();
    }

    public async Task<List<tasklist>> GetTasksAsync(int userId, string status)
    {
        var url = $"{BaseUrl}/getItems_action.php?userId={userId}&status={status}";
        var response = await _httpClient.GetAsync(url);

        var result = JsonConvert.DeserializeObject<APIResponse>(response);

        if (result.status == 200 && result.data != null)
        {
            return result.data;
        }
        
        return new List<tasklist>();
    }

    public async Task<tasklist> AddTaskAsync(tasklist newTask)
    {
        var url = $"{BaseUrl}/addItem_action.php";

        var postData = new Dictionary<string, string>
        {
            { "item_name", newTask.taskname },
            { "item_description", newTask.description },
            { "user_id", newTask.userId.ToString() },
        };
        
        var content = new FormUrlEncodedContent(postData);
        
        var response = await _httpClient.PostAsync(url, content);
        
        var responseString = await response.Content.ReadAsStringAsync();

        var result = JsonConvert.DeserializeObject<APIResponse>(responseString);

        if (result.status == 200 && result.data != null)
        {
            var task = result.data[0];
            return task;
        }
        
        return null;
        
    }

    public async Task<bool> DeleteTaskAsync(int taskId)
    {
        var url = $"{BaseUrl}/deleteItem_action.php?taskId={taskId}";
        var response = await _httpClient.DeleteAsync(url);
        
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> UpdateTaskAsync(tasklist updatedTask)
    {
        var url = $"{BaseUrl}/editItem_action.php";

        var postData = new Dictionary<string, string>
        {
            { "item_name", updatedTask.taskname },
            { "item_description", updatedTask.description },
            { "user_id", updatedTask.userId.ToString() },
        };
        
        var content = new FormUrlEncodedContent(postData);
        var response = await _httpClient.PutAsync(url, content);
        return response.IsSuccessStatusCode;
        
    }
    
    public class APIResponse
    {
        public int status { get; set; }
        public string message { get; set; }
        public List<tasklist> data { get; set; }
    }
    
}

