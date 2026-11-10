using DonationHub.Domain.Common;
using DonationHub.Domain.Enums;
using DonationHub.Domain.ValueObjects;

namespace DonationHub.Domain.Entities
{
    public class Donation : BaseEntity
    {
        public Guid CampaignId { get;private set; }
        public Guid DonorUserId { get; private set; } 
        public Money Amount { get; private set; } = null!;
        public DonationStatus Status { get; private set; }
    //    public DateTime CreateAt { get; private set; }
        public DateTime? CompletedAt { get; private set; }
        public string? FailureReason { get; private set; }
        public string? ReceiptNumber { get; private set; }

        public string? ReceiptFileName { get; private set; }
        private Donation() { }
        public Donation(
             Campaign campaign,
        Guid donorUserId,
        Money amount,
        decimal minAmount,
        decimal maxAmount,
          DateTime currentTime) : base(currentTime)
        {
            campaign.EnsureCanReceiveDonation(currentTime);
            if (amount.Amount < minAmount ||
           amount.Amount > maxAmount)
            {
                throw new ArgumentException(
                    "Donation amount is outside allowed limits.");
            }
            if (amount.Currency != campaign.GoalAmount.Currency)
                throw new ArgumentException("Currency mismatch.");

            CampaignId = campaign.Id;
            DonorUserId = donorUserId;
            Amount = amount;

            Status = DonationStatus.Pending;
        }
        public void MarkAsSucceeded(Campaign campaign , DateTime currentTime)
        {
            if (Status != DonationStatus.Pending)
                throw new InvalidOperationException(
                    "Donation is not pending.");

            campaign.RegisterSuccessfulDonation(Amount,
               currentTime );

            Status = DonationStatus.Succeeded;
            CompletedAt = currentTime;
        }

        public void MarkAsFailed(string reason, DateTime currentTime)
        {
            if (Status != DonationStatus.Pending)
                throw new InvalidOperationException(
                    "Donation is not pending.");

            if (string.IsNullOrWhiteSpace(reason))
                throw new ArgumentException("Failure reason is required.");

            Status = DonationStatus.Failed;
            FailureReason = reason;
            SetUpdatedAt(currentTime);
        }

    }
}
