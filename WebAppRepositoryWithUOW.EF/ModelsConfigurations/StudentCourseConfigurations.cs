using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebAppRepositoryWithUOW.Core.Models;

namespace WebAppRepositoryWithUOW.EF.ModelsConfigurations
{
    public class StudentCourseConfigurations : IEntityTypeConfiguration<StudentCourse>
    {
        public void Configure(EntityTypeBuilder<StudentCourse> builder)
        {
            builder.HasKey(x => new { x.CourseId, x.StudentId });

            builder.HasOne(x => x.Course)
                .WithMany(x => x.StudentCourses)
                .HasForeignKey(x => x.CourseId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Student)
                .WithMany(x => x.StudentCourses)
                .HasForeignKey(x => x.StudentId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
