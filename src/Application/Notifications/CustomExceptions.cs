using System;
using System.Net.Http;

namespace Fusap.TimeSheet.Application.Notifications
{
    [Serializable]
    public class ApiException : Exception
    {
        public HttpResponseMessage? HttpResponseMessage { get; }

        public ApiException() { }
        public ApiException(string message) : base(message) { }
        public ApiException(string message, Exception inner) : base(message, inner) { }
        public ApiException(HttpResponseMessage httpResponseMessage)
        {
            HttpResponseMessage = httpResponseMessage;
        }

        protected ApiException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
