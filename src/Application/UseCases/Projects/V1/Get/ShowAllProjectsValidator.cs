using System.Linq;
using FluentValidation;
using FluentValidation.Results;
using Fusap.TimeSheet.Application.Notifications;
using Fusap.TimeSheet.Application.Util;
using Fusap.TimeSheet.Domain.TimeSheetAggregate.Response;

namespace Fusap.TimeSheet.Application.UseCases.Projects.V1.Get
{
    public class ShowAllProjectsValidator : AbstractValidator<ShowAllProjectsRequest>
    {
        public ShowAllProjectsValidator()
        {
            RuleFor(x => x.Offset)
            .GreaterThanOrEqualTo(1)
            .WithErrorCatalog(ErrorCatalog.TimeSheet.InvalidOffset);
        }

        protected override bool PreValidate(ValidationContext<ShowAllProjectsRequest> context, ValidationResult result)
        {
            if (context.InstanceToValidate.SortDirection != null && !Validations.IsValidOrderType(context.InstanceToValidate.SortDirection))
            {
                var error = new ValidationFailure(
                    nameof(context.InstanceToValidate.SortDirection),
                    ErrorCatalog.TimeSheet.InvalidSortDirection.Message,
                    context.InstanceToValidate.SortDirection)
                {
                    ErrorCode = ErrorCatalog.TimeSheet.InvalidSortDirection.Code
                };

                result.Errors.Add(error);
            }

            if (context.InstanceToValidate.SortColumn != null && !IsValidColumnName(context.InstanceToValidate.SortColumn))
            {
                var error = new ValidationFailure(
                    nameof(context.InstanceToValidate.SortColumn),
                    ErrorCatalog.TimeSheet.InvalidSortColumn.Message,
                    context.InstanceToValidate.SortColumn)
                {
                    ErrorCode = ErrorCatalog.TimeSheet.InvalidSortColumn.Code
                };

                result.Errors.Add(error);
            }

            return true;
        }

        private bool IsValidColumnName(string sortColumn)
        {
            return new ProjectMasterResponse()
                .GetType()
                .GetProperties()
                .ToList()
                .ConvertAll(x => x.Name.ToUpper())
                .Contains(sortColumn.ToUpper());
        }
    }
}

