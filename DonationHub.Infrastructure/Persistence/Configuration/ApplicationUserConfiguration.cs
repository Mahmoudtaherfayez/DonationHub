using DonationHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DonationHub.Infrastructure.Persistence.Configuration
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.Property(u => u.FullName)
                     .HasMaxLength(150)
                     .IsRequired();

            builder.Property(u => u.PreferredLanguage)
                   .HasMaxLength(2)
                   .IsRequired();

            builder.Property(u => u.IsActive)
                   .IsRequired();

            builder.Property(u => u.CreatedAt)
                   .IsRequired();
        }
    }
}
