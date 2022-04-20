using Microsoft.EntityFrameworkCore;
using WebAppRepositoryWithUOW.Core.Models;
using WebAppRepositoryWithUOW.EF.ModelsConfigurations;

namespace WebAppRepositoryWithUOW.EF
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CourseConfigurations).Assembly);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DepartmentConfigurations).Assembly);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(InstructorConfigurations).Assembly);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(StudentConfigurations).Assembly);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(StudentCourseConfigurations).Assembly);
        }

        public DbSet<Course> Courses { get; set; }

        public DbSet<Department> Departments { get; set; }

        public DbSet<Instructor> Instructors { get; set; }

        public DbSet<Student> Students { get; set; }

        public DbSet<StudentCourse> StudentCourses { get; set; }
    }
}
