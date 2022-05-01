namespace WebAppRepositoryWithUOW.Core.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Manager { get; set; }
        public IEnumerable<Student>? Students { get; set; }
        public IEnumerable<Instructor>? Instructors { get; set; }
        public IEnumerable<Course>? Courses { get; set; }
    }
}
