using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API.Data.Configurations
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.ToTable("Users");

            builder.Property(u => u.FirstName)
                .HasMaxLength(100)
                .IsRequired(false);

            builder.Property(u => u.LastName)
                .HasMaxLength(100)
                .IsRequired(false);

            builder.Property(u => u.FullName)
                .HasMaxLength(201)
                .HasComputedColumnSql("[FirstName] + ' ' + [LastName]");

            // سایر تنظیمات...
        }
    }
}