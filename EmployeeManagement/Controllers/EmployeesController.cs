using EmployeeManagement.Models;
using EmployeeManagement.Services; // Add this line
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeesController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        // ... the rest of your controller code remains the same
        [HttpGet]
        public IActionResult GetEmployees([FromQuery] string? searchTerm, [FromQuery] int pageNumber = 1)
        {
            var (employees, totalRecords) = _employeeService.GetEmployees(pageNumber, 5, searchTerm);
            return Ok(new { Employees = employees, TotalRecords = totalRecords });
        }

        [HttpGet("{id}")]
        public IActionResult GetEmployee(int id)
        {
            var employee = _employeeService.GetEmployeeById(id);
            if (employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }

        [HttpPost]
        public IActionResult CreateEmployee([FromBody] EmployeeDto employeeDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var employee = new Employee
            {
                Name = employeeDto.Name,
                Email = employeeDto.Email,
                Department = employeeDto.Department
            };
            
            if (!_employeeService.AddEmployee(employee, out string errorMessage))
            {
                return BadRequest(new { message = errorMessage });
            }

            return CreatedAtAction(nameof(GetEmployee), new { id = employee.EmployeeId }, employee);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateEmployee(int id, [FromBody] EmployeeDto employeeDto)
        {
            if (id <= 0 || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var employeeToUpdate = _employeeService.GetEmployeeById(id);
            if (employeeToUpdate == null)
            {
                return NotFound();
            }

            employeeToUpdate.Name = employeeDto.Name;
            employeeToUpdate.Email = employeeDto.Email;
            employeeToUpdate.Department = employeeDto.Department;

            if (!_employeeService.UpdateEmployee(employeeToUpdate, out string errorMessage))
            {
                return BadRequest(new { message = errorMessage });
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteEmployee(int id)
        {
            var employee = _employeeService.GetEmployeeById(id);
            if (employee == null)
            {
                return NotFound();
            }

            _employeeService.DeleteEmployee(id);
            return NoContent();
        }
    }
}