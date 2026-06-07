using Employee_Management.Controllers;
using Employee_Management.Models;
using Employee_Management.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Threading.Tasks;

namespace EmployeeTests
{
    public class EmployeeControllerTests
    {
        private readonly Mock<IEmployeeRepository> _mockRepo;

        public EmployeeControllerTests()
        {
            _mockRepo = EmployeeStubData.GetMockEmployeeRepository();
        }

        [Fact]
        public async Task AddNewEmployee()
        {
            // Arrange
            var controller = new EmployeeController(_mockRepo.Object);
            var newEmployee = new Employee { FirstName = "John", LastName = "Doe", Email = "john.doe@example.com" };
            var initialCount = EmployeeStubData.Employees.Count;

            // Act
            var result = await controller.CreateEmployee(newEmployee);

            // Assert
            Assert.Equal(initialCount + 1, EmployeeStubData.Employees.Count);
            Assert.IsType<CreatedAtActionResult>(result.Result); 
        }

        [Fact]
        public async Task GetAllEmployees_ReturnsOk()
        {
            // Arrange
            var controller = new EmployeeController(_mockRepo.Object);
            var employees = new List<Employee>
            {
                new Employee { Id = 1, FirstName = "John", LastName = "Doe" },
                new Employee { Id = 2, FirstName = "Jane", LastName = "Smith" }
            };

            // Act
            var result = await controller.GetAllAsync();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
;            Assert.Same(employees, okResult.Value);
            _mockRepo.Verify(r => r.GetAllEmployeesAsync(), Times.Once);
        }

        [Fact]
        public async Task GetEmployeeById_ReturnsOk()
        {

        }

    }
}
