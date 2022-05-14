namespace WebAppRepositoryWithUOW.Core.ViewModel
{
    public class DepartmentVM
    {
        public Department? Department { get; set; }
        public IEnumerable<Student>? Students { get; set; }
        public IEnumerable<Instructor>? Instructors { get; set; }
        public IEnumerable<Course>? Courses { get; set; }
    }
}
