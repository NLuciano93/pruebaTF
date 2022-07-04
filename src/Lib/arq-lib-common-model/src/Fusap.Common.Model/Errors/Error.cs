using System;

namespace Fusap.Common.Model
{
    public class Error
    {
        public string Code { get; }
        public string Message { get; }

        public Error(ErrorCatalogEntry catalogEntry)
        {
            Code = catalogEntry.Code;
            Message = catalogEntry.Message;
        }

        public Error(string code, string message)
        {
            Code = code;
            Message = message;
        }

        public static implicit operator Error(Exception exception)
        {
            return UnexpectedError.FromException(exception);
        }

        public override string ToString()
        {
            return $"{Code}: {Message}";
        }
    }
}
