using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebAppRepositoryWithUOW.Core.Models;

namespace WebAppRepositoryWithUOW.EF.ModelsConfigurations
{
    public class StudentConfigurations : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
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
                .WithMany(x => x.Students)
                .HasForeignKey(x => x.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
