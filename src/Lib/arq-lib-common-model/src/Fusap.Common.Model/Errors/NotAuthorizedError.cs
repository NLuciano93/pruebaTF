namespace Fusap.Common.Model
{
    public class NotAuthorizedError : Error
    {
        private const string ErrorCode = "AUTHORIZATION_ERROR";
        private const string ErrorMessage = "You are not authorized to perform this action";

        public NotAuthorizedError() : base(ErrorCode, ErrorMessage)
        {
        }

        public NotAuthorizedError(ErrorCatalogEntry catalogEntry) : base(catalogEntry)
        {
        }

        public NotAuthorizedError(string code, string message) : base(code, message)
        {
        }
    }
}
