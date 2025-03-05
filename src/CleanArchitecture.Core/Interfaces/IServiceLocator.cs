using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Core.Interfaces;

public interface IServiceLocator : IDisposable
{
    IServiceScope CreateScope();

    T Get<T>();
}
