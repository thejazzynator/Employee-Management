using Moq;
using Employee_Management.Repositories.Interfaces;
using Employee_Management.Models;

namespace EmployeeTests
{
    public static class EmployeeStubData
    {

        public static Mock<IEmployeeRepository> GetMockEmployeeRepository()
        {
            var mock = new Mock<IEmployeeRepository>();

            mock.Setup(s => s.GetAllEmployeesAsync()).ReturnsAsync(Employees);
            mock.Setup(s => s.GetEmployeeByIdAsync(It.IsAny<int>())).ReturnsAsync((int id) => Employees.FirstOrDefault(e => e.Id == id));
            mock.Setup(s => s.AddEmployeeAsync(It.IsAny<Employee?>())).Returns((Employee? employee) =>
            {
                if (employee != null)
                {
                    employee.Id = Employees.Max(e => e.Id) + 1;
                    Employees.Add(employee);
                }
                return Task.CompletedTask;
            });
            mock.Setup(s => s.DeleteEmployeeAsync(It.IsAny<int>())).Returns(Task.CompletedTask);

            return mock;
        }

        public static List<Employee> Employees = new List<Employee>
        {
            new() { Id = 1, FirstName = "John", Position = "Software Engineer", Email = "john@example.com" },
            new() { Id = 2, FirstName = "Jane", Position = "Project Manager", Email = "jane@example.com" }
        };

    }
}
