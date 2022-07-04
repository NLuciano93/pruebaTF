using System;
using Microsoft.AspNetCore.Mvc;

namespace Fusap.Common.Model.Presenter.WebApi
{
    public static class PresenterOptionsExtensions
    {
        public static PresenterOptions WithErrorRenderer(this PresenterOptions options, Func<Error, ActionResult?> renderer)
        {
            var previous = options.ErrorRenderer;
            options.ErrorRenderer = (error) =>
            {
                var rendered = renderer(error);
                if (rendered != null)
                {
                    return rendered;
                }

                return previous?.Invoke(error);
            };

            return options;
        }

        public static PresenterOptions WithErrorRenderer<TError>(this PresenterOptions options, Func<TError, ActionResult?> renderer)
            where TError : Error
        {
            return options.WithErrorRenderer(error =>
            {
                if (error is TError te)
                {
                    return renderer(te);
                }

                return null;
            });
        }
    }
}
