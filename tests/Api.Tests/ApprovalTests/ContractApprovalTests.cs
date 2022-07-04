using System;
using System.Net.Http;
using System.Threading.Tasks;
using ApprovalTests;
using ApprovalTests.Reporters;
using Xunit;

namespace Fusap.TimeSheet.Api.Tests.ApprovalTests
{
    [UseReporter(typeof(ClipboardReporter), typeof(DiffReporter))]
    public sealed class ContractApprovalTests : IClassFixture<CustomWebApplicationFactory<Startup>>, IDisposable
    {
        private readonly HttpClient _httpClient;

        public ContractApprovalTests(CustomWebApplicationFactory<Startup> applicationFactory)
        {
            _httpClient = applicationFactory.CreateClient();
            _httpClient.DefaultRequestHeaders.Add("User-agent", "TestClient");
        }

        [Fact]
        public async Task InternalContract()
        {
            //Arrange

            //Act
            var json = await _httpClient.GetStringAsync("/swagger/internal/swagger.json");

            //Assert
            Approvals.Verify(FixNewLine(json));
        }

        public void Dispose()
        {
            _httpClient?.Dispose();
        }

        private static string FixNewLine(string input)
        {
            return input.Replace("\\r\\n", "\\n");
        }
    }
}
