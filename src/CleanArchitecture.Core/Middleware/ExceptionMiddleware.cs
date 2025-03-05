using System;
using System.Net;
using System.Text.Json;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Entities.Enum;
using CleanArchitecture.Core.Entities.Errors;
using CleanArchitecture.Core.Interfaces;
using CleanArchitecture.Core.Interfaces.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Core.Middleware;

public class ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env, IServiceLocator _serviceScopeFactoryLocator)
{
    private readonly IServiceScope _scope = _serviceScopeFactoryLocator.CreateScope();

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        bool developmentMode = !env.IsDevelopment();
        logger.LogError(ex, ex.Message);
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        ApiException apiException = new(0, string.Empty, string.Empty);
        LogEntry log = new LogEntry()
        {
            LogLevel = LogLevel.Error,
            OperationType = "Exception",
            Status = "Error",
            Message = ex.Message ?? String.Empty,
            ExceptionMessage = ex.StackTrace ?? String.Empty,
        };
        if (developmentMode)
        {
            apiException = new(context.Response.StatusCode, ex.Message, ex.StackTrace?.ToString());
            await SaveErrorToDatabase(log);
        }
        else
        {
            apiException = new(context.Response.StatusCode, HttpStatusCode.InternalServerError.ToString(), string.Empty);
        }

        await context.Response.WriteAsync(JsonSerializer.Serialize(apiException, new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }));
    }

    private async Task SaveErrorToDatabase(LogEntry log)
    {
        IRepositoryAsync? repository = _scope.ServiceProvider.GetService<IRepositoryAsync>();
        await repository!.AddAsync(log);
    }
}
