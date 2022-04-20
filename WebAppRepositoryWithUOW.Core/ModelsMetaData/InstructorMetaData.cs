using System.ComponentModel.DataAnnotations;

namespace WebAppRepositoryWithUOW.Core.ModelsMetaData
{
    public class InstructorMetaData
    {
        [Required(ErrorMessage = "name is required"),
         MaxLength(length: 50, ErrorMessage = "name must be less than 50 character")]
        public string Name { get; set; }



        [Range(minimum: 20, maximum: 50)]
        public int Age { get; set; }



        [Required(ErrorMessage = "Address is required"),
         MaxLength(length: 150, ErrorMessage = "Address must be less than 150 character")]
        public string Address { get; set; }



        [Range(minimum: 3000, maximum: 20000, ErrorMessage = "salary must be greater than 3000 egp")]
        public int Salary { get; set; }



        [Display(Name = "Department")]
        public int DepartmentId { get; set; }



        [Display(Name = "Course")]
        public int CourseId { get; set; }
    }
}
