using Microsoft.AspNetCore.Mvc;
using WebAppRepositoryWithUOW.Core.ModelsMetadata;

namespace WebAppRepositoryWithUOW.Core.Models
{
    [ModelMetadataType(typeof(StudentCourseMetadata))]
    public class StudentCourse
    {
        public int StudentDegree { get; set; }

        public int StudentId { get; set; }

        public Student Student { get; set; }

        public int CoursetId { get; set; }

        public Course Course { get; set; }
    }
}
