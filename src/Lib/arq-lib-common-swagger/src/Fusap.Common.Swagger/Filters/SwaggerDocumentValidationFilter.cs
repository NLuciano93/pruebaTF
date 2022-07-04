using System;
using System.Linq;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Fusap.Common.Swagger
{
    public class SwaggerDocumentValidationFilter : IDocumentFilter
    {
        private readonly SwaggerDocumentValidationOptions _options;

        public SwaggerDocumentValidationFilter(IOptions<SwaggerDocumentValidationOptions> options)
        {
            _options = options.Value;
        }

        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            CheckDuplicatedOperationIds(swaggerDoc);
            CheckOperationSummaries(swaggerDoc);
            CheckOperationParameterDescriptions(swaggerDoc);
            CheckResponsesForOperationsWithSecuritySchemes(swaggerDoc);
            CheckIfExistAtLeastOneNonErrorResponse(swaggerDoc);
            CheckIfDeleteOperationsHave404Responses(swaggerDoc);
        }

        private void CheckDuplicatedOperationIds(OpenApiDocument swaggerDoc)
        {
            if (!_options.RequireSingleOperationIds)
            {
                return;
            }

            var operationIds = swaggerDoc
                .Paths
                .SelectMany(x => x.Value.Operations)
                .Select(x => x.Value.OperationId)
                .ToArray();

            var duplicated = operationIds
                .Where(x => operationIds.Count(y => x == y) > 1)
                .Distinct()
                .ToArray();

            if (duplicated.Any())
            {
                throw new InvalidOperationException(
                    $"The following operation ids are duplicated: {string.Join(", ", duplicated)}. " +
                    "As per OpenApi spec, operation ids must be unique. Please ensure that all your controller methods have unique names " +
                    "even across namespaces and versions. If it is not possible to use unique action method names you can set a custom operation " +
                    "id using attributes.");
            }
        }

        private void CheckOperationSummaries(OpenApiDocument swaggerDoc)
        {
            if (!_options.RequireOperationSummaries)
            {
                return;
            }

            var operations = swaggerDoc
                .Paths
                .SelectMany(x => x.Value.Operations, (a, b) => new { Path = a.Key, Method = b.Key, Operation = b.Value })
                .Where(x => string.IsNullOrWhiteSpace(x.Operation.Summary))
                .ToArray();

            if (operations.Any())
            {
                throw new InvalidOperationException("The following operations are missing summaries: " +
                                                    string.Join(", ", operations.Select(x => x.Method + " " + x.Path)));
            }
        }

        private void CheckOperationParameterDescriptions(OpenApiDocument swaggerDoc)
        {
            if (!_options.RequireOperationParameterDescriptions)
            {
                return;
            }

            var operations = swaggerDoc
                .Paths
                .SelectMany(x => x.Value.Operations,
                    (a, b) => new
                    {
                        Path = a.Key,
                        Method = b.Key,
                        Operation = b.Value,
                        Parameters = b.Value.Parameters
                            .Where(y => string.IsNullOrWhiteSpace(y.Description))
                            .ToArray()
                    })
                .Where(x => x.Parameters.Any())
                .ToArray();

            if (operations.Any())
            {
                throw new InvalidOperationException("The following operations have parameters that are missing descriptions: " +
                                                    string.Join(", ", operations.Select(x => x.Method + " " +
                                                        x.Path + "(" + string.Join(", ", x.Parameters.Select(y => y.Name)) + ")")));
            }
        }

        private void CheckResponsesForOperationsWithSecuritySchemes(OpenApiDocument swaggerDoc)
        {
            if (!_options.Require401ResponseForOperationsWithSecuritySchemes)
            {
                return;
            }

            var operations = swaggerDoc
                .Paths
                .SelectMany(x => x.Value.Operations, (a, b) => new { Path = a.Key, Method = b.Key, Operation = b.Value })
                .Where(x => x.Operation.Security.Count > 0 && x.Operation.Responses.All(y => y.Key != "401"))
                .ToArray();

            if (operations.Any())
            {
                throw new InvalidOperationException("The following operations have security requirements but do not include a possible 401 return: " +
                                                    string.Join(", ", operations.Select(x => x.Method + " " + x.Path)));
            }
        }

        private void CheckIfExistAtLeastOneNonErrorResponse(OpenApiDocument swaggerDoc)
        {
            if (!_options.RequireAtLeastOneNonErrorResponse)
            {
                return;
            }

            var operations = swaggerDoc
                .Paths
                .SelectMany(x => x.Value.Operations, (a, b) => new
                {
                    Path = a.Key,
                    Method = b.Key,
                    Operation = b.Value,
                    ResponseCodes = b.Value
                        .Responses
                        .Select(y =>
                        {
                            if (int.TryParse(y.Key, out var statusCodeInt))
                            {
                                return statusCodeInt;
                            }

                            return -1;
                        })
                        .Where(x => x > -1)
                        .ToArray()
                })
                .Where(x => x.ResponseCodes.All(y => y > 299))
                .ToArray();

            if (operations.Any())
            {
                throw new InvalidOperationException("The following operations do not include a successful response code: " +
                    string.Join(", ", operations.Select(x => x.Method + " " + x.Path + " (" +
                    string.Join(", ", x.ResponseCodes) + ")")));
            }
        }

        private void CheckIfDeleteOperationsHave404Responses(OpenApiDocument swaggerDoc)
        {
            if (!_options.Require404ResponseForDeleteOperations)
            {
                return;
            }

            var operations = swaggerDoc
                .Paths
                .SelectMany(x => x.Value.Operations, (a, b) => new
                {
                    Path = a.Key,
                    Method = b.Key,
                    Operation = b.Value,
                    ResponseCodes = b.Value
                        .Responses
                        .Select(y =>
                        {
                            if (int.TryParse(y.Key, out var statusCodeInt))
                            {
                                return statusCodeInt;
                            }

                            return -1;
                        })
                        .Where(x => x > -1)
                        .ToArray()
                })
                .Where(x => x.Method == OperationType.Delete && x.ResponseCodes.All(y => y != 404))
                .ToArray();

            if (operations.Any())
            {
                throw new InvalidOperationException("The following DELETE operations do not include the 404 response code: " +
                                                    string.Join(", ", operations.Select(x => x.Method + " " + x.Path + " (" +
                                                        string.Join(", ", x.ResponseCodes) + ")")));
            }
        }
    }
}
