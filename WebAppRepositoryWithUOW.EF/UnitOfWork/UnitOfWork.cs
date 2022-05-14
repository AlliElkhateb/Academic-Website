using WebAppRepositoryWithUOW.Core;
using WebAppRepositoryWithUOW.EF.Data;
using WebAppRepositoryWithUOW.EF.Repository;

namespace WebAppRepositoryWithUOW.EF.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            DepartmentRepository = new Repository<Department>(_context);
            StudentRepository = new Repository<Student>(_context);
            InstructorRepository = new Repository<Instructor>(_context);
            CourseRepository = new Repository<Course>(_context);
            StudentCourseRepository = new Repository<StudentCourse>(_context);
        }

        public IRepository<Department> DepartmentRepository { get; private set; }
        public IRepository<Student> StudentRepository { get; private set; }
        public IRepository<Instructor> InstructorRepository { get; private set; }
        public IRepository<Course> CourseRepository { get; private set; }
        public IRepository<StudentCourse> StudentCourseRepository { get; private set; }
        public void Save()
        {
            _context.SaveChanges();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
