using global::CleanArchitecture.Core.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Core.Services;

/// <summary>
/// A wrapper around ServiceScopeFactory to make it easier to fake out with MOQ.
/// </summary>
/// <see cref="https://stackoverflow.com/a/53509491/54288"/>
public sealed class ServiceScopeFactoryLocator(IServiceScopeFactory _factory) : IServiceLocator
{
    private IServiceScope? _scope;

    public IServiceScope CreateScope()
    {
        // if (_scope == null) comment this out to avoid {"Cannot access a disposed object.\r\nObject name: 'IServiceProvider'."}
        _scope = _factory.CreateScope();
        return _scope;
    }

    public void Dispose()
    {
        _scope?.Dispose();
        _scope = null;
    }

    public T Get<T>()
    {
        CreateScope();
        T? service = _scope!.ServiceProvider.GetService<T>();
        return service == null ? throw new InvalidOperationException($"Service of type {typeof(T).Name} not found.") : service;
    }
}
