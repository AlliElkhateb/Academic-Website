using WebAppRepositoryWithUOW.Core;
using WebAppRepositoryWithUOW.Core.IRepository;
using WebAppRepositoryWithUOW.Core.Models;
using WebAppRepositoryWithUOW.EF.Repository;

namespace WebAppRepositoryWithUOW.EF
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _Context;

        public UnitOfWork(AppDbContext Context)
        {
            _Context = Context;

            DepartmentRepository = new BaseRepository<Department>(_Context);

            StudentRepository = new BaseRepository<Student>(_Context);

            InstructorRepository = new BaseRepository<Instructor>(_Context);

            CourseRepository = new BaseRepository<Course>(_Context);

            StudentCourseRepository = new BaseRepository<StudentCourse>(_Context);
        }

        public IBaseRepository<Department> DepartmentRepository { get; private set; }

        public IBaseRepository<Student> StudentRepository { get; private set; }

        public IBaseRepository<Instructor> InstructorRepository { get; private set; }

        public IBaseRepository<Course> CourseRepository { get; private set; }

        public IBaseRepository<StudentCourse> StudentCourseRepository { get; private set; }

        public void SaveChanges()
        {
            _Context.SaveChanges();
        }

        public void Dispose()
        {
            _Context.Dispose();
        }
    }
}
