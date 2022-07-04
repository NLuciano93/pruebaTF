using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using OpenTracing;
using OpenTracing.Tag;
using Fusap.Common.Authorization.Client;

namespace Fusap.Common.Mediator.Authorization
{
    public class AuthorizationMiddlewareBase
    {
        private readonly IFusapAuthorizationClient _authorizationClient;
        private readonly IEnumerable<IAuthorizationRequirementsDescriptor> _descriptors;
        private readonly ITracer _tracer;
        private readonly ILogger _logger;

        protected AuthorizationMiddlewareBase(
            IFusapAuthorizationClient authorizationClient,
            IEnumerable<IAuthorizationRequirementsDescriptor> descriptors,
            ITracer tracer, ILogger logger)
        {
            _authorizationClient = authorizationClient;
            _descriptors = descriptors;
            _tracer = tracer;
            _logger = logger;
        }

        protected async ValueTask<bool> ValidateAsync(object request, CancellationToken cancellationToken)
        {
            var requirements = _descriptors.SelectMany(descriptor => descriptor.DescribeRequirements(request));

            var requirementArray = requirements.ToArray();

            if (requirementArray.Length == 0)
            {
                return true;
            }

            var requiredActions = requirementArray.SelectMany(r => r.Actions).ToArray();
            _logger.LogInformation("Authorizing request {RequestName} for actions {RequiredActions}",
                request.GetType().Name, requiredActions);

            var actions = string.Join(", ", requiredActions);
            var spanBuilder = _tracer.BuildSpan($"Authorizing request {request.GetType().Name} ({actions})");
            spanBuilder.WithTag("requestType", request.GetType().FullName);
            spanBuilder.WithTag("requirements", string.Join("; ", requirementArray.Select(x => x.ToString())));

            using var spanContext = spanBuilder.StartActive();

            var result = await _authorizationClient.AuthorizeAsync(requirementArray, cancellationToken);

            if (!result)
            {
                _logger.LogInformation("Authorization denied for request {RequestName}, denied actions: {DeniedActions}",
                    request.GetType().Name, result.Where(x => !x.Successful)
                        .SelectMany(x => x.Requirement.Actions));

                spanContext.Span.SetTag(Tags.Error, true);
                spanContext.Span.SetTag("denied", string.Join(", ", result
                    .Where(x => !x.Successful).Select(x => x.Requirement.ToString())));
            }

            return result;
        }
    }
}
