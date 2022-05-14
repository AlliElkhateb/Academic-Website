namespace WebAppRepositoryWithUOW.Core.ViewModel
{
    public class CourseVM
    {
        public Course? Course { get; set; }
        public IEnumerable<Department>? Departments { get; set; }
        public IEnumerable<Instructor>? Instructors { get; set; }
        public IEnumerable<StudentCourse>? StudentCourses { get; set; }
    }
}
