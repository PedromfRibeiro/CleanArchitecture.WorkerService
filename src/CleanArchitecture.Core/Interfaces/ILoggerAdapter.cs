using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Core.Interfaces;

// Helps if you need to confirm logging is happening
// https://ardalis.com/testing-logging-in-aspnet-core
public interface ILoggerAdapter<T>
{
    void Log(LogLevel logLevel, Exception? exception, string? message, params object?[] args);

    void LogDebug(Exception? exception, string? message, params object?[] args);

    void LogError(Exception? exception, string? message, params object?[] args);

    void LogInformation(string message, params object[] args);

    void LogQueue(LogLevel logLevel, string operationType, string status, string message, Exception? ex = null);

    void LogTrace(Exception? exception, string? message, params object?[] args);

    void LogWarning(Exception? exception, string? message, params object?[] args);
}
