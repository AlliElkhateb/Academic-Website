using Microsoft.AspNetCore.Mvc;
using WebAppRepositoryWithUOW.Core.ModelsMetaData;

namespace WebAppRepositoryWithUOW.Core.Models
{
    [ModelMetadataType(typeof(StudentCourseMetaData))]
    public class StudentCourse
    {
        public int StudentDegree { get; set; }

        public int StudentId { get; set; }

        public Student Student { get; set; }

        public int CoursetId { get; set; }

        public Course Course { get; set; }
    }
}
