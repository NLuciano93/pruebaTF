using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace Fusap.Common.Hosting.WebApi.Versioning
{
    public class VersionPrefixConvention : IApplicationModelConvention
    {
        private const string VERSION_PREFIX_TEMPLATE = "v{version:apiVersion}";
        private readonly AttributeRouteModel _routePrefix = new AttributeRouteModel(new RouteAttribute(VERSION_PREFIX_TEMPLATE));

        public void Apply(ApplicationModel application)
        {
            foreach (var selector in application.Controllers.SelectMany(c => c.Selectors))
            {
                if (selector.AttributeRouteModel == null)
                {
                    selector.AttributeRouteModel = _routePrefix;
                }
                else if (!selector.AttributeRouteModel.Template.Contains("{version:"))
                {
                    selector.AttributeRouteModel = AttributeRouteModel.CombineAttributeRouteModel(_routePrefix, selector.AttributeRouteModel);
                }
            }
        }
    }
}
