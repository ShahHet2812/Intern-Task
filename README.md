Employee Management System API
This project is an ASP.NET Core Web API for managing employee records. It provides a complete set of CRUD (Create, Read, Update, Delete) operations, built with a 3-layered architecture and backed by a SQL Server database using ADO.NET and stored procedures.

The API includes advanced features such as pagination, search functionality, and validation, with a Swagger interface for easy interaction and testing.

Features
CRUD Operations: Full support for creating, reading, updating, and deleting employee records.

3-Layered Architecture: Organized into Presentation (API Controller), Business Logic (Service), and Data Access (Repository) layers for maintainability and separation of concerns.

ADO.NET and Stored Procedures: All database operations are handled securely and efficiently through stored procedures.

Pagination: The GET /api/Employees endpoint supports pagination to handle large datasets efficiently.

Search: The employee list can be filtered by name or email using a search term.

Validation: The system ensures that the name, email, and department fields are not empty, and that each employee has a unique email address.

Swagger UI: Integrated Swagger for interactive API documentation and testing directly in the browser.

Technology Stack
Framework: ASP.NET Core (.NET 9.0)

Language: C#

Database: Microsoft SQL Server

Data Access: ADO.NET

API Documentation: Swashbuckle (Swagger)

Prerequisites
Before you begin, ensure you have the following installed on your system:

.NET SDK (Version 9.0 or later)

Microsoft SQL Server (Express or Developer edition is sufficient)

SQL Server Management Studio (SSMS) or a similar database management tool.

Setup and Installation
Follow these steps to get the project up and running on your local machine.

1. Clone the Repository
Bash

git clone <your-repository-url>
cd EmployeeManagement
2. Set Up the Database
Open SQL Server Management Studio (SSMS) and connect to your local SQL Server instance (usually (localdb)\mssqllocaldb).

Open the DatabaseScript.sql file provided in the root of the project.

Execute the script. This will create the InternDB database, the Employees table, and all the required stored procedures.

3. Configure the Connection String
Open the appsettings.json file in the main project directory.

Verify that the DefaultConnection string points to your local SQL Server instance. The default is configured for local development and should work for most setups.

JSON

"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=InternDB;Trusted_Connection=True;TrustServerCertificate=True;"
}
4. Run the Application
Open a terminal or command prompt in the root directory of the project (EmployeeManagement).

Run the following commands to restore the necessary packages and start the application:

Bash

dotnet restore
dotnet run
The application will start, and a browser window will automatically open to the Swagger UI at http://localhost:5065/swagger.

API Endpoints and Usage
You can test all API endpoints directly from the Swagger UI.

GET /api/Employees
Retrieves a paginated list of employees.

Parameters: searchTerm (optional string), pageNumber (optional integer, default is 1).

Success Response: 200 OK with a list of employees.

GET /api/Employees/{id}
Retrieves a single employee by their ID.

Parameters: id (required integer).

Success Response: 200 OK with the employee data.

Error Response: 404 Not Found if the ID does not exist.

POST /api/Employees
Creates a new employee.

Request Body: JSON object with name, email, and department.

JSON

{
  "name": "Jane Smith",
  "email": "jane.smith@example.com",
  "department": "Marketing"
}
Success Response: 201 Created with the newly created employee's data.

Error Response: 400 Bad Request if validation fails (e.g., duplicate email).

PUT /api/Employees/{id}
Updates an existing employee's information.

Parameters: id (required integer).

Request Body: JSON object with the fields to be updated.

JSON

{
  "name": "Jane Smith",
  "email": "jane.smith.new@example.com",
  "department": "Senior Marketing"
}
Success Response: 204 No Content.

Error Response: 404 Not Found if the ID does not exist.

DELETE /api/Employees/{id}
Deletes an employee from the database.

Parameters: id (required integer).

Success Response: 204 No Content.

Error Response: 404 Not Found if the ID does not exist.
