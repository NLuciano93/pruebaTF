using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Http;

namespace Fusap.Common.Hosting.UserAgent
{
    public class UserAgentInjectorBuilderFilter : IHttpMessageHandlerBuilderFilter
    {
        private readonly IServiceProvider _provider;

        public UserAgentInjectorBuilderFilter(IServiceProvider provider)
        {
            _provider = provider;
        }

        public Action<HttpMessageHandlerBuilder> Configure(Action<HttpMessageHandlerBuilder> next)
        {
            if (next == null)
            {
                throw new ArgumentNullException(nameof(next));
            }

            return builder =>
            {
                next(builder);

                builder.AdditionalHandlers.Add(ActivatorUtilities.CreateInstance<UserAgentInjectorRequestHandler>(_provider));
            };
        }
    }
}