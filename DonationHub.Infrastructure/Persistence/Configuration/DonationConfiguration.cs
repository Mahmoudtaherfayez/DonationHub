using DonationHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DonationHub.Infrastructure.Persistence.Configuration
{
    public class DonationConfiguration : IEntityTypeConfiguration<Donation>
    {
        public void Configure(EntityTypeBuilder<Donation> builder)
        {
            builder.HasKey(d => d.Id);

            builder.Property(d => d.Status)
                   .IsRequired();

            builder.Property(d => d.FailureReason)
                   .HasMaxLength(500);

            builder.Property(d => d.ReceiptNumber)
                   .HasMaxLength(100);

            builder.Property(d => d.ReceiptFileName)
                   .HasMaxLength(255);

            builder.Property(d => d.CompletedAt)
                   .IsRequired(false);

            builder.OwnsOne(d => d.Amount, money =>
            {
                money.Property(m => m.Amount)
                     .HasPrecision(18, 2)
                     .HasColumnName("Amount");

                money.Property(m => m.Currency)
                     .HasMaxLength(3)
                     .HasColumnName("Currency");
            });

            builder.HasOne<Campaign>()
                   .WithMany()
                   .HasForeignKey(d => d.CampaignId)
                   .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne<ApplicationUser>()
       .WithMany()
       .HasForeignKey(d => d.DonorUserId)
       .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(d => d.CampaignId);
            builder.HasIndex(d => d.DonorUserId);
        }
    }
}
