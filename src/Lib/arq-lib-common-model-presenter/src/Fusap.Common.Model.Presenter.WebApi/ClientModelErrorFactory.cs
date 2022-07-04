using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Fusap.Common.Model.Presenter.WebApi
{
    public class ClientModelErrorFactory : IClientErrorFactory
    {
        private readonly IPresenter _presenter;

        public ClientModelErrorFactory(IPresenter presenter)
        {
            _presenter = presenter;
        }

        public IActionResult GetClientError(ActionContext actionContext, IClientErrorActionResult clientError)
        {
            return clientError switch
            {
                { StatusCode: StatusCodes.Status404NotFound } =>
                    _presenter.Present(new NotFoundError(RootErrorCatalog.NotFound)),

                { StatusCode: StatusCodes.Status400BadRequest } =>
                    _presenter.Present(new ValidationError(RootErrorCatalog.Invalid)),

                { StatusCode: StatusCodes.Status409Conflict } =>
                    _presenter.Present(new ConflictError(RootErrorCatalog.Conflict)),

                { StatusCode: StatusCodes.Status401Unauthorized } =>
                    _presenter.Present(new NotAuthorizedError(RootErrorCatalog.NotAuthorized)),

                { StatusCode: StatusCodes.Status403Forbidden } =>
                    _presenter.Present(new NotAuthorizedError(RootErrorCatalog.NotAuthorized)),

                { StatusCode: StatusCodes.Status422UnprocessableEntity } =>
                    _presenter.Present(new BusinessRuleViolatedError(RootErrorCatalog.BusinessRuleViolated)),

                _ => new ObjectResult(new ApiError
                {
                    Code = RootErrorCatalog.UnexpectedError.Code,
                    Message = RootErrorCatalog.UnexpectedError.Message,
                })
                { StatusCode = clientError.StatusCode }
            };
        }
    }
}
