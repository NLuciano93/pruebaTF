using System;
using System.Diagnostics.CodeAnalysis;

namespace Fusap.Common.Authorization.Client
{
    [ExcludeFromCodeCoverage]
    public static class FusapResources
    {
        public static Uri Root { get; } = new Uri("urn:Fusap:resource");

        public static Uri AccountsRoot { get; } = new Uri($"{Root.AbsoluteUri}:account");

        public static Uri PersonsRoot { get; } = new Uri($"{Root.AbsoluteUri}:person");

        public static Uri Account(Guid accountId)
        {
            return new Uri($"{AccountsRoot.AbsoluteUri}:{accountId}");
        }

        public static Uri Person(Guid personId)
        {
            return new Uri($"{PersonsRoot.AbsoluteUri}:{personId}");
        }
    }
}