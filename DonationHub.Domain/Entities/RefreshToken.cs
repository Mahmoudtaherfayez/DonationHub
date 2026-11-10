namespace DonationHub.Domain.Entities
{
    public class RefreshToken
    {
        public Guid UserId { get; private set; }
        public Guid RefreshTokenId { get; private set; }
        public string TokenHash { get; private set; }
        public DateTime ExpiresAt { get; private set; }
        public DateTime? RevokedAt { get; private set; }
        public Guid? ReplacedByTokenId { get; private set; }

        private RefreshToken() { }
        public RefreshToken(
            Guid userId,
            string tokenHash,
            DateTime expiresAt)
        {
            if (string.IsNullOrEmpty(tokenHash))
                throw new ArgumentException("Token hash is required .");
            if (expiresAt <= DateTime.UtcNow)
                throw new ArgumentException("Expiration date must be in the future.");
            UserId = userId;
            TokenHash = tokenHash;
            ExpiresAt = expiresAt;
        }
        public bool IsExpired()
        {
            return DateTime.UtcNow >= ExpiresAt;
        }
        public bool IsRevoked()
        {
            return RevokedAt.HasValue;
        }
        public bool IsActive()
        {
            return !IsExpired() && !IsRevoked();
        }
        public void Revoke(Guid? replacedByTokenId = null)
        {
            if (!IsActive())
                throw new InvalidOperationException("Token is not active.");

            RevokedAt = DateTime.UtcNow;
            ReplacedByTokenId = replacedByTokenId;
        }
    }
}
