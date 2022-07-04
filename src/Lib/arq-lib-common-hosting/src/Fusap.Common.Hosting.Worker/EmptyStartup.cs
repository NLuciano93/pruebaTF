using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Fusap.Common.Hosting.Worker
{
    internal class EmptyStartup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            // Nothing to do.
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseRouting();
        }
    }
}
