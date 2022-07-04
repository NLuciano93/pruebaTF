namespace Fusap.Common.Model.Tests.ErrorCatalogs
{
    public static class ErrorCatalogWithDuplicatedCodes
    {
        public static ErrorCatalogEntry EntryOne => ("RN-01", "Error");
        public static ErrorCatalogEntry EntryTwo => ("RN-01", "Same Error");
    }
}