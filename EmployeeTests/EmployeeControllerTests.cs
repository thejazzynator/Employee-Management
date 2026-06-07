using Employee_Management.Controllers;
using Employee_Management.Models;
using Employee_Management.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace EmployeeTests
{
    public class EmployeeControllerTests
    {
        private readonly Mock<IEmployeeRepository> _mockRepo;
        private readonly EmployeeController _controller;

        public EmployeeControllerTests()
        {
            EmployeeStubData.Employees = new List<Employee>
        {
            new() { Id = 1, FirstName = "John", Position = "Software Engineer", Email = "john@example.com" },
            new() { Id = 2, FirstName = "Jane", Position = "Project Manager", Email = "jane@example.com" }
        };
            _mockRepo = EmployeeStubData.GetMockEmployeeRepository();
            _controller = new EmployeeController(_mockRepo.Object);
        }

        // ── GET ALL ──────────────────────────────────────────────────────────────

        [Fact]
        public async Task GetAllAsync_ReturnsOk_WithAllEmployees()
        {
            // Act
            var result = await _controller.GetAllAsync();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var employees = Assert.IsAssignableFrom<IEnumerable<Employee>>(okResult.Value);
            Assert.Equal(2, employees.Count());
        }

        // ── GET BY ID ────────────────────────────────────────────────────────────

        [Fact]
        public async Task GetEmployeeById_ReturnsOk_WhenEmployeeExists()
        {
            // Act
            var result = await _controller.GetEmployeeById(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var employee = Assert.IsType<Employee>(okResult.Value);
            Assert.Equal(1, employee.Id);
        }

        [Fact]
        public async Task GetEmployeeById_ReturnsNotFound_WhenEmployeeDoesNotExist()
        {
            // Act
            var result = await _controller.GetEmployeeById(99);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        // ── CREATE ───────────────────────────────────────────────────────────────

        [Fact]
        public async Task CreateEmployee_ReturnsCreatedAtAction_AndAddsEmployee()
        {
            // Arrange
            var newEmployee = new Employee { FirstName = "Alice", Position = "Designer", Email = "alice@example.com" };
            var initialCount = EmployeeStubData.Employees.Count;

            // Act
            var result = await _controller.CreateEmployee(newEmployee);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            Assert.Equal(nameof(_controller.GetEmployeeById), createdResult.ActionName);
            Assert.Equal(initialCount + 1, EmployeeStubData.Employees.Count);

            var returned = Assert.IsType<Employee>(createdResult.Value);
            Assert.Equal("Alice", returned.FirstName);
        }

        // ── DELETE ───────────────────────────────────────────────────────────────

        [Fact]
        public async Task DeleteEmployeeById_ReturnsNoContent()
        {
            // Act
            var result = await _controller.DeleteEmployeeById(1);

            // Assert
            Assert.IsType<NoContentResult>(result.Result);
            _mockRepo.Verify(r => r.DeleteEmployeeAsync(1), Times.Once);
        }

        // ── UPDATE ───────────────────────────────────────────────────────────────

        [Fact]
        public async Task UpdateEmployeeAsync_ReturnsCreatedAtAction_WhenIdMatches()
        {
            // Arrange
            var updatedEmployee = new Employee { Id = 1, FirstName = "John Updated", Position = "Senior Engineer", Email = "john@example.com" };

            // Act
            var result = await _controller.UpdateEmployeeAsync(1, updatedEmployee);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var returned = Assert.IsType<Employee>(createdResult.Value);
            Assert.Equal("John Updated", returned.FirstName);
            _mockRepo.Verify(r => r.UpdateEmployeeAsync(updatedEmployee), Times.Once);
        }

        [Fact]
        public async Task UpdateEmployeeAsync_ReturnsBadRequest_WhenIdMismatch()
        {
            // Arrange — route id doesn't match employee body id
            var updatedEmployee = new Employee { Id = 2, FirstName = "John", Email = "john@example.com" };

            // Act
            var result = await _controller.UpdateEmployeeAsync(1, updatedEmployee);

            // Assert
            Assert.IsType<BadRequestResult>(result.Result);
            _mockRepo.Verify(r => r.UpdateEmployeeAsync(It.IsAny<Employee>()), Times.Never);
        }
    }
}
