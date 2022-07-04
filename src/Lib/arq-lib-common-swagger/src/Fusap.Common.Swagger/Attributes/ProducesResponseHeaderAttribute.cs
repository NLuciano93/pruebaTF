using System;

namespace Fusap.Common.Swagger
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class ProducesResponseHeaderAttribute : Attribute
    {
        public ProducesResponseHeaderAttribute(string name, int statusCode)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            StatusCode = statusCode;
            Type = HeaderResponseType.String;
        }

        public string Name { get; set; }
        public int StatusCode { get; set; }
        public HeaderResponseType Type { get; set; }
        public string? Description { get; set; }
    }
}