using System.Diagnostics.CodeAnalysis;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Fusap.Common.Model.Presenter.WebApi
{
    public class PresentResultFilter : IActionFilter
    {
        private const int HttpOk = 200;
        private readonly IPresenter _presenter;

        public PresentResultFilter(IPresenter presenter)
        {
            _presenter = presenter;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Result is ObjectResult obj && obj.Value is IResult res)
            {
                context.Result = _presenter.Present(res, (HttpStatusCode)(obj.StatusCode ?? HttpOk));
            }
        }

        [ExcludeFromCodeCoverage]
        public void OnActionExecuting(ActionExecutingContext context)
        {
            // Nothing to do
        }
    }
}
