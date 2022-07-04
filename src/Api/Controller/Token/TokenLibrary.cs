using System;

namespace Fusap.TimeSheet.Api.Controller.Token
{
    public static class TokenLibrary
    {
        public static string? GetToken(string authorization)
        {
            if (string.IsNullOrEmpty(authorization))
            {
                return null;
            }

            if (authorization.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
            {
                return authorization["Bearer ".Length..].Trim();
            }

            return null;
        }
    }
}
