using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Fusap.Common.Model.Presenter.WebApi
{
    [ExcludeFromCodeCoverage]
    [ApiConventionType(typeof(FusapApiConventions))]
    [ApiController]
    [ProducesErrorResponseType(typeof(void))]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class FusapApiController : ControllerBase
    {
        /* =============================================
         * Presenter with success status codes shortcuts
         * =============================================
         */

        [NonAction]
        protected ActionResult Accepted(IResult result)
        {
            return Presenter.Accepted(result);
        }

        [NonAction]
        protected ActionResult<TValue> Accepted<TValue>(IResult<TValue> result)
        {
            return Presenter.Accepted(result);
        }

        [NonAction]
        protected ActionResult<TViewModel> AcceptedAs<TViewModel>(IResult result)
        {
            return Presenter.AcceptedAs<TViewModel>(result);
        }

        [NonAction]
        protected ActionResult Created(object? value)
        {
            return Presenter.Present(value, HttpStatusCode.Created);
        }

        [NonAction]
        protected ActionResult Created(IResult result)
        {
            return Presenter.Present(result, HttpStatusCode.Created);
        }

        [NonAction]
        protected ActionResult<TValue> Created<TValue>(IResult<TValue> result)
        {
            return Presenter.Present(result, HttpStatusCode.Created);
        }

        [NonAction]
        protected ActionResult<TViewModel> CreatedAs<TViewModel>(IResult result)
        {
            return Presenter.PresentAs<TViewModel>(result, HttpStatusCode.Created);
        }


        [NonAction]
        protected ActionResult Ok(IResult result)
        {
            return Presenter.Present(result, HttpStatusCode.OK);
        }

        [NonAction]
        protected ActionResult<TValue> Ok<TValue>(IResult<TValue> result)
        {
            return Presenter.Present(result, HttpStatusCode.OK);
        }

        [NonAction]
        protected ActionResult<TViewModel> OkAs<TViewModel>(IResult result)
        {
            return Presenter.PresentAs<TViewModel>(result, HttpStatusCode.OK);
        }


        [NonAction]
        protected ActionResult NoContent(IResult result)
        {
            return Presenter.Present(result, HttpStatusCode.NoContent);
        }


        /* ====================
         * IPresenter shortcuts
         * ====================
         */

        [NonAction]
        protected ActionResult Error(Error error)
        {
            return Presenter.Present(error);
        }

        /* =================
         * IMapper shortcuts
         * =================
         */

        [NonAction]
        protected TDestination Map<TDestination>(object source)
        {
            return Mapper.Map<TDestination>(source);
        }

        [NonAction]
        protected TDestination Map<TSource, TDestination>(TSource source)
        {
            return Mapper.Map<TSource, TDestination>(source);
        }

        [NonAction]
        protected TDestination Map<TSource, TDestination>(TSource source, TDestination destination)
        {
            return Mapper.Map<TSource, TDestination>(source, destination);
        }

        /* ===================
         * IMediator shortcuts
         * ===================
         */

        [NonAction]
        protected Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = new CancellationToken())
        {
            return Mediator.Send(request, cancellationToken);
        }

        /* =========================================
         * Commonly used services property shortcuts
         * =========================================
         */

        private IPresenter? _presenter;
        protected IPresenter Presenter
        {
            get => _presenter ??= HttpContext.RequestServices.GetRequiredService<IPresenter>();
            set => _presenter = value;
        }

        private IMediator? _mediator;
        protected IMediator Mediator
        {
            get => _mediator ??= HttpContext.RequestServices.GetRequiredService<IMediator>();
            set => _mediator = value;
        }

        private IMapper? _mapper;
        protected IMapper Mapper
        {
            get => _mapper ??= HttpContext.RequestServices.GetRequiredService<IMapper>();
            set => _mapper = value;
        }
    }
}
