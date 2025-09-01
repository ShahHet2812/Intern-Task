using EmployeeManagement.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace EmployeeManagement.Data
{
    public class EmployeeRepository : IEmployeeRepository // Add this interface
    {
        // ... The rest of your code in this file remains the same ...
        private readonly string _connectionString;
        private readonly ILogger<EmployeeRepository> _logger;

        public EmployeeRepository(IConfiguration configuration, ILogger<EmployeeRepository> logger)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            _logger = logger;
        }

        public (IEnumerable<Employee> Employees, int TotalRecords) GetEmployees(int pageNumber, int pageSize, string searchTerm)
        {
            var employees = new List<Employee>();
            int totalRecords = 0;
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand("GetEmployeesPaginated", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@PageNumber", pageNumber);
                        command.Parameters.AddWithValue("@PageSize", pageSize);
                        command.Parameters.AddWithValue("@SearchTerm", string.IsNullOrEmpty(searchTerm) ? DBNull.Value : searchTerm);

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                employees.Add(new Employee
                                {
                                    EmployeeId = (int)reader["EmployeeId"],
                                    Name = reader["Name"]?.ToString() ?? string.Empty,
                                    Email = reader["Email"]?.ToString() ?? string.Empty,
                                    Department = reader["Department"]?.ToString() ?? string.Empty
                                });
                            }

                            if (reader.NextResult() && reader.Read())
                            {
                                totalRecords = (int)reader["TotalRecords"];
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting paginated employees.");
                throw;
            }
            return (employees, totalRecords);
        }

        public Employee? GetEmployeeById(int id)
        {
            Employee? employee = null;
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var command = new SqlCommand("GetEmployeeById", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    command.Parameters.AddWithValue("@EmployeeId", id);
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            employee = new Employee
                            {
                                EmployeeId = (int)reader["EmployeeId"],
                                Name = reader["Name"]?.ToString() ?? string.Empty,
                                Email = reader["Email"]?.ToString() ?? string.Empty,
                                Department = reader["Department"]?.ToString() ?? string.Empty
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting employee by ID {EmployeeId}", id);
            }
            return employee;
        }

        public Employee? GetEmployeeByEmail(string email)
        {
            Employee? employee = null;
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                     var command = new SqlCommand("GetEmployeeByEmail", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    command.Parameters.AddWithValue("@Email", email);
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                             employee = new Employee
                            {
                                EmployeeId = (int)reader["EmployeeId"],
                                Name = reader["Name"]?.ToString() ?? string.Empty,
                                Email = reader["Email"]?.ToString() ?? string.Empty,
                                Department = reader["Department"]?.ToString() ?? string.Empty
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                 _logger.LogError(ex, "Error getting employee by Email {Email}", email);
            }
            return employee;
        }
        
        public void AddEmployee(Employee employee)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var command = new SqlCommand("AddEmployee", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    command.Parameters.AddWithValue("@Name", employee.Name);
                    command.Parameters.AddWithValue("@Email", employee.Email);
                    command.Parameters.AddWithValue("@Department", employee.Department);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding a new employee.");
                throw;
            }
        }

        public void UpdateEmployee(Employee employee)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var command = new SqlCommand("UpdateEmployee", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    command.Parameters.AddWithValue("@EmployeeId", employee.EmployeeId);
                    command.Parameters.AddWithValue("@Name", employee.Name);
                    command.Parameters.AddWithValue("@Email", employee.Email);
                    command.Parameters.AddWithValue("@Department", employee.Department);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating employee {EmployeeId}", employee.EmployeeId);
                throw;
            }
        }

        public void DeleteEmployee(int id)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var command = new SqlCommand("DeleteEmployee", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    command.Parameters.AddWithValue("@EmployeeId", id);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting employee {EmployeeId}", id);
                throw;
            }
        }
    }
}