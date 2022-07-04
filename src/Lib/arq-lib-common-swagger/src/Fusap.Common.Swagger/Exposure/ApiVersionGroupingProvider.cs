using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

namespace Fusap.Common.Swagger
{
    public class ApiVersionGroupingProvider : IGroupingProvider
    {
        private readonly IActionDescriptorCollectionProvider _actionDescriptorCollectionProvider;
        private readonly IOptionsFactory<OpenApiInfo> _openApiInfoFactory;

        public ApiVersionGroupingProvider(IActionDescriptorCollectionProvider actionDescriptorCollectionProvider, IOptionsFactory<OpenApiInfo> openApiInfoFactory)
        {
            _actionDescriptorCollectionProvider = actionDescriptorCollectionProvider;
            _openApiInfoFactory = openApiInfoFactory;
        }

        public IEnumerable<string> GroupNames => _actionDescriptorCollectionProvider
            .ActionDescriptors
            .Items
            .SelectMany(GetVersionsForDescriptor)
            .Distinct()
            .OrderBy(x => x.ToString())
            .Select(x => x.ToString().ToLowerInvariant());

        public OpenApiInfo GetApiInfo(string groupName)
        {
            return _openApiInfoFactory.Create(Options.DefaultName);
        }

        public bool IsOnGroup(string groupName, ApiDescription apiDescription)
        {
            if (!ApiVersion.TryParse(groupName, out var version))
            {
                return false;
            }

            var versions = GetVersionsForDescriptor(apiDescription.ActionDescriptor);

            return versions.Any(x => x == version);
        }

        private IEnumerable<ApiVersion> GetVersionsForDescriptor(ActionDescriptor actionDescriptor)
        {
            if (actionDescriptor is ControllerActionDescriptor controllerActionDescriptor)
            {
                var methodAttributes = controllerActionDescriptor.MethodInfo.GetCustomAttributes<ApiVersionAttribute>();
                var classAttributes = controllerActionDescriptor.MethodInfo.DeclaringType?
                    .GetCustomAttributes<ApiVersionAttribute>() ?? Array.Empty<ApiVersionAttribute>();

                var attributes = methodAttributes
                    .Concat(classAttributes)
                    .SelectMany(x => x.Versions)
                    .Distinct();

                if (attributes.Any())
                {
                    return attributes;
                }
            }

            return new[] { ApiVersion.Default };
        }
    }
}