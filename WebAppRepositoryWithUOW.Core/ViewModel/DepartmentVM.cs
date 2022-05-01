using System.ComponentModel.DataAnnotations;
using WebAppRepositoryWithUOW.Core.Models;

namespace WebAppRepositoryWithUOW.Core.ViewModel
{
    public class DepartmentVM
    {
        public int Id { get; set; }


        [Required(ErrorMessage = "name is required"),
         MaxLength(length: 50, ErrorMessage = "name must be less than 50 character")]
        //[Remote(controller: "Department", action: "NameExist", AdditionalFields = "Id", ErrorMessage = "this name is already exist")]
        public string Name { get; set; }


        [Required(ErrorMessage = "manager name is required"),
         MaxLength(length: 50, ErrorMessage = "manager name must be less than 50 character"),
         Display(Name = "Manager Name")]
        public string Manager { get; set; }


        public IEnumerable<Student>? Students { get; set; }


        public IEnumerable<Instructor>? Instructors { get; set; }


        public IEnumerable<Course>? Courses { get; set; }
    }
}
