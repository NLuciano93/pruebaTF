using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Fusap.Common.ApiClient.Abstractions
{
    [ExcludeFromCodeCoverage]
    public class ApiException<TResult> : ApiException
    {
        public TResult Result { get; }

        public ApiException(string message, int statusCode, string? response,
            IReadOnlyDictionary<string, IEnumerable<string>> headers, TResult result,
            Exception? innerException)
            : base(message, statusCode, response, headers, innerException)
        {
            Result = result;
        }
    }
}
