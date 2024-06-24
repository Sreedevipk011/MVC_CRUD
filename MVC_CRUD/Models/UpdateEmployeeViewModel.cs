using System.ComponentModel.DataAnnotations;

namespace MVC_CRUD.Models
{
    public class UpdateEmployeeViewModel
    {
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }

        public string? Email { get; set; }

        public long? Salary { get; set; }
        public string? Department { get; set; }
        public DateTime? DateOfBirth { get; set; }
    }
}
