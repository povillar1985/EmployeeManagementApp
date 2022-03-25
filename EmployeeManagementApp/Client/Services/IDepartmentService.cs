using EmployeeManagementApp.Shared;
using System.Net.Http.Json;

namespace EmployeeManagementApp.Client.Services
{
    public interface IDepartmentService
    {
        Task<IEnumerable<Department>> GetAllAsync();
        Task<Department> GetAsync(int id);
        Task<Department> AddAsync(Department department);
        Task<bool> UpdateAsync(Department department);
        Task<bool> DeleteAsync(int id);
    }

    public class DepartmentService : IDepartmentService
    {
        private readonly HttpClient _httpClient;
        private const string API_BASE = "api/Departments";

        public DepartmentService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Department> AddAsync(Department department)
        {
            var result = await _httpClient.PostAsJsonAsync(API_BASE, department);
            return await result.Content.ReadFromJsonAsync<Department>();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var result = await _httpClient.DeleteAsync($"{API_BASE}/{id}");
            return result.IsSuccessStatusCode;
        }

        public async Task<IEnumerable<Department>> GetAllAsync()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<Department>>(API_BASE);
        }

        public async Task<Department> GetAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Department>($"{API_BASE}/{id}");
        }

        public async Task<bool> UpdateAsync(Department department)
        {
            var result = await _httpClient.PutAsJsonAsync($"{API_BASE}/{department.Id}", department);
            return result.IsSuccessStatusCode;
        }
    }
}
