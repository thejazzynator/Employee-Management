using Employee_Management.Data;
using Employee_Management.Repositories;
using Employee_Management.Repositories.Interfaces;
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

builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>(); // this registers the EmployeeRepository as a scoped service, which means that a new instance of the repository will be created for each HTTP request. This is important for managing database connections and ensuring that each request has its own repository instance.

builder.Services.AddControllers(); // this adds support for controllers in the application, which are responsible for handling HTTP requests and returning responses. It allows you to define API endpoints and their corresponding actions in controller classes.

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if(app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
        c.RoutePrefix = string.Empty; // Set Swagger UI at the app's root
    });
}


app.UseCors("MyCors");

app.MapControllers();

app.Run();
