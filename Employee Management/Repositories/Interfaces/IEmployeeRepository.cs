using Employee_Management.Models;

namespace Employee_Management.Repositories.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetAllEmployeesAsync();
        Task<Employee?> GetEmployeeByIdAsync(int id);
        Task AddEmployeeAsync(Employee? employee);
        Task UpdateEmployeeAsync(Employee employee);
        Task DeleteEmployeeAsync(int id);
    }
}

// Sets up the contract to be used by the EmployeeRepository class. It defines the methods that the repository must implement to perform CRUD operations on Employee entities. Each method is asynchronous, returning a Task, which allows for non-blocking database operations.
