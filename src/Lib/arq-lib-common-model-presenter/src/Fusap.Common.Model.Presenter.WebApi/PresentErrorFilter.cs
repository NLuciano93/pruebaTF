using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Fusap.Common.Model.Presenter.WebApi
{
    public class PresentErrorFilter : IActionFilter
    {
        private readonly IPresenter _presenter;

        public PresentErrorFilter(IPresenter presenter)
        {
            _presenter = presenter;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Result is ObjectResult res && res.Value is Error err)
            {
                context.Result = _presenter.Present(err);
            }
        }

        [ExcludeFromCodeCoverage]
        public void OnActionExecuting(ActionExecutingContext context)
        {
            // Nothing to do
        }
    }
}
