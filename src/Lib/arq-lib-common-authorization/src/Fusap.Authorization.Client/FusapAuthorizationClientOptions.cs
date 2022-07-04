using System;

namespace Fusap.Common.Authorization.Client
{
    public class FusapAuthorizationClientOptions
    {
        public ResourceAuthority[] Authorities { get; set; } = Array.Empty<ResourceAuthority>();
    }
}