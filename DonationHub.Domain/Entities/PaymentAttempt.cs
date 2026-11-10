using DonationHub.Domain.Common;
using DonationHub.Domain.Enums;
using DonationHub.Domain.ValueObjects;

namespace DonationHub.Domain.Entities
{
    public class PaymentAttempt : BaseEntity
    {
        public Guid DonationId { get; private set; }
        public string StripeSessionId { get; private set; } = null!;
        public string ? StripePaymentIntenId { get; private set; }
        public Money Amount { get; private set; } = null;
        public PaymentAttemptStatus Status { get; private set; }
        public string? FailureCode { get; private set; }
        private PaymentAttempt() { }
        public PaymentAttempt(
            Guid donationId,
            string stripeSessionId,
            Money amount)
        {
            if (string.IsNullOrEmpty(stripeSessionId))
                throw new ArgumentException("Stripe session ID is required");
            if (amount.Amount <= 0)
                throw new ArgumentException("Amount must be greater than zero");
            DonationId = donationId;
            StripeSessionId = stripeSessionId;
            Amount = amount;
            Status = PaymentAttemptStatus.Created;
            
        }

        public void MarkAsSucceeded (string? paymentIntentId)
        {
            if(Status != PaymentAttemptStatus.Created)
                throw new InvalidOperationException(
                "Payment attempt is not in Created state.");
            StripePaymentIntenId = paymentIntentId;
            Status = PaymentAttemptStatus.Succeeded;
            UpdateAt = DateTime.UtcNow;

        }
        public void MarkAsFailed(string failurecode)
        {
            if (Status != PaymentAttemptStatus.Created)
                throw new InvalidOperationException(
                "Payment attempt is not in Created state.");
            if (string.IsNullOrWhiteSpace(failurecode))
                throw new ArgumentException("Failure code is required.");
            FailureCode = failurecode;
            Status = PaymentAttemptStatus.Failed;
            UpdateAt = DateTime.UtcNow;
        }
        public void MarkAsExpired()
        {
            if (Status != PaymentAttemptStatus.Created)
                throw new InvalidOperationException(
                    "Payment attempt is not in Created state.");

            Status = PaymentAttemptStatus.Expired;

            UpdateAt = DateTime.UtcNow;
        }
    }
}
