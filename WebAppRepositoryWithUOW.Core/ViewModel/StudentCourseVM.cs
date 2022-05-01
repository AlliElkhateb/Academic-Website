using System.ComponentModel.DataAnnotations;
using WebAppRepositoryWithUOW.Core.Models;

namespace WebAppRepositoryWithUOW.Core.ViewModel
{
    public class StudentCourseVM
    {
        public int StudentDegree { get; set; }


        [Display(Name = "Student")]
        public int StudentId { get; set; }
        public Student? Student { get; set; }
        public IEnumerable<Student>? Students { get; set; }


        [Display(Name = "Course")]
        public int CourseId { get; set; }
        public Course? Course { get; set; }
        public IEnumerable<Course>? Courses { get; set; }
    }
}
