using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Fusap.Common.Model;

namespace Fusap.Common.Mediator
{
    [ExcludeFromCodeCoverage]
    public abstract class HandlerBase
    {
        protected virtual Error HandleException(Exception exception)
        {
            return UnexpectedError.FromException(exception);
        }

        protected static Result Conflict(ErrorCatalogEntry catalogEntry)
        {
            return Result.Failure(new ConflictError(catalogEntry));
        }

        protected static Result NotFound(ErrorCatalogEntry catalogEntry)
        {
            return Result.Failure(new NotFoundError(catalogEntry));
        }

        protected static Result NotAuthorized(ErrorCatalogEntry catalogEntry)
        {
            return Result.Failure(new NotAuthorizedError(catalogEntry));
        }

        protected static Result Invalid(ErrorCatalogEntry catalogEntry)
        {
            return Result.Failure(new ValidationError(new ValidationErrorDetail(catalogEntry)));
        }

        protected static Result Invalid(string propertyName, ErrorCatalogEntry catalogEntry)
        {
            return Result.Failure(new ValidationError(new ValidationErrorDetail(propertyName, catalogEntry)));
        }

        protected static Result Invalid(params ValidationErrorDetail[] details)
        {
            return Result.Failure(new ValidationError(details));
        }

        protected static Result BusinessRuleViolated(ErrorCatalogEntry catalogEntry, IEnumerable<KeyValuePair<string, string>>? properties = null)
        {
            return Result.Failure(new BusinessRuleViolatedError(new BusinessRuleViolation(catalogEntry, properties)));
        }

        protected static Result BusinessRuleViolated(params BusinessRuleViolation[] violations)
        {
            return Result.Failure(new BusinessRuleViolatedError(violations));
        }

        protected static Result Error(Error error)
        {
            return Result.Failure(error);
        }

        protected static Result Error(ErrorCatalogEntry catalogEntry)
        {
            return Result.Failure(catalogEntry);
        }
    }
}
