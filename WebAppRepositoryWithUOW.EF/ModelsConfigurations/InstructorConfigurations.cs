using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebAppRepositoryWithUOW.Core.Models;

namespace WebAppRepositoryWithUOW.EF.ModelsConfigurations
{
    public class InstructorConfigurations : IEntityTypeConfiguration<Instructor>
    {
        public void Configure(EntityTypeBuilder<Instructor> builder)
        {
            builder
                .HasKey(x => x.Id);

            builder
                .Property(x => x.Name)
                .HasMaxLength(50);

            builder
                .Property(x => x.Address)
                .HasMaxLength(150);

            builder
                .HasOne(x => x.Department)
                .WithMany(x => x.Instructors)
                .HasForeignKey(x => x.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(x => x.Course)
                .WithMany(x => x.Instructors)
                .HasForeignKey(x => x.CourseId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
