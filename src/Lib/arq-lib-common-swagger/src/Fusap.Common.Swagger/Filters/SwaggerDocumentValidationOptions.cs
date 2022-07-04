namespace Fusap.Common.Swagger
{
    public class SwaggerDocumentValidationOptions
    {
        public bool RequireSingleOperationIds { get; set; } = true;
        public bool RequireOperationSummaries { get; set; } = true;
        public bool RequireOperationParameterDescriptions { get; set; } = true;
        public bool Require401ResponseForOperationsWithSecuritySchemes { get; set; } = true;
        public bool Require404ResponseForDeleteOperations { get; set; } = true;
        public bool RequireAtLeastOneNonErrorResponse { get; set; } = true;
    }
}
