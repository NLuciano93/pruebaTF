using System;
using Microsoft.OpenApi.Models;

namespace Fusap.Common.Swagger
{
    public class SecuritySchemeAttribute : Attribute
    {
        public SecuritySchemeType Value { get; set; }

        public SecuritySchemeAttribute(SecuritySchemeType value)
        {
            Value = value;
        }
    }
}
