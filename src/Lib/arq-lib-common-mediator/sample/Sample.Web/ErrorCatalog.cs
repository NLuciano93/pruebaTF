using Fusap.Common.Model;

namespace Fusap.Sample.Web
{
    public static class ErrorCatalog
    {
        public static ErrorCatalogEntry ErrorWithZeroParameters => ("RN-01", "This error has a static message");
        public static ErrorCatalogEntry ErrorWithOneParameter => ("RN-02", "This error has one parameter and it is '{0}'");
        public static ErrorCatalogEntry ErrorWithTwoParameter => ("RN-04", "This error has two parameter and they are '{0}' and '{1}'");
        public static ErrorCatalogEntry ErrorWithThreeParameter => ("RN-03", "This error has three parameter and they are '{0}', '{1}' and '{2}'");
        public static ErrorCatalogEntry ErrorWithFourParameter => ("RN-05", "This error has four parameter and they are '{0}', '{1}', '{2}' and '{3}'");
        public static ErrorCatalogEntry Test => ("RN-06", "This error has four parameter and they are '{0}', '{1}', '{2}' and '{3}'");

        public static class Subdomain
        {
            public static ErrorCatalogEntry InvalidLegalPersonId => ("LP-ASSOCIATE-GEN-01", "Random error description");

            public static ErrorCatalogEntry InvalidUserId => ("LP-ASSOCIATE-GEN-02", "Random error description");

            public static class Configurations
            {
                public static ErrorCatalogEntry NotFound => ("LP-ASSOCIATE-CFG-01", "Random error description");

                public static ErrorCatalogEntry InvalidMinimumApprovals => ("LP-ASSOCIATE-CFG-02", "I see dead people");
                public static ErrorCatalogEntry InvalidAssociatePlanId => ("LP-ASSOCIATE-CFG-03", "Some other error");
            }

            public static class Associations
            {
                public static ErrorCatalogEntry NotFound => ("LP-ASSOCIATE-ASSOC-01", "Random error description");

                public static ErrorCatalogEntry InvalidDocumentType => ("LP-ASSOCIATE-ASSOC-02", "Random error description");
                public static ErrorCatalogEntry InvalidDocumentNumber => ("LP-ASSOCIATE-ASSOC-03", "Random error description");
                public static ErrorCatalogEntry InvalidProspectData => ("LP-ASSOCIATE-ASSOC-04", "Random error description");

                public static ErrorCatalogEntry PersonDoesNotExistAndCannotBeOnboarded => ("LP-ASSOCIATE-ASSOC-05", "Random error description");
            }
        }
    }
}
