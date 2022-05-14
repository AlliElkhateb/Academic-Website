namespace WebAppRepositoryWithUOW.Core.ViewModel
{
    public class InstructorVM
    {
        public Instructor? Instructor { get; set; }
        public IEnumerable<Department>? Departments { get; set; }
        public IEnumerable<Course>? Courses { get; set; }
        public IEnumerable<Student>? Students { get; set; }
    }
}
