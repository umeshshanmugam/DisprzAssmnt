using System.ComponentModel.DataAnnotations;

namespace DisprzAssmnt.Models
{
    public class Employee
    {
        [Required]
        public int EmployeeID { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(50)]
        public string LastName { get; set; }

        [EmailAddress]
        public string? EmailAddress { get; set; }

    }

}
