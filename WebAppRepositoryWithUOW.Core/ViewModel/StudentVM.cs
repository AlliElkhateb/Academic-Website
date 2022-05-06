using System.ComponentModel.DataAnnotations;
using WebAppRepositoryWithUOW.Core.Models;

namespace WebAppRepositoryWithUOW.Core.ViewModel
{
    public class StudentVM
    {
        public int Id { get; set; }


        [Required(ErrorMessage = "name is required"),
         MaxLength(length: 50, ErrorMessage = "name must be less than 50 character")]
        public string Name { get; set; }


        [Range(minimum: 20, maximum: 50, ErrorMessage = "age must be between 20 and 50 years")]
        public int Age { get; set; }


        [Required(ErrorMessage = "Address is required"),
         MaxLength(length: 150, ErrorMessage = "Address must be less than 150 character")]
        public string Address { get; set; }


        [Display(Name = "Department")]
        public int DepartmentId { get; set; }
        public Department? Department { get; set; }
        public IEnumerable<Department>? Departments { get; set; }
        public IEnumerable<StudentCourse>? StudentCourses { get; set; }
        public IEnumerable<Course>? Courses { get; set; }
    }
}
