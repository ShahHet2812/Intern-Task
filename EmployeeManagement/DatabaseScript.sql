-- Create the Employees table
CREATE TABLE Employees (
    EmployeeId INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) NOT NULL UNIQUE,
    Department NVARCHAR(50) NOT NULL
);
GO

-- Stored Procedure to Add an Employee
CREATE PROCEDURE AddEmployee
    @Name NVARCHAR(100),
    @Email NVARCHAR(100),
    @Department NVARCHAR(50)
AS
BEGIN
    INSERT INTO Employees (Name, Email, Department)
    VALUES (@Name, @Email, @Department);
END
GO

-- Stored Procedure to Get a single Employee by ID
CREATE PROCEDURE GetEmployeeById
    @EmployeeId INT
AS
BEGIN
    SELECT EmployeeId, Name, Email, Department
    FROM Employees
    WHERE EmployeeId = @EmployeeId;
END
GO

-- Stored Procedure to Get a single Employee by Email
CREATE PROCEDURE GetEmployeeByEmail
    @Email NVARCHAR(100)
AS
BEGIN
    SELECT EmployeeId, Name, Email, Department
    FROM Employees
    WHERE Email = @Email;
END
GO

-- Stored Procedure to Update an Employee
CREATE PROCEDURE UpdateEmployee
    @EmployeeId INT,
    @Name NVARCHAR(100),
    @Email NVARCHAR(100),
    @Department NVARCHAR(50)
AS
BEGIN
    UPDATE Employees
    SET Name = @Name,
        Email = @Email,
        Department = @Department
    WHERE EmployeeId = @EmployeeId;
END
GO

-- Stored Procedure to Delete an Employee
CREATE PROCEDURE DeleteEmployee
    @EmployeeId INT
AS
BEGIN
    DELETE FROM Employees
    WHERE EmployeeId = @EmployeeId;
END
GO

-- Stored Procedure for Search and Pagination
CREATE PROCEDURE GetEmployeesPaginated
    @PageNumber INT = 1,
    @PageSize INT = 5,
    @SearchTerm NVARCHAR(100) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    SELECT EmployeeId, Name, Email, Department
    FROM Employees
    WHERE (@SearchTerm IS NULL OR Name LIKE '%' + @SearchTerm + '%' OR Email LIKE '%' + @SearchTerm + '%')
    ORDER BY Name
    OFFSET (@PageNumber - 1) * @PageSize ROWS
    FETCH NEXT @PageSize ROWS ONLY;

    -- Also, get the total count for pagination
    SELECT COUNT(*) AS TotalRecords
    FROM Employees
    WHERE (@SearchTerm IS NULL OR Name LIKE '%' + @SearchTerm + '%' OR Email LIKE '%' + @SearchTerm + '%');
END
GO