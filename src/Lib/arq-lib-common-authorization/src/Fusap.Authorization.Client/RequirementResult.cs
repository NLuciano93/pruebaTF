using System;
using System.Collections.Generic;

namespace Fusap.Common.Authorization.Client
{
    public class RequirementResult
    {
        public Requirement Requirement { get; }

        public bool Successful { get; }

        public RequirementResult(Requirement requirement, bool successful)
        {
            Requirement = requirement ?? throw new System.ArgumentNullException(nameof(requirement));
            Successful = successful;
        }

        public static implicit operator bool(RequirementResult result)
        {
            return result.Successful;
        }

        public override bool Equals(object? obj)
        {
            return obj is RequirementResult result &&
                   EqualityComparer<Requirement>.Default.Equals(Requirement, result.Requirement) &&
                   Successful == result.Successful;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Requirement, Successful);
        }
    }
}