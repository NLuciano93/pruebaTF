using System.Net.Http;

namespace Fusap.Common.ApiClient.Abstractions.Tests
{
    public interface ITestClient
    {
        HttpClient HttpClient { get; }
    }
}
