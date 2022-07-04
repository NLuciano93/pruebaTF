using System;
using Microsoft.AspNetCore.Http;

namespace Fusap.Common.Swagger
{
    [AttributeUsage(AttributeTargets.Method)]
    public class ProducesPaginationResponseHeadersAttribute : Attribute
    {
        public int StatusCode { get; }

        public ProducesPaginationResponseHeadersAttribute(int statusCode = StatusCodes.Status200OK)
        {
            StatusCode = statusCode;
        }
    }
}