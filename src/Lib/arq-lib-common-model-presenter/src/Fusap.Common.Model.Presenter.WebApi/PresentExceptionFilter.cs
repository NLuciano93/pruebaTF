using Microsoft.AspNetCore.Mvc.Filters;

namespace Fusap.Common.Model.Presenter.WebApi
{
    public class PresentExceptionFilter : IExceptionFilter
    {
        private readonly IPresenter _presenter;

        public PresentExceptionFilter(IPresenter presenter)
        {
            _presenter = presenter;
        }

        public void OnException(ExceptionContext context)
        {
            context.Result = _presenter.Present(UnexpectedError.FromException(context.Exception));
        }
    }
}
