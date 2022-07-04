using System;

namespace Fusap.Common.Model.Tests.ErrorCatalogs
{
    public static class ErrorCatalogWithInvalidProperties
    {
        public static ErrorCatalogEntry EntryOne { get; set; } = ("RN-01", "Error");
        public static string EntryTwo => "RN-02";
#pragma warning disable S2376 // Write-only properties should not be used
        public static ErrorCatalogEntry EntryThree
#pragma warning restore S2376 // Write-only properties should not be used
        {
            set { throw new NotImplementedException(); }
        }
    }
}