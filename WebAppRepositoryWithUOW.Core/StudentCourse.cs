using System.ComponentModel.DataAnnotations;

namespace WebAppRepositoryWithUOW.Core
{
    public class StudentCourse
    {
        public int StudentDegree { get; set; }

        [Display(Name = "Student")]
        public int StudentId { get; set; }
        public Student? Student { get; set; }

        [Display(Name = "Course")]
        public int CourseId { get; set; }
        public Course? Course { get; set; }
    }
}
