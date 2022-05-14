using WebAppRepositoryWithUOW.Core;
using WebAppRepositoryWithUOW.EF.Repository;

namespace WebAppRepositoryWithUOW.EF.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Department> DepartmentRepository { get; }
        IRepository<Student> StudentRepository { get; }
        IRepository<Instructor> InstructorRepository { get; }
        IRepository<Course> CourseRepository { get; }
        IRepository<StudentCourse> StudentCourseRepository { get; }
        void Save();
    }
}
