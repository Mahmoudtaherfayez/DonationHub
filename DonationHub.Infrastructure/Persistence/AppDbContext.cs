using DonationHub.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DonationHub.Infrastructure.Persistence
{
    public class AppDbContext : IdentityDbContext<ApplicationUser,IdentityRole<Guid>,Guid>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { }
        
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Campaign> Campaigns => Set<Campaign>();
        public DbSet<Donation> Donations => Set<Donation>();
        public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();
        public DbSet<PaymentAttempt> PaymentAttempts => Set<PaymentAttempt>();
        public DbSet<ProcessedStripeEvent> ProcessedStripeEvents => Set<ProcessedStripeEvent>();
        public DbSet<IdempotencyRecord> IdempotencyRecords => Set<IdempotencyRecord>();
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(
                typeof(AppDbContext).Assembly
                );
        }
      
    }
}
