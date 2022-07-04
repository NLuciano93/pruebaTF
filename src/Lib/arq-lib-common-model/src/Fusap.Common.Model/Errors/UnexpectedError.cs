using System;

namespace Fusap.Common.Model
{
    public class UnexpectedError : Error
    {
        private const string ErrorCode = "UNEXPECTED_ERROR";
        private const string ErrorMessage = "An unexpected error happened";

        public Exception? Exception { get; }

        protected UnexpectedError(Exception exception) : this(exception, ErrorCode, ErrorMessage)
        {
        }

        protected UnexpectedError(Exception exception, ErrorCatalogEntry catalogEntry) : base(catalogEntry)
        {
            Exception = exception;
        }

        protected UnexpectedError(Exception exception, string code, string message) : base(code, message)
        {
            Exception = exception;
        }

        public UnexpectedError(ErrorCatalogEntry catalogEntry) : base(catalogEntry)
        {
        }

        public UnexpectedError(string code, string message) : base(code, message)
        {
        }

        public static UnexpectedError FromException(Exception exception)
        {
            var exceptionType = exception.GetType();
            var errorType = typeof(UnexpectedError<>).MakeGenericType(exceptionType);

            return (UnexpectedError)Activator.CreateInstance(errorType, exception);
        }

        public override string ToString()
        {
            return Exception == null
                ? base.ToString()
                : $"{Code}: {Message} ({Exception})";
        }
    }
}
