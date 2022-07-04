using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Fusap.Common.Swagger
{
    public class SwaggerGenConfigureOptions : IConfigureOptions<SwaggerGenOptions>
    {
        private static readonly Dictionary<string, int> s_httpVerbOrdering = new Dictionary<string, int>
        {
            { "GET", 0 },
            { "POST", 1 },
            { "PUT", 2 },
            { "PATCH", 3 },
            { "DELETE", 4 },
        };
        private static readonly Regex s_versionRegex = new Regex("^(.*\\/)?(v\\d)(\\/.*?)?$");

        private readonly IGroupingProvider _groupingProvider;
        private readonly FusapSwaggerOptions _fusapSwaggerOptions;

        public SwaggerGenConfigureOptions(
            IGroupingProvider groupingProvider,
            IOptions<FusapSwaggerOptions> fusapSwaggerOptions
        )
        {
            _groupingProvider = groupingProvider;
            _fusapSwaggerOptions = fusapSwaggerOptions.Value;
        }

        public void Configure(SwaggerGenOptions options)
        {
            options.DocumentFilter<LowercaseDocumentFilter>();
            options.DocumentFilter<SwaggerDocumentValidationFilter>();
            options.OperationFilter<SecurityRequirementsOperationFilter>();
            options.OperationFilter<ResponseHeadersFilter>();
            options.OperationFilter<SwaggerDefaultValuesFilter>();
            options.OperationFilter<ResponsePaginationHeadersFilter>();
            options.OperationFilter<RequiredRequestBodyFilter>();
            options.OperationFilter<MultipartFormDataExclusiveOperationFilter>();
            options.SchemaFilter<NullableReferenceTypeSchemaFilter>();

            options.DescribeAllParametersInCamelCase();

            IncludeSecurityDefinitions(options);

            options.CustomOperationIds(CustomOperationIdSelector);

            options.DocInclusionPredicate(_groupingProvider.IsOnGroup);

            options.OrderActionsBy(CustomSortKeySelector);

            foreach (var groupName in _groupingProvider.GroupNames)
            {
                options.SwaggerDoc(groupName, _groupingProvider.GetApiInfo(groupName));
            }

            options.MapType<decimal>(() => new OpenApiSchema { Type = "number", Format = "decimal" });

            options.MapType<decimal?>(() => new OpenApiSchema { Type = "number", Format = "decimal", Nullable = true });

            options.UseAllOfForInheritance();
            options.UseAllOfToExtendReferenceSchemas();
            options.UseOneOfForPolymorphism();

            IncludeXmlComments(Assembly.GetExecutingAssembly()?.Location, options);
            IncludeXmlComments(Assembly.GetEntryAssembly()?.Location, options);
        }

        private static string CustomSortKeySelector(ApiDescription apiDescription)
        {
            var match = s_versionRegex.Match(apiDescription.RelativePath);

            if (!match.Success)
            {
                return apiDescription.RelativePath + apiDescription.HttpMethod;
            }

            var verbOrder = s_httpVerbOrdering.ContainsKey(apiDescription.HttpMethod)
                ? s_httpVerbOrdering[apiDescription.HttpMethod]
                : 99;

            var pathWithoutVersion = $"{match.Groups[1].Value}{match.Groups[3].Value} {match.Groups[2].Value} {verbOrder}";

            return pathWithoutVersion;
        }

        private static string? CustomOperationIdSelector(ApiDescription x)
        {
            if (!x.TryGetMethodInfo(out var methodInfo))
            {
                return null;
            }

            var attributes = methodInfo.GetCustomAttributes<HttpMethodAttribute>();
            foreach (var attribute in attributes)
            {
                if (!string.IsNullOrWhiteSpace(attribute.Name))
                {
                    return attribute.Name;
                }
            }

            var name = methodInfo.Name;

            if (name.EndsWith("Async"))
            {
                name = name[0..^5];
            }
            return name;
        }

        private void IncludeSecurityDefinitions(SwaggerGenOptions options)
        {
            foreach (var securityDefinition in _fusapSwaggerOptions.SecurityDefinitions)
            {
                options.AddSecurityDefinition(securityDefinition);
            }
        }

        private static void IncludeXmlComments(string? location, SwaggerGenOptions options)
        {
            if (location == null)
            {
                return;
            }

            var path = Path.GetDirectoryName(location);
            foreach (var file in Directory.GetFiles(path, "*.xml"))
            {
                options.IncludeXmlComments(file);
            }
        }
    }
}
