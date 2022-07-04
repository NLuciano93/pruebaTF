using System;

namespace Fusap.Common.Model
{
    public static class ExceptionExtensions
    {
        public static UnexpectedError<TException> AsFusapError<TException>(this TException exception)
            where TException : Exception
        {
            return (UnexpectedError<TException>)UnexpectedError.FromException(exception);
        }
    }
}
