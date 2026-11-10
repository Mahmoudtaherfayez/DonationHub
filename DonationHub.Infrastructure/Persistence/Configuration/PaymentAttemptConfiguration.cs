using DonationHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DonationHub.Infrastructure.Persistence.Configuration
{
    public class PaymentAttemptConfiguration : IEntityTypeConfiguration<PaymentAttempt>
    {
        public void Configure(EntityTypeBuilder<PaymentAttempt> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.StripeSessionId)
                .HasMaxLength(255)
                .IsRequired();
            builder.Property(p => p.StripePaymentIntenId)
                .HasMaxLength(255);
            //Amount Value object
            builder.OwnsOne(p => p.Amount, money =>
            {
                money.Property(m => m.Amount)
                .HasPrecision(18, 2)
                .HasColumnName("Amount");

                money.Property(m => m.Currency)
                .HasMaxLength(3)
                .HasColumnName("Currency");

            });
            builder.Property(p => p.Status)
                 .IsRequired();

            // Failure Code
            builder.Property(p => p.FailureCode)
                   .HasMaxLength(100);

            // Donation Relationship
            builder.HasOne<Donation>()
                   .WithMany()
                   .HasForeignKey(p => p.DonationId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Indexes
            builder.HasIndex(p => p.DonationId);
            builder.HasIndex(p => p.StripeSessionId)
                   .IsUnique();

        }

    }
    
}
