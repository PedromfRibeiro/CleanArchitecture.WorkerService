using System.Diagnostics;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Interfaces;
using CleanArchitecture.Core.Interfaces.Repository;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Infrastructure;

/// <summary>
/// An ILoggerAdapter implementation that uses Microsoft.Extensions.Logging
/// </summary>
/// <typeparam name="T"></typeparam>
public class LoggerAdapter<T>(ILogger<T> _logger, IRepositoryAsync _repository) : ILoggerAdapter<T>
{
    public void Log(LogLevel logLevel, Exception? exception, string? message, params object?[] args)
    {
        var formattedMessage = string.Format("Status:{0} | Operation:{1} | " + message ?? string.Empty, args);
        _logger.Log(logLevel, exception, formattedMessage);
        SaveLog(logLevel, "Generic", "None", formattedMessage, exception);
    }

    public void LogDebug(Exception? exception, string? message, params object?[] args) => Log(LogLevel.Debug, exception, message, args);

    public void LogError(Exception? exception, string? message, params object?[] args) => Log(LogLevel.Error, exception, message, args);

    public void LogInformation(string message, params object[] args) => Log(LogLevel.Information, null, message, args);

    public void LogQueue(LogLevel logLevel, string operationType, string status, string message, Exception? ex = null) => Log(logLevel, ex, message, status, operationType);

    public void LogTrace(Exception? exception, string? message, params object?[] args) => Log(LogLevel.Trace, exception, message, args);

    public void LogWarning(Exception? exception, string? message, params object?[] args) => Log(LogLevel.Warning, exception, message, args);

    private async void SaveLog(LogLevel logLevel, string operationType, string status, string message, Exception? ex)
    {
        await _repository.AddAsync(new LogEntry
        {
            LogLevel = logLevel,
            OperationType = operationType,
            Status = status,
            Message = message,
            ExceptionMessage = ex?.Message,
            StackTrace = ex?.StackTrace,
        });
    }
}
