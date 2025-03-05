namespace CleanArchitecture.Core.Entities.Errors;

public class ApiException(int statusCode, string? message, string? details) : BaseEntity
{
    public string? Details { get; set; } = details;
    public string? Message { get; set; } = message;
    public int StatusCode { get; set; } = statusCode;
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
}
