using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebAppRepositoryWithUOW.Core.Models;
using WebAppRepositoryWithUOW.EF.IdentityModels;
using WebAppRepositoryWithUOW.EF.ModelsConfigurations;

namespace WebAppRepositoryWithUOW.EF
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new CourseConfigurations().Configure(modelBuilder.Entity<Course>());
            new DepartmentConfigurations().Configure(modelBuilder.Entity<Department>());
            new InstructorConfigurations().Configure(modelBuilder.Entity<Instructor>());
            new StudentConfigurations().Configure(modelBuilder.Entity<Student>());
            new StudentCourseConfigurations().Configure(modelBuilder.Entity<StudentCourse>());
            //modelBuilder.Entity<AppUser>(obj =>
            //{
            //    obj.Property(x => x.UserName)
            //       .HasComputedColumnSql("CONCAT(FirstName,'_', LastName)");
            //});
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<StudentCourse> StudentCourses { get; set; }
    }
}
