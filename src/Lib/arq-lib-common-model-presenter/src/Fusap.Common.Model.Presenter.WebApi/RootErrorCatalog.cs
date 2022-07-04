using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Fusap.Common.Model.Presenter.WebApi.Tests")]

namespace Fusap.Common.Model.Presenter.WebApi
{
    internal static class RootErrorCatalog
    {
        public static ErrorCatalogEntry Conflict => ("Conflict", "There is a conflict");
        public static ErrorCatalogEntry NotFound => ("Not found", "Resource not found");
        public static ErrorCatalogEntry Invalid => ("Validation", "Invalid request");
        public static ErrorCatalogEntry BusinessRuleViolated => ("Business", "Business rules violated");
        public static ErrorCatalogEntry NotAuthorized => ("Security", "Access denied");
        public static ErrorCatalogEntry UnexpectedError => ("Unexpected error", "An unexpected error happened");
    }
}
