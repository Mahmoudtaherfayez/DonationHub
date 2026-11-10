using DonationHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DonationHub.Infrastructure.Persistence.Configuration
{
    public class CampaignConfiguration : IEntityTypeConfiguration<Campaign>
    {
        void IEntityTypeConfiguration<Campaign>.Configure(EntityTypeBuilder<Campaign> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.TitleAr)
                .HasMaxLength(200)
                .IsRequired();
            builder.Property(c => c.TitleEn)
                .HasMaxLength(200)
                .IsRequired();
            builder.Property(c => c.DescriptionAr)
                .HasMaxLength(2000)
                .IsRequired();
            builder.Property(c => c.DescriptionEn)
                .HasMaxLength(2000)
                .IsRequired();
            builder.OwnsOne(c => c.GoalAmount, money =>
            {
                money.Property(m => m.Amount)
                 .HasPrecision(18, 2)
                 .HasColumnName("GoalAmount");
                money.Property(m => m.Currency)
                 .HasMaxLength(3)
                 .HasColumnName("GoalCurrency");

            });
            builder.OwnsOne(c => c.RaisedAmount, money =>
            {
               money.Property(m => m.Amount)
                .HasPrecision(18, 2)
                .HasColumnName("RaisedAmount");
                money.Property(m => m.Currency)
                 .HasMaxLength(3)
                 .HasColumnName("RaisedCurrency");

            });
            builder.HasOne<ApplicationUser>()
       .WithMany()
       .HasForeignKey(c => c.CreatedByUserId)
       .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne<Category>()
                .WithMany(c => c.Campaigns)
                .HasForeignKey(c => c.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.Property(c => c.RowVersion)
                .IsRowVersion();
            builder.HasIndex(c => c.CategoryId);
            builder.HasIndex(c => c.Status);
        }
    }
}
