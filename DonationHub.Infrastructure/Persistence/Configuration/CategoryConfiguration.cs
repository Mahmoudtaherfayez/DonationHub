using DonationHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DonationHub.Infrastructure.Persistence.Configuration
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.NameAr)
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(c => c.NameEn)
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(c => c.DescriptionAr)
                   .HasMaxLength(1000);

            builder.Property(c => c.DescriptionEn)
                   .HasMaxLength(1000);
        }
    }
}
