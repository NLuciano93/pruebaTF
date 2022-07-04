using System.Net.Http;

namespace Fusap.Common.ApiClient.Abstractions.Tests
{
    public class TestClient : ITestClient
    {
        public HttpClient HttpClient { get; }

        public TestClient(HttpClient httpClient)
        {
            HttpClient = httpClient;
        }
    }
}
