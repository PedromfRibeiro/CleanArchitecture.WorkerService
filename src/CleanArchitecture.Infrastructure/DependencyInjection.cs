using CleanArchitecture.Core.Interfaces.Messaging;
using CleanArchitecture.Core.Interfaces;
using CleanArchitecture.Core.Interfaces.Repository;
using CleanArchitecture.Core.Services;
using CleanArchitecture.Infrastructure.Messaging;
using CleanArchitecture.Infrastructure.Persistence.Extensions;
using CleanArchitecture.Infrastructure.Persistence.Repositories;
using CleanArchitecture.Infrastructure.Proxy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using CleanArchitecture.Infrastructure.Persistence;
using CleanArchitecture.Core.Settings;

namespace CleanArchitecture.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDatabase(configuration);
        //services.Configure<EntryPointSettings>(configuration.GetSection("EntryPointSettings"));
        return services;
    }

    public static void AddMessageQueues(this IServiceCollection services)
    {
        services.AddSingleton<IQueueReceiver, InMemoryQueueReceiver>();
        services.AddSingleton<IQueueSender, InMemoryQueueSender>();
    }

    public static void AddRepositories(this IServiceCollection services) => services.AddScoped<IRepository, EfRepository>();

    public static void AddRepositoriesAsync(this IServiceCollection services) => services.AddScoped<IRepositoryAsync, EfRepositoryAsync>();

    public static void AddUrlCheckingServices(this IServiceCollection services)
    {
        services.AddTransient<IUrlStatusChecker, UrlStatusChecker>();
        services.AddTransient<IHttpService, HttpService>();
    }

    //public static async Task<WebApplication> AddPersistenceApplication(this WebApplication app, IServiceScope scope)
    //{
    //    if (app.Environment.IsDevelopment())
    //    {
    //        await new SeedManager(scope.ServiceProvider).InitiateSeeds();
    //    }
    //    return app;
    //}
}
