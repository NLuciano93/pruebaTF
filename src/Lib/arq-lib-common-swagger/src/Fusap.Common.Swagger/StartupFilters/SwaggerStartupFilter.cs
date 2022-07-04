using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Fusap.Common.Swagger
{
    public class SwaggerStartupFilter : IStartupFilter
    {
        public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
        {
            return app =>
            {
                next(app);

                app.UseFusapSwagger();

                var fusapSwaggerOptions = app.ApplicationServices.GetService<IOptions<FusapSwaggerOptions>>().Value;
                if (fusapSwaggerOptions.RedirectHomeToSwaggerUI)
                {
                    app.UseFusapSwaggerHomeRedirect();
                }
            };
        }
    }
}
