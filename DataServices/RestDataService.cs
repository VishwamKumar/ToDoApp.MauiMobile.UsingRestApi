


namespace ToDoApp.MauiMobile.UsingRestApi.DataServices
{
    public class RestDataService : IRestDataService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseAddress;
        private readonly string _url;
        private readonly IConfiguration _config;
        private readonly JsonSerializerOptions _jsonSearlizerOptions;

        public RestDataService(HttpClient httpClient, IConfiguration configuration)
        {
            //_httpClient = new HttpClient();
            _config = configuration;
            var restApiUrl = _config["RestApiUrl"];
            restApiUrl = restApiUrl ?? "https://localhost:7054";

            _httpClient = httpClient;//Using http Client Factory
            _baseAddress = DeviceInfo.Platform == DevicePlatform.Android ? "http://10.0.2.2:44327" : restApiUrl;
            _url = $"{_baseAddress}/api";

            _jsonSearlizerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
        }

        public async Task AddToDoAsync(ToDo toDo)
        {
            
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                Console.WriteLine("-->No Internet Access...");
                return;
            }

            try
            {
                string jsonToDo = JsonSerializer.Serialize<ToDo>(toDo,_jsonSearlizerOptions);
                StringContent reqContent = new StringContent(jsonToDo,Encoding.UTF8,"application/json");

                HttpResponseMessage response = await _httpClient.PostAsync($"{_url}/todos", reqContent);

                if (response.IsSuccessStatusCode)
                {
                    Console.Write("Successfully Created");                   
                }
                else
                {
                    Console.WriteLine("-->Non Http 2xx response");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Oops Exception {ex.Message}");
            }

            return;
        }

        public async Task DeleteToDoAsync(int id)
        {
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                Console.WriteLine("-->No Internet Access...");
                return;
            }

            try
            {  
                HttpResponseMessage response = await _httpClient.DeleteAsync($"{_url}/todos/{id}");

                if (response.IsSuccessStatusCode)
                {
                    Console.Write("Successfully Deleted.");
                }
                else
                {
                    Console.WriteLine("-->Non Http 2xx response");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Oops Exception {ex.Message}");
            }

            return;
        }

        public async Task<List<ToDo>?> GetAllToDosAsync()
        {
            List<ToDo>? toDos = [];
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                Console.WriteLine("-->No Internet Access...");
                return toDos;
            }

            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"{_url}/todos");
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    toDos = JsonSerializer.Deserialize<List<ToDo>>(content, _jsonSearlizerOptions);
                    return toDos;
                }
                else
                {
                    Console.WriteLine("-->Non Http 2xx response");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Oops Exception {ex.Message}");
            }

            return toDos;
        }

        public async Task UpdateToDoAsync(int id, ToDo toDo)
        {
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                Console.WriteLine("-->No Internet Access...");
                return;
            }

            try
            {
                string jsonToDo = JsonSerializer.Serialize<ToDo>(toDo, _jsonSearlizerOptions);
                StringContent reqContent = new StringContent(jsonToDo, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PutAsync($"{_url}/todos/{id}", reqContent);

                if (response.IsSuccessStatusCode)
                {
                    Console.Write("Successfully Updated");
                }
                else
                {
                    Console.WriteLine("-->Non Http 2xx response");
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine($"Oops Exception {ex.Message}");
            }

            return;
        }
    }
}
