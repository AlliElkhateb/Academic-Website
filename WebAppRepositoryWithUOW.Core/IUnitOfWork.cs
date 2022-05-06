using WebAppRepositoryWithUOW.Core.IRepository;
using WebAppRepositoryWithUOW.Core.Models;

namespace WebAppRepositoryWithUOW.Core
{
    public interface IUnitOfWork : IDisposable
    {
        IBaseRepository<Department> DepartmentRepository { get; }
        IBaseRepository<Student> StudentRepository { get; }
        IBaseRepository<Instructor> InstructorRepository { get; }
        IBaseRepository<Course> CourseRepository { get; }
        IBaseRepository<StudentCourse> StudentCourseRepository { get; }
        void Commit();
    }
}
