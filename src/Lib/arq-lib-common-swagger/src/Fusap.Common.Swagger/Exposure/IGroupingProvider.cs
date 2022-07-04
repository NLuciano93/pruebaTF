using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.OpenApi.Models;

namespace Fusap.Common.Swagger
{
    public interface IGroupingProvider
    {
        IEnumerable<string> GroupNames { get; }

        OpenApiInfo GetApiInfo(string groupName);
        bool IsOnGroup(string groupName, ApiDescription apiDescription);
    }
}