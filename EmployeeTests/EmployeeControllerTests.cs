using Employee_Management.Controllers;
using Employee_Management.Models;
using Employee_Management.Repositories.Interfaces;
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
    }
}
