using CleanArchitecture.Core.Entities;

namespace CleanArchitecture.Core.Interfaces;

public interface IUrlStatusChecker
{
    Task<UrlStatusHistory> CheckUrlAsync(string url, string requestId);
}
