using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Fusap.Common.Authorization.Client
{
    public class FusapAuthorizationClient : IFusapAuthorizationClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<FusapAuthorizationClient> _logger;
        private readonly FusapAuthorizationClientOptions _options;

        public FusapAuthorizationClient(HttpClient httpClient, IOptions<FusapAuthorizationClientOptions> options, ILogger<FusapAuthorizationClient> logger)
        {
            _options = options.Value;
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<AuthorizationResult> AuthorizeAsync(Requirement[] requirements, CancellationToken cancellationToken = default)
        {
            try
            {
                // Group requirements that can be checked against the same authority.
                var tasks = requirements
                    .GroupBy(GetAuthorityForRequirement)
                    .Select(group => DoAuthorizeAsync(group.Key, group, cancellationToken));

                // When all requests arrive, flatten the list and produce final result.
                var results = (await Task.WhenAll(tasks))
                    .SelectMany(x => x);

                return new AuthorizationResult(results);
            }
            catch (Exception ex)
            {
                var requirementStr = requirements.Select(r => r.ToString());

                _logger.LogCritical(ex, "Failed to authorize requirements {Requirements}", requirementStr);

                throw;
            }
        }

        private async Task<IEnumerable<RequirementResult>> DoAuthorizeAsync(ResourceAuthority authority,
            IEnumerable<Requirement> requirements, CancellationToken cancellationToken = default)
        {
            using var postContent = new StringContent(JsonConvert.SerializeObject(requirements), Encoding.UTF8, "application/json");
            using var response = await _httpClient.PostAsync(authority.ConnectionString, postContent, cancellationToken);

            response.EnsureSuccessStatusCode();

            var jsonResult = await response.Content.ReadAsStringAsync();

            var grants = JsonConvert.DeserializeObject<bool[]>(jsonResult);

            var results = requirements
                .Zip(grants, (req, res) => new RequirementResult(req, res))
                .ToArray();

            return results;
        }

        private ResourceAuthority GetAuthorityForRequirement(Requirement requirement)
        {
            foreach (var authority in _options.Authorities)
            {
                if (!requirement.Resources.All(resource => authority.Resources.Any(prefix => resource.AbsoluteUri.StartsWith(prefix))))
                {
                    continue;
                }

                _logger.LogDebug("Using authority {ResourceAuthorityConnectionString} for requirement {Requirement}", authority.ConnectionString, requirement);

                return authority;
            }

            throw new InvalidOperationException($"There is no authority capable of authorizing all resources of {nameof(requirement)} {requirement}.");
        }
    }
}
