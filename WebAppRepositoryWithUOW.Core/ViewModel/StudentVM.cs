namespace WebAppRepositoryWithUOW.Core.ViewModel
{
    public class StudentVM
    {
        public Student? Student { get; set; }
        public IEnumerable<Department>? Departments { get; set; }
        public IEnumerable<StudentCourse>? StudentCourses { get; set; }
        public IEnumerable<Course>? Courses { get; set; }
    }
}
