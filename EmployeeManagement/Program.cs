using EmployeeManagement.Data;
using EmployeeManagement.Services;

var builder = WebApplication.CreateBuilder(args);

// --- API and Swagger Configuration ---
// Add services to the container.
builder.Services.AddControllers(); // Use AddControllers for Web API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(); // Add Swagger generator

// --- Dependency Injection ---
// Register your services and repositories as before
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();

var app = builder.Build();

// --- Middleware Pipeline ---
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(); // Enable middleware to serve Swagger UI
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers(); // Map attribute-routed API controllers

app.Run();