using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Fusap.Common.Swagger;

namespace Example.Api.Tests.ApprovalTests
{
    public class CustomWebApplicationFactory<TStartup>
        : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices((ctx, services) =>
            {
                services.Configure<FusapSwaggerOptions>(opt => opt.UseVersionFromEntryAssembly = false);
            });
        }
    }
}
