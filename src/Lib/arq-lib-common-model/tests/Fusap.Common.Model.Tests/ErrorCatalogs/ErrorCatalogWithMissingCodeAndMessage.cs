namespace Fusap.Common.Model.Tests.ErrorCatalogs
{
    public static class ErrorCatalogWithMissingCodeAndMessage
    {
        public static ErrorCatalogEntry EntryOne => ("", "Error");
        public static ErrorCatalogEntry EntryTwo => ("RN-01", "");
    }
}