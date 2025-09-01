using EmployeeManagement.Data;
using EmployeeManagement.Models;

namespace EmployeeManagement.Services
{
    public class EmployeeService : IEmployeeService // Make sure this interface is here
    {
        // ... The rest of your code in this file remains the same ...
        private readonly IEmployeeRepository _repository;

        public EmployeeService(IEmployeeRepository repository)
        {
            _repository = repository;
        }

        public (IEnumerable<Employee> Employees, int TotalRecords) GetEmployees(int pageNumber, int pageSize, string? searchTerm)
        {
            return _repository.GetEmployees(pageNumber, pageSize, searchTerm ?? string.Empty);
        }

        public Employee? GetEmployeeById(int id)
        {
            return _repository.GetEmployeeById(id);
        }

        public bool AddEmployee(Employee employee, out string errorMessage)
        {
            errorMessage = string.Empty;
            if (string.IsNullOrEmpty(employee.Email))
            {
                errorMessage = "Email cannot be empty.";
                return false;
            }
            var existingEmployee = _repository.GetEmployeeByEmail(employee.Email);
            if (existingEmployee != null)
            {
                errorMessage = "An employee with this email already exists.";
                return false;
            }

            _repository.AddEmployee(employee);
            return true;
        }

        public bool UpdateEmployee(Employee employee, out string errorMessage)
        {
            errorMessage = string.Empty;
            if (string.IsNullOrEmpty(employee.Email))
            {
                errorMessage = "Email cannot be empty.";
                return false;
            }
            var existingEmployee = _repository.GetEmployeeByEmail(employee.Email);
            if (existingEmployee != null && existingEmployee.EmployeeId != employee.EmployeeId)
            {
                errorMessage = "An employee with this email already exists.";
                return false;
            }
            
            _repository.UpdateEmployee(employee);
            return true;
        }

        public void DeleteEmployee(int id)
        {
            _repository.DeleteEmployee(id);
        }
    }
}