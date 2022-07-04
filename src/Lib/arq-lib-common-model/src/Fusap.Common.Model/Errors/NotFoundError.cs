namespace Fusap.Common.Model
{
    public class NotFoundError : Error
    {
        public NotFoundError(ErrorCatalogEntry catalogEntry) : base(catalogEntry)
        {
        }

        public NotFoundError(string code, string message) : base(code, message)
        {
        }
    }
}