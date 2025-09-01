using EmployeeManagement.Models;

namespace EmployeeManagement.Data
{
    public interface IEmployeeRepository
    {
        (IEnumerable<Employee> Employees, int TotalRecords) GetEmployees(int pageNumber, int pageSize, string searchTerm);
        Employee? GetEmployeeById(int id);
        Employee? GetEmployeeByEmail(string email);
        void AddEmployee(Employee employee);
        void UpdateEmployee(Employee employee);
        void DeleteEmployee(int id);
    }
}