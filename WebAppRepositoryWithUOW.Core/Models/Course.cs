namespace WebAppRepositoryWithUOW.Core.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int MaxDegree { get; set; }
        public int MinDegree { get; set; }
        public int DepartmentId { get; set; }
        public Department? Department { get; set; }
        public IEnumerable<StudentCourse>? StudentCourses { get; set; }
        public IEnumerable<Instructor>? Instructors { get; set; }
    }
}
