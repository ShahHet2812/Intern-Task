using EmployeeManagement.Models;

namespace EmployeeManagement.Services
{
    public interface IEmployeeService
    {
        (IEnumerable<Employee> Employees, int TotalRecords) GetEmployees(int pageNumber, int pageSize, string? searchTerm);
        Employee? GetEmployeeById(int id);
        bool AddEmployee(Employee employee, out string errorMessage);
        bool UpdateEmployee(Employee employee, out string errorMessage);
        void DeleteEmployee(int id);
    }
}