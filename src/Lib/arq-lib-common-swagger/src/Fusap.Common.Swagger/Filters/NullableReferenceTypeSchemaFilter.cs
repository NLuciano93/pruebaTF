using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Fusap.Common.Swagger
{
    public class NullableReferenceTypeSchemaFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (context.MemberInfo is PropertyInfo property)
            {
                if (!IsNullable(property.PropertyType, property.DeclaringType, property.CustomAttributes))
                {
                    schema.Nullable = false;
                }
            }
            else if (context.ParameterInfo != null)
            {
                if (!IsNullable(context.ParameterInfo.ParameterType, context.ParameterInfo.Member, context.ParameterInfo.CustomAttributes))
                {
                    schema.Nullable = false;
                }
            }
            else
            {
                if (context.Type.IsEnum)
                {
                    return;
                }

                Debug.WriteLine(context.Type.FullName);
            }
        }

        private static bool IsNullable(Type memberType, MemberInfo? declaringType, IEnumerable<CustomAttributeData> customAttributes)
        {
            // Check if it is Nullable<T>
            if (memberType.IsValueType)
            {
                return Nullable.GetUnderlyingType(memberType) != null;
            }

            return CheckNullableAttribute(customAttributes)
                   ?? CheckNullableContextAttribute(declaringType)
                   ?? false;
        }

        private static bool? CheckNullableAttribute(IEnumerable<CustomAttributeData> customAttributes)
        {
            const byte Two = 2;
            var nullable = customAttributes
                .FirstOrDefault(x => x.AttributeType.FullName == "System.Runtime.CompilerServices.NullableAttribute");
            if (nullable != null && nullable.ConstructorArguments.Count == 1)
            {
                var attributeArgument = nullable.ConstructorArguments[0];
                if (attributeArgument.ArgumentType == typeof(byte[]))
                {
                    var args = (ReadOnlyCollection<CustomAttributeTypedArgument>)attributeArgument.Value!;
                    if (args.Count > 0 && args[0].ArgumentType == typeof(byte))
                    {
                        {
                            return (byte)args[0].Value! == Two;
                        }
                    }
                }
                else if (attributeArgument.ArgumentType == typeof(byte))
                {
                    {
                        return (byte)attributeArgument.Value! == Two;
                    }
                }
            }

            return null;
        }

        private static bool? CheckNullableContextAttribute(MemberInfo? declaringType)
        {
            const byte Two = 2;
            for (var type = declaringType; type != null; type = type.DeclaringType)
            {
                var context = type.CustomAttributes
                    .FirstOrDefault(x =>
                        x.AttributeType.FullName == "System.Runtime.CompilerServices.NullableContextAttribute");
                if (context != null &&
                    context.ConstructorArguments.Count == 1 &&
                    context.ConstructorArguments[0].ArgumentType == typeof(byte))
                {
                    {
                        return (byte)context.ConstructorArguments[0].Value! == Two;
                    }
                }
            }

            return null;
        }
    }
}
