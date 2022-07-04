namespace Fusap.Common.Authorization.Client
{
    public static class RequirementExtensions
    {
        public static RequirementResult Grant(this Requirement requirement)
        {
            return new RequirementResult(requirement, true);
        }

        public static RequirementResult Deny(this Requirement requirement)
        {
            return new RequirementResult(requirement, false);
        }
    }
}