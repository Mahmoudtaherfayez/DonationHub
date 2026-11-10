namespace DonationHub.Domain.Entities
{
    public class ProcessedStripeEvent
    {
        public string EventId { get; private set; } = null!;

        public string Type { get; private set; } = null!;

        public DateTime ProcessedAt { get; private set; }

        private ProcessedStripeEvent() { }

        public ProcessedStripeEvent(string eventId, string type)
        {
            if (string.IsNullOrWhiteSpace(eventId))
                throw new ArgumentException("Event ID is required.");

            if (string.IsNullOrWhiteSpace(type))
                throw new ArgumentException("Event type is required.");

            EventId = eventId;
            Type = type;
            ProcessedAt = DateTime.UtcNow;
        }
    }
    }
