using System.ComponentModel.DataAnnotations;

namespace WebAppRepositoryWithUOW.Core.ModelsMetaData
{
    public class DepartmentMetaData
    {
        public int Id { get; set; }



        [Required(ErrorMessage = "name is required"),
         MaxLength(length: 50, ErrorMessage = "name must be less than 50 character")]
        //[Remote(controller: "Department", action: "NameExist", AdditionalFields = "Id", ErrorMessage = "this name is already exist")]
        public string Name { get; set; }



        [Required(ErrorMessage = "manager name is required"),
         MaxLength(length: 50, ErrorMessage = "manager name must be less than 50 character")]
        public string Manager { get; set; }
    }
}
