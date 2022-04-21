using Microsoft.AspNetCore.Mvc;
using WebAppRepositoryWithUOW.Core.ModelsMetadata;

namespace WebAppRepositoryWithUOW.Core.Models
{
    [ModelMetadataType(typeof(InstructorMetadata))]
    public class Instructor
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }

        public byte[] Image { get; set; }

        public string Address { get; set; }

        public int Salary { get; set; }

        public int DepartmentId { get; set; }

        public Department Department { get; set; }

        public int CourseId { get; set; }

        public Course Course { get; set; }
    }
}
