using AppLogic;
using DTO;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IRHConnector _rhConnector;

        public EmployeeController(IRHConnector rhConnector)
        {
            _rhConnector = rhConnector;
        }

        [HttpGet("RetriveAllEmployees")]
        public async Task<List<Employee>> RetriveAllEmployees()
        {
            return await _rhConnector.ReturnAllEmployeesAsync();
        }

        [HttpGet("RetriveEmployeeBySecurityId")]
        public async Task<Employee> RetriveEmployeeBySecurityId([FromQuery] string pSecurityId)
        {
            return await _rhConnector.ReturnEmployeeBySecurityIdAsync(pSecurityId);
        }

        [HttpGet("RetriveSpecialties")]
        public async Task<List<string>> RetriveSpecialties()
        {
            return await _rhConnector.ReturnGetSpecialtiesAsync();
        }

        [HttpPost("AddEmployee")]
        public async Task<EmployeeResponse> AddEmployee([FromBody] Employee employee)
        {
            return await _rhConnector.AddEmployeeAsync(employee);
        }

        [HttpPut("UpdateEmployee")]
        public async Task<EmployeeResponse> UpdateEmployee([FromBody] Employee employee)
        {
            return await _rhConnector.UpdateEmployeeAsync(employee);
        }

        // Práctica 1
        // Obtener manager de un empleado por su ID
        [HttpGet("GetEmployeeManager")]
        public async Task<ActionResult<Employee>> GetEmployeeManager([FromQuery] int employeeId)
        {
            var employees = await _rhConnector.ReturnAllEmployeesAsync();
            var employee = employees.FirstOrDefault(e => e.Id == employeeId);

            if (employee == null || employee.ManagerId == null)
                return NotFound("Empleado o manager no encontrado.");

            var manager = employees.FirstOrDefault(e => e.Id == employee.ManagerId);
            if (manager == null)
                return NotFound("Manager no encontrado.");

            return Ok(manager);
        }

        // Obtener el empleado más antiguo (o empleados si hay varios con la misma fecha)
        [HttpGet("GetOldestEmployee")]
        public async Task<ActionResult<List<Employee>>> GetOldestEmployee()
        {
            var employees = await _rhConnector.ReturnAllEmployeesAsync();
            var oldestDate = employees.Min(e => e.HiringDate);
            var oldestEmployees = employees.Where(e => e.HiringDate == oldestDate).ToList();

            return Ok(oldestEmployees);
        }

        // Obtener el empleado más nuevo (o empleados si hay varios con la misma fecha)
        [HttpGet("GetNewestEmployee")]
        public async Task<ActionResult<List<Employee>>> GetNewestEmployee()
        {
            var employees = await _rhConnector.ReturnAllEmployeesAsync();
            var newestDate = employees.Max(e => e.HiringDate);
            var newestEmployees = employees.Where(e => e.HiringDate == newestDate).ToList();

            return Ok(newestEmployees);
        }

        // Obtener empleado por Id
        [HttpGet("GetEmployeeById")]
        public async Task<ActionResult<Employee>> GetEmployeeById([FromQuery] int id)
        {
            var employees = await _rhConnector.ReturnAllEmployeesAsync();
            var employee = employees.FirstOrDefault(e => e.Id == id);

            if (employee == null)
                return NotFound("Empleado no encontrado.");

            return Ok(employee);
        }

        // Obtener empleados con más de X años en la empresa
        [HttpGet("GetEmployeesWithMoreThan")]
        public async Task<ActionResult<List<Employee>>> GetEmployeesWithMoreThan([FromQuery] int years)
        {
            var employees = await _rhConnector.ReturnAllEmployeesAsync();
            var cutoffDate = DateTime.Today.AddYears(-years);

            var result = employees.Where(e => e.HiringDate <= cutoffDate).ToList();

            return Ok(result);
        }

        // Obtener empleados con menos de X años en la empresa
        [HttpGet("GetEmployeesWithLessThan")]
        public async Task<ActionResult<List<Employee>>> GetEmployeesWithLessThan([FromQuery] int years)
        {
            var employees = await _rhConnector.ReturnAllEmployeesAsync();
            var cutoffDate = DateTime.Today.AddYears(-years);

            var result = employees.Where(e => e.HiringDate >= cutoffDate).ToList();

            return Ok(result);
        }
        // RestSharp
        [HttpGet("GetAllEmployeesRestSharp")]
        public async Task<ActionResult<List<Employee>>> GetAllEmployeesRestSharp()
        {
            var employees = await _rhConnector.GetAllEmployeesRestSharpAsync();
            return Ok(employees);
        }
        // Flurl
        [HttpGet("GetAllEmployeesFlur")]
        public async Task<ActionResult<List<Employee>>> GetAllEmployeesFlur()
        {
            var employees = await _rhConnector.GetAllEmployeesFlurAsync();
            return Ok(employees);
        }
    }
}