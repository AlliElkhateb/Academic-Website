namespace WebAppRepositoryWithUOW.Core
{
    public class InstructorWithStudent
    {
        public int StudentDegree { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; }
        public int InstructorId { get; set; }
        public Instructor Instructor { get; set; }
    }
}
