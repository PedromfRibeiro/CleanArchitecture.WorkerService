namespace CleanArchitecture.Core.Entities;

public class UrlStatusHistory : BaseEntity
{
    public DateTime RequestDateUtc { get; } = DateTime.UtcNow;
    public string? RequestId { get; set; }
    public int StatusCode { get; set; }
    public string? Uri { get; set; }

    public override string ToString()
    {
        return $"Fetched {Uri} at {RequestDateUtc.ToLocalTime()} with status code {StatusCode}.";
    }
}
