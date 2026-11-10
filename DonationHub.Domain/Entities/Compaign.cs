using DonationHub.Domain.Common;
using DonationHub.Domain.Enums;
using DonationHub.Domain.ValueObjects;

namespace DonationHub.Domain.Entities
{
    public class Campaign : BaseEntity
    {
        public string TitleAr { get; private set; }
        public string TitleEn { get; private set; }
        public string DescriptionAr { get; private set; }
        public string DescriptionEn { get; private set; }

        public Money GoalAmount { get; private set; } = null!;
        public Money RaisedAmount { get; private set; } = null!;

        public DateTime EndDate { get; private set; }
        public Guid CategoryId { get; private set; }
        public Guid CreatedByUserId { get; private set; } 
        public CampaignStatus Status { get; private set; }
        public byte[] RowVersion { get; private set; } 
        private Campaign()
        {
            
        }
        public Campaign(
             string titleAr,
        string titleEn,
        string descriptionAr,
        string descriptionEn,
        Money goalAmount,
        DateTime endDate,
        Guid categoryId,
        Guid createdByUserId
            )
        {
            if (goalAmount.Amount <= 0)
                throw new ArgumentException("Goal amount must be greater than zero.");

            if (endDate <= DateTime.UtcNow)
                throw new ArgumentException("End date must be in the future.");
            TitleAr = titleAr;
            TitleEn = titleEn;
            DescriptionAr = descriptionAr;
            DescriptionEn = descriptionEn;

            GoalAmount = goalAmount;
            RaisedAmount = new Money(0, goalAmount.Currency);

            EndDate = endDate;
            CategoryId = categoryId;
            CreatedByUserId = createdByUserId;

            Status = CampaignStatus.Active;
        }
        public void UpdateGoalAmount(Money newGoalAmount, DateTime currentTime)
        {
            if(newGoalAmount.Amount <= 0)
            {
                throw new ArgumentException("Amount must be more zero");
            }
            if (newGoalAmount.Currency != GoalAmount.Currency)
                throw new ArgumentException("Amount must be same Currency");

            if (newGoalAmount.Amount < RaisedAmount.Amount)
                throw new InvalidOperationException();
            GoalAmount = newGoalAmount;
            SetUpdatedAt(currentTime);
        }
        public void UpdateEndDate(DateTime newEndDate,DateTime currentTime)
        {
            if (newEndDate <= DateTime.UtcNow)
                throw new ArgumentException("End date must be in the future ");
            if(Status != CampaignStatus.Active)
                throw new InvalidOperationException("Campaign is not active.");
            EndDate = newEndDate;
            SetUpdatedAt(currentTime);
        }
        public void EnsureCanReceiveDonation(DateTime currentTime)
        {
            if (Status != CampaignStatus.Active)
                throw new InvalidOperationException("Campaign is not active.");

            if (EndDate <= currentTime)
                throw new InvalidOperationException("Campaign has expired.");
        }

        public void RegisterSuccessfulDonation(Money amount, DateTime currentTime)
        {
            EnsureCanReceiveDonation(currentTime);

            if (amount.Currency != RaisedAmount.Currency)
                throw new ArgumentException("Currency mismatch.");

            RaisedAmount = new Money(
                RaisedAmount.Amount + amount.Amount,
                RaisedAmount.Currency);

            if (RaisedAmount.Amount >= GoalAmount.Amount)
                Status = CampaignStatus.Completed;

            SetUpdatedAt(currentTime);
        }

    }
}
