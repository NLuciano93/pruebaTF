using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace Fusap.Common.Swagger
{
    // ReSharper disable once InconsistentNaming
    public class SwaggerUIConfigureOptions : IConfigureOptions<SwaggerUIOptions>
    {
        private readonly IGroupingProvider _groupingProvider;

        public SwaggerUIConfigureOptions(IGroupingProvider groupingProvider)
        {
            _groupingProvider = groupingProvider;
        }

        public void Configure(SwaggerUIOptions options)
        {
            options.EnableFilter();
            options.RoutePrefix = "docs";

            options.OAuthClientId(string.Empty);
            options.OAuthClientSecret(string.Empty);

            foreach (var groupName in _groupingProvider.GroupNames)
            {
                options.SwaggerEndpoint($"../swagger/{groupName}/swagger.json", groupName);
            }
        }
    }
}