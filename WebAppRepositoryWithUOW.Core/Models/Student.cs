using Microsoft.AspNetCore.Mvc;
using WebAppRepositoryWithUOW.Core.ModelsMetaData;

namespace WebAppRepositoryWithUOW.Core.Models
{
    [ModelMetadataType(typeof(StudentMetaData))]
    public class Student
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }

        public byte[] Image { get; set; }

        public string Address { get; set; }

        public int DepartmentId { get; set; }

        public Department Department { get; set; }

        public List<StudentCourse> StudentCourses { get; set; }
    }
}
