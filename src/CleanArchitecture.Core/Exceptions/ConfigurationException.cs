using Microsoft.Extensions.Configuration;

namespace CleanArchitecture.Core.Exceptions;

public class ConfigurationException(string key) : Exception($"Expected configuration {key} is not properly specified")
{ }
