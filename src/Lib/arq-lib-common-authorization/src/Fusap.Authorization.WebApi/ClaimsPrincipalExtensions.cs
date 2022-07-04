using System;
using System.Linq;
using System.Security.Claims;

namespace Fusap.Common.Authorization.WebApi
{
    public static class ClaimsPrincipalExtensions
    {
        private const string CLIENT_ID_CLAIM = "client_id";

        public static Guid GetSubjectGuid(this ClaimsPrincipal user)
        {
            foreach (var nameClaim in user.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier))
            {
                if (Guid.TryParse(nameClaim.Value, out var guid))
                {
                    return guid;
                }
            }

            return default;
        }

        public static string? GetClientId(this ClaimsPrincipal user)
        {
            var clientId = user.Claims.FirstOrDefault(c => c.Type == CLIENT_ID_CLAIM)?.Value;

            return $"{clientId}";
        }

        public static string? GetCoreAuditId(this ClaimsPrincipal user)
        {
            return $"{user.GetClientId()}:{user.GetSubjectGuid()}";
        }
    }
}
