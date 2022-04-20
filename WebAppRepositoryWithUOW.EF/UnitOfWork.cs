using WebAppRepositoryWithUOW.Core;
using WebAppRepositoryWithUOW.Core.IRepository;
using WebAppRepositoryWithUOW.Core.Models;
using WebAppRepositoryWithUOW.EF.Repository;

namespace WebAppRepositoryWithUOW.EF
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _Context;

        public IBaseRepository<Department> DepartmentRepository { get; }

        public IBaseRepository<Student> StudentRepository { get; }

        public IBaseRepository<Instructor> InstructorRepository { get; }

        public IBaseRepository<Course> CourseRepository { get; }

        public IBaseRepository<StudentCourse> StudentCourseRepository { get; }

        public UnitOfWork(AppDbContext Context)
        {
            _Context = Context;

            DepartmentRepository = new BaseRepository<Department>(_Context);

            StudentRepository = new BaseRepository<Student>(_Context);

            InstructorRepository = new BaseRepository<Instructor>(_Context);

            CourseRepository = new BaseRepository<Course>(_Context);

            StudentCourseRepository = new BaseRepository<StudentCourse>(_Context);
        }

        public async Task SaveChanges()
        {
            await _Context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _Context.Dispose();
        }

    }
}
