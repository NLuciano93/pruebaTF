using System;

namespace Fusap.Common.Authorization.Client
{
    public class ResourceAuthority
    {
        public string[] Resources { get; set; } = Array.Empty<string>();
        public Uri ConnectionString { get; set; } = default!;
    }
}