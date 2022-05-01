namespace WebAppRepositoryWithUOW.Core.Models
{
    public class StudentCourse
    {
        public int StudentDegree { get; set; }
        public int StudentId { get; set; }
        public Student? Student { get; set; }
        public int CourseId { get; set; }
        public Course? Course { get; set; }
    }
}
