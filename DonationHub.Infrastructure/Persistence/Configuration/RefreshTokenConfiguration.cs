using DonationHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DonationHub.Infrastructure.Persistence.Configuration
{
    public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.HasKey(r => r.RefreshTokenId);
            builder.Property(r => r.TokenHash)
                .HasMaxLength(500)
                .IsRequired();
            builder.HasIndex(r => r.TokenHash)
                .IsUnique();
            builder.Property(r => r.ExpiresAt)
                .IsRequired();
            builder.Property(r => r.RevokedAt)
                .IsRequired();
            builder.HasOne<ApplicationUser>()
                .WithMany()
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            //index
            builder.HasIndex(r => r.UserId);

        }
    }
}
