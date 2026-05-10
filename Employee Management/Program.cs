using Employee_Management.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDbContext>(
    options => options.UseInMemoryDatabase("EmployeeDb")
    ); // this is a service that can be used for dependency injection in the controllers and repositories

builder.Services.AddCors(options =>
{
    options.AddPolicy("MyCors", builder =>
    {
        builder.WithOrigins("http://localhost:4200") //angular port
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
}); // this allows cross-origin requests from the Angular frontend running on localhost:4200. It allows any HTTP method and any header in the requests.
var app = builder.Build();
app.UseCors("MyCors");

app.MapGet("/", () => "Hello World!");

app.Run();
