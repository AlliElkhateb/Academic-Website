using System.ComponentModel.DataAnnotations;

namespace WebAppRepositoryWithUOW.Core
{
    public class Course
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "name is required"),
         MaxLength(length: 50, ErrorMessage = "name must be less than 50 character")]
        public string? Name { get; set; }
        public int MaxDegree { get; set; }
        public int MinDegree { get; set; }

        [Display(Name = "Department")]
        public int DepartmentId { get; set; }
        public Department? Department { get; set; }
    }
}
