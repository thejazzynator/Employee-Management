using Employee_Management.Validfations;
using System.ComponentModel.DataAnnotations;

namespace Employee_Management.Models
{
    public class Employee
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "First Name is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        public string LastName { get; set; }
        
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
        
        [Required(ErrorMessage = "Phone number is required")]
        [RegularExpression(@"^\d{3}-\d{3}-\d{4}$", ErrorMessage = "Invalid Phone Number")]
        public string PhoneNumber { get; set; }
        
        [Required(ErrorMessage = "Position is required")]
        public string Position { get; set; }
    }
}
