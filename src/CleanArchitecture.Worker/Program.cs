using CleanArchitecture.Core;
using CleanArchitecture.Core.Interfaces;
using CleanArchitecture.Core.Services;
using CleanArchitecture.Infrastructure;
using CleanArchitecture.Worker;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();
builder.Services.AddCoreServices(builder.Configuration);
builder.Services.AddInfrastructureServices(builder.Configuration);

// Infrastructure.ContainerSetup
builder.Services.AddMessageQueues();
builder.Services.AddRepositories();
builder.Services.AddRepositoriesAsync();
builder.Services.AddUrlCheckingServices();

builder.Services.AddSingleton(typeof(ILoggerAdapter<>), typeof(LoggerAdapter<>));
builder.Services.AddSingleton<IEntryPointService, EntryPointService>();
builder.Services.AddSingleton<IServiceLocator, ServiceScopeFactoryLocator>();

var host = builder.Build();
host.Run();
