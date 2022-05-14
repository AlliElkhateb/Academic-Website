namespace WebAppRepositoryWithUOW.Core.ViewModel
{
    public class StudentCourseVM
    {
        public StudentCourse? StudentCourse { get; set; }
        public IEnumerable<Student>? Students { get; set; }
        public IEnumerable<Course>? Courses { get; set; }
    }
}
