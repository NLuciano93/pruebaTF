using System.Collections.Generic;

namespace Fusap.Common.Swagger
{
    public class FusapSwaggerOptions
    {
        public List<FusapSwaggerSecurityDefinition> SecurityDefinitions { get; set; } = new List<FusapSwaggerSecurityDefinition>();

        public bool RedirectHomeToSwaggerUI { get; set; } = true;

        /// <summary>
        /// Indicates if the version of the document should be set to the one specified in the entry assembly.
        /// It is useful to disable this if you are in a testing scenario.
        /// </summary>
        public bool UseVersionFromEntryAssembly { get; set; } = true;
    }
}
