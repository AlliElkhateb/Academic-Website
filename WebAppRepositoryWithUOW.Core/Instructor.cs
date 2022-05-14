using System.ComponentModel.DataAnnotations;

namespace WebAppRepositoryWithUOW.Core
{
    public class Instructor
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "name is required"),
         MaxLength(length: 50, ErrorMessage = "name must be less than 50 character")]
        public string? Name { get; set; }

        [Range(20, 50, ErrorMessage = "age must be between 20 and 50 years")]
        public int Age { get; set; }

        [Required(ErrorMessage = "Address is required"),
         MaxLength(length: 150, ErrorMessage = "Address must be less than 150 character")]
        public string? Address { get; set; }

        [Range(3000, 20000, ErrorMessage = "salary must be greater than 3000 egp and less than 20000 egp")]
        public int Salary { get; set; }

        [Display(Name = "Department")]
        public int DepartmentId { get; set; }
        public Department? Department { get; set; }

        [Display(Name = "Course")]
        public int CourseId { get; set; }
        public Course? Course { get; set; }
    }
}
