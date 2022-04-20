using System.ComponentModel.DataAnnotations;

namespace WebAppRepositoryWithUOW.Core.ModelsMetaData
{
    public class StudentCourseMetaData
    {
        [Display(Name = "Student")]
        public int StudentId { get; set; }



        [Display(Name = "Course")]
        public int CourseId { get; set; }
    }
}
