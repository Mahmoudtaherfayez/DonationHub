using DonationHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DonationHub.Infrastructure.Persistence.Configuration
{
    public class IdempotencyRecordConfiguration : IEntityTypeConfiguration<IdempotencyRecord>
    {
        public void Configure(EntityTypeBuilder<IdempotencyRecord> builder)
        {
            // Primary Key
            builder.HasKey(i => i.Key);

            // Key
            builder.Property(i => i.Key)
                   .HasMaxLength(255)
                   .IsRequired();

            // Request Hash
            builder.Property(i => i.RequestHash)
                   .HasMaxLength(500)
                   .IsRequired();

            // Response Status Code
            builder.Property(i => i.ResponseStatusCode)
                   .IsRequired();

            // Response Body
            builder.Property(i => i.ResponseBody)
                   .IsRequired();

            // Created At
            builder.Property(i => i.CreatedAt)
                   .IsRequired();

            // Expires At
            builder.Property(i => i.ExpiresAt)
                   .IsRequired();

            // User Relationship
            builder.HasOne<ApplicationUser>()
                   .WithMany()
                   .HasForeignKey(i => i.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Index
            builder.HasIndex(i => new { i.UserId, i.ExpiresAt });
        }
    }
}
