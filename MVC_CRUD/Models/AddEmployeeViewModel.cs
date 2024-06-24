using System.ComponentModel.DataAnnotations;

namespace MVC_CRUD.Models
{
    public class AddEmployeeViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required,DataType(DataType.EmailAddress)]

        public string Email { get; set; }

        public long Salary { get; set; }
        public string Department { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
