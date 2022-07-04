using System.Linq;
using FluentValidation;
using FluentValidation.Results;
using Fusap.TimeSheet.Application.Notifications;
using Fusap.TimeSheet.Application.Util;
using Fusap.TimeSheet.Domain.TimeSheetAggregate.Response;

namespace Fusap.TimeSheet.Application.UseCases.Users.V1.Get
{
    public class ShowAllUsersUnderAdminValidator : AbstractValidator<ShowAllUsersUnderAdminRequest>
    {
        public ShowAllUsersUnderAdminValidator()
        {
            RuleFor(x => x.Offset)
            .GreaterThanOrEqualTo(1)
            .WithErrorCatalog(ErrorCatalog.TimeSheet.InvalidOffset);

            When(x => x.Name != null, () =>
            {
                RuleFor(x => x.Name)
                .NotEmpty()
                .WithErrorCatalog(ErrorCatalog.TimeSheet.InvalidName)
                .MaximumLength(100)
                .WithErrorCatalog(ErrorCatalog.TimeSheet.InvalidNameLength);
            });
        }

        protected override bool PreValidate(ValidationContext<ShowAllUsersUnderAdminRequest> context, ValidationResult result)
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
            return new RegistrationSummaryResponse()
                .GetType()
                .GetProperties()
                .ToList()
                .ConvertAll(x => x.Name.ToUpper())
                .Contains(sortColumn.ToUpper());
        }
    }
}
