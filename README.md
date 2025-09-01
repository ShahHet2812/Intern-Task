# Employee Management System API  

## 🔧 Setup Instructions  

1. Clone the repository:  
   ```bash
   git clone <repository-url>
   cd EmployeeManagement
Set up the database:

Open DatabaseScript.sql in SQL Server Management Studio (SSMS).

Execute it to create the InternDB database, Employees table, and stored procedures.

Configure the connection string in appsettings.json with your SQL Server details.

Run the application:

dotnet restore
dotnet run


http://localhost:5065/swagger
Testing Instructions

You can test the API using Swagger UI or Postman.

The following operations are supported:

Create a new employee

Read employees (single and paginated list with search)

Update employee details

Delete an employee
