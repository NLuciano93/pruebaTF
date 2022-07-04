using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace Fusap.Common.Model.Presenter.WebApi
{
    public class PresenterStartupFilter : IStartupFilter
    {
        public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
        {
            return app =>
            {
                // Adds middleware to catch all 404 errors and render them with the appropriate not found response.
                app.Use(async (ctx, nextMiddleware) =>
                {
                    await nextMiddleware();

                    if (ctx.Response.StatusCode == StatusCodes.Status404NotFound && !ctx.Response.HasStarted)
                    {
                        var presenter = ctx.RequestServices.GetRequiredService<IPresenter>();
                        var notFoundResult = presenter.Present(new NotFoundError(RootErrorCatalog.NotFound));
                        var actionContext = new ActionContext(ctx, new RouteData(), new ActionDescriptor());

                        await notFoundResult.ExecuteResultAsync(actionContext);
                    }
                });

                next(app);
            };
        }
    }
}
