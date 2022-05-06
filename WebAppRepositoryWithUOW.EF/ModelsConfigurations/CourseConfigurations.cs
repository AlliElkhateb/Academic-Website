using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebAppRepositoryWithUOW.Core.Models;

namespace WebAppRepositoryWithUOW.EF.ModelsConfigurations
{
    public class CourseConfigurations : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.Property(x => x.Name)
                .HasMaxLength(50);

            builder.HasOne(x => x.Department)
                .WithMany(x => x.Courses)
                .HasForeignKey(x => x.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
