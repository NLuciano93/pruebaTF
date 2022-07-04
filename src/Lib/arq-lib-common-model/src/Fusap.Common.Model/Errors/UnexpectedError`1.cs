using System;

namespace Fusap.Common.Model
{
    public class UnexpectedError<TException> : UnexpectedError
        where TException : Exception
    {
        public new TException Exception { get; }

        public UnexpectedError(TException exception) : base(exception)
        {
            Exception = exception;
        }

        public UnexpectedError(TException exception, ErrorCatalogEntry catalogEntry) : base(exception, catalogEntry)
        {
            Exception = exception;
        }

        public UnexpectedError(TException exception, string code, string message) : base(exception, code, message)
        {
            Exception = exception;
        }
    }
}
