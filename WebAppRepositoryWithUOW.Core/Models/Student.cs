namespace WebAppRepositoryWithUOW.Core.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public byte[]? Image { get; set; }
        public string Address { get; set; }
        public int DepartmentId { get; set; }
        public Department? Department { get; set; }
        public IEnumerable<StudentCourse>? StudentCourses { get; set; }
    }
}
