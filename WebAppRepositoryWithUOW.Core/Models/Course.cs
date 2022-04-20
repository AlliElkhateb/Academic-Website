using Microsoft.AspNetCore.Mvc;
using WebAppRepositoryWithUOW.Core.ModelsMetaData;

namespace WebAppRepositoryWithUOW.Core.Models
{
    [ModelMetadataType(typeof(CourseMetaData))]
    public class Course
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int MaxDegree { get; set; }

        public int MinDegree { get; set; }

        public int DepartmentId { get; set; }

        public Department Department { get; set; }

        public List<StudentCourse> StudentCourses { get; set; }

        public List<Instructor> Instructors { get; set; }
    }
}
