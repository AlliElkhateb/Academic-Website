using Microsoft.AspNetCore.Mvc;
using WebAppRepositoryWithUOW.Core.ModelsMetaData;

namespace WebAppRepositoryWithUOW.Core.Models
{
    [ModelMetadataType(typeof(DepartmentMetaData))]
    public class Department
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Manager { get; set; }

        public List<Student> Students { get; set; }

        public List<Instructor> Instructors { get; set; }

        public List<Course> Courses { get; set; }
    }
}
