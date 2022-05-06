using WebAppRepositoryWithUOW.Core;
using WebAppRepositoryWithUOW.Core.IRepository;
using WebAppRepositoryWithUOW.Core.Models;
using WebAppRepositoryWithUOW.EF.Repository;

namespace WebAppRepositoryWithUOW.EF
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            DepartmentRepository = new BaseRepository<Department>(_context);
            StudentRepository = new BaseRepository<Student>(_context);
            InstructorRepository = new BaseRepository<Instructor>(_context);
            CourseRepository = new BaseRepository<Course>(_context);
            StudentCourseRepository = new BaseRepository<StudentCourse>(_context);
        }

        public IBaseRepository<Department> DepartmentRepository { get; private set; }
        public IBaseRepository<Student> StudentRepository { get; private set; }
        public IBaseRepository<Instructor> InstructorRepository { get; private set; }
        public IBaseRepository<Course> CourseRepository { get; private set; }
        public IBaseRepository<StudentCourse> StudentCourseRepository { get; private set; }
        public void Commit()
        {
            _context.SaveChanges();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
