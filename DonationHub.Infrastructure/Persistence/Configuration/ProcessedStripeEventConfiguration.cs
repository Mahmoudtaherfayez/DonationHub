using DonationHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DonationHub.Infrastructure.Persistence.Configuration
{
    public class ProcessedStripeEventConfiguration : IEntityTypeConfiguration<ProcessedStripeEvent>
    {
        public void Configure(EntityTypeBuilder<ProcessedStripeEvent> builder)
        {
            // Primary Key
            builder.HasKey(e => e.EventId);

            // Event ID
            builder.Property(e => e.EventId)
                   .HasMaxLength(255)
                   .IsRequired();

            // Event Type
            builder.Property(e => e.Type)
                   .HasMaxLength(200)
                   .IsRequired();

            // Processed At
            builder.Property(e => e.ProcessedAt)
                   .IsRequired();
        }
    }
}
