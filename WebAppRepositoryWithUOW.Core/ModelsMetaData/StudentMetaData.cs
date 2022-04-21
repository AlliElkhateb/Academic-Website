using System.ComponentModel.DataAnnotations;

namespace WebAppRepositoryWithUOW.Core.ModelsMetadata
{
    public class StudentMetadata
    {
        [Required(ErrorMessage = "name is required"),
         MaxLength(length: 50, ErrorMessage = "name must be less than 50 character")]
        public string Name { get; set; }



        [Range(minimum: 20, maximum: 50)]
        public int Age { get; set; }



        [Required(ErrorMessage = "Address is required"),
         MaxLength(length: 150, ErrorMessage = "Address must be less than 150 character")]
        public string Address { get; set; }



        [Display(Name = "Department")]
        public int DepartmentId { get; set; }
    }
}
