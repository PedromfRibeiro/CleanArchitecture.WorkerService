using Microsoft.Extensions.Configuration;

namespace CleanArchitecture.Core.Settings.ConnectedService;

internal static class ConnectedServices
{
    private static IConfiguration _configuration { get => _configuration ??= new ConfigurationBuilder().Build(); set => _configuration = value; }

    internal static T GetExpectedValue<T>(this IConfiguration configuration, string key) => configuration.GetExpectedValue<T>(key) ?? throw new Exception($"Expected configuration {key} is not properly specified");

    internal static string InternetBanking(string key) => _configuration[$"ConnectedServices:InternetBanking:Endpoints:{key}"] ?? throw new KeyNotFoundException($"Key '{key}' not found in configuration.");

    internal static string SmartIzi(string key) => _configuration[$"ConnectedServices:SmartIzi:Endpoints:{key}"] ?? throw new KeyNotFoundException($"Key '{key}' not found in configuration.");
}
