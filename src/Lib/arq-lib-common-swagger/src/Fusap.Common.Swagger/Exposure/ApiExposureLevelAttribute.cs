using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Fusap.Common.Swagger
{
    /// <summary>
    /// Indicate the expected exposure level for a given Api.
    /// Please note that this don't actually protect or restrict access.
    /// This information is used only for indicating the correct intention on Swagger.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true)]
    public class ApiExposureLevelAttribute : Attribute
    {
        internal static readonly IEnumerable<ApiExposureLevel> DEFAULT_LEVEL = new ApiExposureLevel[] { ApiExposureLevel.Internal };

        public ApiExposureLevel Level { get; }

        /// <summary>
        /// Indicate the expected exposure level for a given Api.
        /// Please note that this don't actually protect or restrict access.
        /// This information is used only for indicating the correct intention on Swagger.
        /// </summary>
        public ApiExposureLevelAttribute(ApiExposureLevel level = ApiExposureLevel.Internal)
        {
            Level = level;
        }

        public static IEnumerable<ApiExposureLevel> GetLevels(MethodInfo methodInfo)
        {
            var methodAttributes = methodInfo.GetCustomAttributes<ApiExposureLevelAttribute>();
            var classAttributes = methodInfo.DeclaringType?.GetCustomAttributes<ApiExposureLevelAttribute>() ?? Array.Empty<ApiExposureLevelAttribute>();

            var attributes = methodAttributes
                .Concat(classAttributes)
                .Select(x => x.Level)
                .Distinct();

            if (attributes.Any())
            {
                return attributes;
            }

            return DEFAULT_LEVEL;
        }
    }
}