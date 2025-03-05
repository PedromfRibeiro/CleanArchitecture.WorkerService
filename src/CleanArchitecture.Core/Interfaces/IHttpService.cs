namespace CleanArchitecture.Core.Interfaces;

public interface IHttpService
{
    Task<int> GetUrlResponseStatusCodeAsync(string url);
}
