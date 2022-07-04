namespace Fusap.Common.Model.Tests
{
    public static class ErrorCatalog
    {
        public static ErrorCatalogEntry ErrorWithZeroParameters => ("RN-01", "This error has a static message");
        public static ErrorCatalogEntry ErrorWithOneParameter => ("RN-02", "This error has one parameter and it is '{0}'");
        public static ErrorCatalogEntry ErrorWithTwoParameter => ("RN-04", "This error has two parameter and they are '{0}' and '{1}'");
        public static ErrorCatalogEntry ErrorWithThreeParameter => ("RN-03", "This error has three parameter and they are '{0}', '{1}' and '{2}'");
        public static ErrorCatalogEntry ErrorWithFourParameter => ("RN-05", "This error has four parameter and they are '{0}', '{1}', '{2}' and '{3}'");
        public static ErrorCatalogEntry Test => ("RN-06", "This error has four parameter and they are '{0}', '{1}', '{2}' and '{3}'");

        public static class SubDomain
        {
            public static ErrorCatalogEntry Random1 => ("RN-S1-01", "Random error description");

            public static class SubSubDomain
            {
                public static ErrorCatalogEntry Random2 => ("RN-S1-SS1-01", "Random error description");
            }
        }
    }
}
