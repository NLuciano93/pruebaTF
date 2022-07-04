using System;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Fusap.TimeSheet.Api.Controller.Token.V1
{
    [ApiConventionType(typeof(DefaultApiConventions))]
    [ApiVersion("1")]
    public partial class TokenController : TimeSheetController
    {
        private readonly IMediator _mediator;
        private readonly IConfiguration _configuration;

        public TokenController(IMediator mediator, IConfiguration configuration)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _configuration = configuration.GetSection("JwtIssuer");
        }
    }
}
