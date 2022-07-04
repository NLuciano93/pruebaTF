using System.Collections.Generic;
using Fusap.Common.Swagger;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Fusap.TimeSheet.Api.Tests.ApprovalTests
{
    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            // Add any configuration that is required.
            // The main purpose here is to add default values for configurations that will throw an exception
            // if no value is set, like databases.
            // No connection should be attempted in any case, so the value must simply just not be null.

            builder.ConfigureAppConfiguration((ctx, configurationBuilder) =>
            {
                configurationBuilder.AddInMemoryCollection(new Dictionary<string, string>()
                {
                    { "Cors:AllowAll:Origins", "*" },
                    { "Cors:AllowAll:Methods", "*" },
                    { "Cors:AllowAll:Headers", "*" },
                    { "Cors:AllowAll:ExposedHeaders", "*" },
                    { "Cors:AllowDownload:Origins", "*" },
                    { "Cors:AllowDownload:Methods", "*" },
                    { "Cors:AllowDownload:Headers", "*" },
                    { "Cors:AllowDownload:ExposedHeaders", "Content-Disposition" },
                    { "Jwt:PublicKeyValue:Modulus", "z6uvmwVVgkaeyoarLHlNhuMTyeFD4X+jWHfyJ31uB9qVEwWPssEe4AujNcE2208S0MNa1TBKP2KixVdbN+fX6RquUF+GN2Al3vN25FHQOO0l9Ho1N9KBdTuGsFRlJ4CnF0tZGTXq0CEBETi8KbOUKUQuQBycU1wZL83N3B1SIeYsUZEBze/+9WTmfMtvCUIsLsRysR1OcfCxFVp6vyK9zZlS6wi0DPS7gi4mBYXF26kYen+bFR8ByupJrTftUP5Y8KuDFfLDCSVpjMp4ZLpW60DQiL06ZktiWx1lWzesdA4WZVsdz3li7V3P367mbjV2CeWTAp4LvcZg1AIYdhBwsw==" },
                    { "Jwt:PublicKeyValue:Exponent", "AQAB" },
                });
            });

            // Override any service configuration or registration.
            // This is executed after the application is fully configured but before the host starts.
            // The main purpose here is to remove services that would try to make external connections, that are not
            // required to produce the swagger definition or that would cause the swagger definition to change in
            // unexpected ways.

            builder.ConfigureServices((ctx, services) =>
            {
                // The swagger definition contains the application version which includes the commit hash of the code running,
                // since this will change all the time, we should not add the assembly version on the test environment.

                services.Configure<FusapSwaggerOptions>(opt => opt.UseVersionFromEntryAssembly = false);
            });
        }
    }
}
