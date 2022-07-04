using System.Diagnostics.CodeAnalysis;
using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace Fusap.Common.Model.Presenter.WebApi
{
    [ExcludeFromCodeCoverage]
    public static class PresenterExtensions
    {
        public static ActionResult Accepted(this IPresenter presenter, object? value)
        {
            return presenter.Present(value, HttpStatusCode.Accepted);
        }

        public static ActionResult Accepted(this IPresenter presenter, IResult result)
        {
            return presenter.Present(result, HttpStatusCode.Accepted);
        }

        public static ActionResult<TValue> Accepted<TValue>(this IPresenter presenter, IResult<TValue> result)
        {
            return presenter.Present(result, HttpStatusCode.Accepted);
        }

        public static ActionResult<TViewModel> AcceptedAs<TViewModel>(this IPresenter presenter, IResult result)
        {
            return presenter.PresentAs<TViewModel>(result, HttpStatusCode.Accepted);
        }


        public static ActionResult Created(this IPresenter presenter, object? value)
        {
            return presenter.Present(value, HttpStatusCode.Created);
        }
        public static ActionResult Created(this IPresenter presenter, IResult result)
        {
            return presenter.Present(result, HttpStatusCode.Created);
        }

        public static ActionResult<TValue> Created<TValue>(this IPresenter presenter, IResult<TValue> result)
        {
            return presenter.Present(result, HttpStatusCode.Created);
        }

        public static ActionResult<TViewModel> CreatedAs<TViewModel>(this IPresenter presenter, IResult result)
        {
            return presenter.PresentAs<TViewModel>(result, HttpStatusCode.Created);
        }


        public static ActionResult Ok(this IPresenter presenter, object? value)
        {
            return presenter.Present(value, HttpStatusCode.OK);
        }
        public static ActionResult Ok(this IPresenter presenter, IResult result)
        {
            return presenter.Present(result, HttpStatusCode.OK);
        }

        public static ActionResult<TValue> Ok<TValue>(this IPresenter presenter, IResult<TValue> result)
        {
            return presenter.Present(result, HttpStatusCode.OK);
        }

        public static ActionResult<TViewModel> OkAs<TViewModel>(this IPresenter presenter, IResult result)
        {
            return presenter.PresentAs<TViewModel>(result, HttpStatusCode.OK);
        }


        public static ActionResult NoContent(this IPresenter presenter, IResult result)
        {
            return presenter.Present(result, HttpStatusCode.NoContent);
        }
    }
}
