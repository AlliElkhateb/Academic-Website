using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebAppRepositoryWithUOW.Core.Models;

namespace WebAppRepositoryWithUOW.EF.ModelsConfigurations
{
    public class DepartmentConfigurations : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder
                .HasKey(x => x.Id);

            builder
                .Property(x => x.Name)
                .HasMaxLength(50);

            builder
                .Property(x => x.Manager)
                .HasMaxLength(50);
        }
    }
}
