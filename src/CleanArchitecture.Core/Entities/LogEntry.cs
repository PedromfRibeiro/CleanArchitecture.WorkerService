using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Core.Entities;

public class LogEntry : BaseEntity
{
    public string? ExceptionMessage { get; set; }
    public LogLevel LogLevel { get; set; }
    public string Message { get; set; } = string.Empty;
    public string OperationType { get; set; } = string.Empty;
    public string? StackTrace { get; set; }
    public string Status { get; set; } = string.Empty;
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
}
