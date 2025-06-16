using DTO;

namespace AppLogic
{
    public interface IRHConnector
    {
        Task<List<Employee>> ReturnAllEmployeesAsync();
        Task<Employee> ReturnEmployeeBySecurityIdAsync(string securityId);
        Task<List<string>> ReturnGetSpecialtiesAsync();
        Task<EmployeeResponse> AddEmployeeAsync(Employee employee);
        Task<EmployeeResponse> UpdateEmployeeAsync(Employee employee);
        // RestSharp
        Task<List<Employee>> GetAllEmployeesRestSharpAsync();
        // Flurl
        Task<List<Employee>> GetAllEmployeesFlurAsync();
    }
}