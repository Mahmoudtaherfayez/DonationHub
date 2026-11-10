namespace DonationHub.Domain.Entities;

public class IdempotencyRecord
{
    public string Key { get; private set; } = null!;
    public Guid UserId { get; private set; }
    public string RequestHash { get; private set; } = null!;
    public int ResponseStatusCode { get; private set; }
    public string ResponseBody { get; private set; } = null!;
    public DateTime CreatedAt { get; private set; }
    public DateTime ExpiresAt { get; private set; }

    private IdempotencyRecord() { }

    public IdempotencyRecord(
        string key,
        Guid userId,
        string requestHash,
        int responseStatusCode,
        string responseBody,
        DateTime expiresAt)
    {
        if (string.IsNullOrWhiteSpace(key))
            throw new ArgumentException("Key is required.");

        if (string.IsNullOrWhiteSpace(requestHash))
            throw new ArgumentException("Request hash is required.");

        if (responseStatusCode < 100 || responseStatusCode > 599)
            throw new ArgumentException("Invalid HTTP status code.");

        if (expiresAt <= DateTime.UtcNow)
            throw new ArgumentException("Expiration date must be in the future.");

        Key = key;
        UserId = userId;
        RequestHash = requestHash;
        ResponseStatusCode = responseStatusCode;
        ResponseBody = responseBody;
        CreatedAt = DateTime.UtcNow;
        ExpiresAt = expiresAt;
    }

    public bool IsExpired()
    {
        return DateTime.UtcNow >= ExpiresAt;
    }

    public bool MatchesRequest(string requestHash)
    {
        return RequestHash == requestHash;
    }
}