using System.ComponentModel.DataAnnotations;

namespace WebAppRepositoryWithUOW.Core.ModelsMetadata
{
    public class StudentCourseMetadata
    {
        [Display(Name = "Student")]
        public int StudentId { get; set; }



        [Display(Name = "Course")]
        public int CourseId { get; set; }
    }
}
