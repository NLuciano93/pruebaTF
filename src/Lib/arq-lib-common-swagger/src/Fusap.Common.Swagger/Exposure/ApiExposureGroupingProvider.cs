using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

namespace Fusap.Common.Swagger
{
    public class ApiExposureGroupingProvider : IGroupingProvider
    {
        private readonly IActionDescriptorCollectionProvider _actionDescriptorCollectionProvider;
        private readonly IOptionsFactory<OpenApiInfo> _openApiInfoFactory;

        public ApiExposureGroupingProvider(IActionDescriptorCollectionProvider actionDescriptorCollectionProvider, IOptionsFactory<OpenApiInfo> openApiInfoFactory)
        {
            _actionDescriptorCollectionProvider = actionDescriptorCollectionProvider;
            _openApiInfoFactory = openApiInfoFactory;
        }

        public IEnumerable<string> GroupNames => _actionDescriptorCollectionProvider
            .ActionDescriptors
            .Items
            .SelectMany(GetLevelsForDescriptor)
            .Distinct()
            .OrderBy(x => (int)x)
            .Select(x => x.ToString().ToLowerInvariant());

        public OpenApiInfo GetApiInfo(string groupName)
        {
            var apiInfo = _openApiInfoFactory.Create(Options.DefaultName);

            if (Enum.TryParse<ApiExposureLevel>(groupName, true, out var level))
            {
                var note = level switch
                {
                    ApiExposureLevel.Internal => "This Api is intended for private consumption over dedicated links.",
                    ApiExposureLevel.External => "This Api is intended for private consumption over the Internet.",
                    ApiExposureLevel.Open => "This Api is intended for public consumption.",
                    _ => string.Empty
                };

                var newDescription = apiInfo.Description + Environment.NewLine + Environment.NewLine + note;

                apiInfo.Description = newDescription.Trim();
            }

            return apiInfo;
        }

        public bool IsOnGroup(string groupName, ApiDescription apiDescription)
        {
            if (!Enum.TryParse<ApiExposureLevel>(groupName, true, out var level))
            {
                return false;
            }

            var levels = GetLevelsForDescriptor(apiDescription.ActionDescriptor);

            return levels.Any(x => x == level);
        }

        private IEnumerable<ApiExposureLevel> GetLevelsForDescriptor(ActionDescriptor actionDescriptor)
        {

            if (actionDescriptor is ControllerActionDescriptor controllerActionDescriptor)
            {
                return ApiExposureLevelAttribute.GetLevels(controllerActionDescriptor.MethodInfo);
            }

            return ApiExposureLevelAttribute.DEFAULT_LEVEL;
        }
    }
}