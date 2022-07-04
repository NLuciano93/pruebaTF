using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace Fusap.Common.Model.Presenter.WebApi
{
    public interface IPresenter
    {
        ActionResult Present(Error error);

        ActionResult Present(object? value, HttpStatusCode successStatusCode);
        ActionResult Present(IResult result, HttpStatusCode successStatusCode);
        ActionResult<TValue> Present<TValue>(IResult<TValue> result, HttpStatusCode successStatusCode);

        ActionResult<TViewModel> PresentAs<TViewModel>(object? value, HttpStatusCode successStatusCode);
        ActionResult<TViewModel> PresentAs<TViewModel>(IResult result, HttpStatusCode successStatusCode);
    }
}
