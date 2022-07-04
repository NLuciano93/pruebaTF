using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

namespace Fusap.Common.Hosting.WebApi.ErrorMiddleware
{
    public class FusapErrorMiddlewareStartupFilter : IStartupFilter
    {
        public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
        {
            return app =>
            {
                app.UseFusapErrorMiddleware();

                next(app);
            };
        }
    }
}