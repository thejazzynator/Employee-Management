using Microsoft.EntityFrameworkCore;
using Employee_Management.Models;

namespace Employee_Management.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
            
        }
    }
}
