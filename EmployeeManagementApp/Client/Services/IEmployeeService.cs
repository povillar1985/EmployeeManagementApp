using EmployeeManagementApp.Shared;
using System.Net.Http.Json;

namespace EmployeeManagementApp.Client.Services
{
    public interface IEmployeeService
    {
        Task<IEnumerable<Employee>> GetAllAsync();
        Task<IEnumerable<Employee>> GetAllAsync(int departmentId);
        Task<Employee> GetAsync(int id);
        Task<Employee> AddAsync(Employee employee);
        Task<bool> UpdateAsync(Employee employee);
        Task<bool> DeleteAsync(int id);
    }

    public class EmployeeService : IEmployeeService
    {
        private readonly HttpClient _httpClient;
        private const string API_BASE = "api/Employees";

        public EmployeeService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Employee> AddAsync(Employee employee)
        {
            var result = await _httpClient.PostAsJsonAsync(API_BASE, employee);
            return await result.Content.ReadFromJsonAsync<Employee>();  
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var result = await _httpClient.DeleteAsync($"{API_BASE}/{id}");
            return result.IsSuccessStatusCode;
        }

        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<Employee>>(API_BASE);
        }

        public async Task<IEnumerable<Employee>> GetAllAsync(int departmentId)
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<Employee>>(API_BASE+$"/department/{departmentId}");
        }

        public async Task<Employee> GetAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Employee>($"{API_BASE}/{id}");
        }

        public async Task<bool> UpdateAsync(Employee employee)
        {
            var result = await _httpClient.PutAsJsonAsync($"{API_BASE}/{employee.Id}", employee);
            return result.IsSuccessStatusCode;
        }
    }
}
