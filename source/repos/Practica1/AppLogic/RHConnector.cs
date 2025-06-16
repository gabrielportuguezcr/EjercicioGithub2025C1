using DTO;
using Flurl.Http;
using RestSharp;
using System.Net.Http.Json;
using System.Text.Json.Serialization;

namespace AppLogic
{
    public class RHConnector : IRHConnector
    {
        private readonly HttpClient _httpClient;

        public RHConnector(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://rh-central.azurewebsites.net/api/RH/");
        }

        public async Task<List<Employee>> ReturnAllEmployeesAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<List<Employee>>("GetAllEmployees");
            return response ?? new List<Employee>();
        }

        public async Task<Employee> ReturnEmployeeBySecurityIdAsync(string securityId)
        {
            var response = await _httpClient.GetFromJsonAsync<Employee>($"GetEmployeeBySecurityId?pSecurityId={securityId}");
            return response!;
        }

        public async Task<List<string>> ReturnGetSpecialtiesAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<List<string>>("GetSpecialties");
            return response ?? new List<string>();
        }

        public async Task<EmployeeResponse> AddEmployeeAsync(Employee employee)
        {
            var response = await _httpClient.PostAsJsonAsync("AddEmployee", employee);
            response.EnsureSuccessStatusCode();
            return new EmployeeResponse { Success = true, Message = "Employee added successfully" };
        }

        public async Task<EmployeeResponse> UpdateEmployeeAsync(Employee employee)
        {
            var response = await _httpClient.PutAsJsonAsync("UpdateEmployee", employee);
            response.EnsureSuccessStatusCode();
            return new EmployeeResponse { Success = true, Message = "Employee updated successfully" };
        }

        // RestSharp
        public async Task<List<Employee>> GetAllEmployeesRestSharpAsync()
        {
            var client = new RestClient("https://rh-central.azurewebsites.net/");
            var request = new RestRequest("api/RH/GetAllEmployees", Method.Get);
            var response = await client.ExecuteAsync(request);

            if (!response.IsSuccessful)
                return new List<Employee>();

            return JsonConvert.DeserializeObject<List<Employee>>(response.Content);
        }

        // Flurl
        public async Task<List<Employee>> GetAllEmployeesFlurAsync()
        {
            var url = "https://rh-central.azurewebsites.net/api/RH/GetAllEmployees";
            try
            {
                var employees = await url.GetJsonAsync<List<Employee>>();
                return employees;
            }
            catch (Exception)
            {
                return new List<Employee>();
            }
        }
    }
}