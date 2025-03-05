using CleanArchitecture.Core.Exceptions;
using Microsoft.Extensions.Configuration;

namespace CleanArchitecture.Core.Extensions;

public static class ConfigurationExtensions
{
    public static T GetExpectedValue<T>(this IConfiguration configuration, string key) => configuration.GetExpectedValue<T>(key) ?? throw new ConfigurationException(key);
}
