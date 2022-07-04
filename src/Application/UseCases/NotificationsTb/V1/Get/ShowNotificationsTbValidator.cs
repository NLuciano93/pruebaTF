using System.Linq;
using FluentValidation;
using FluentValidation.Results;
using Fusap.TimeSheet.Application.Notifications;
using Fusap.TimeSheet.Application.Util;
using Fusap.TimeSheet.Domain.TimeSheetAggregate.Response;

namespace Fusap.TimeSheet.Application.UseCases.NotificationsTb.V1.Get
{
    public class ShowNotificationsTbValidator : AbstractValidator<ShowNotificationsTbRequest>
    {
        public ShowNotificationsTbValidator()
        {
            RuleFor(x => x.Offset)
            .GreaterThanOrEqualTo(1)
            .WithErrorCatalog(ErrorCatalog.TimeSheet.InvalidOffset);

            When(x => x.Message != null, () =>
            {
                RuleFor(x => x.Message)
                .NotEmpty()
                .WithErrorCatalog(ErrorCatalog.TimeSheet.InvalidMessage)
                .MaximumLength(50)
                .WithErrorCatalog(ErrorCatalog.TimeSheet.InvalidMessageLength);
            });
        }

        protected override bool PreValidate(ValidationContext<ShowNotificationsTbRequest> context, ValidationResult result)
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
            return new NotificationTbResponse()
                .GetType()
                .GetProperties()
                .ToList()
                .ConvertAll(x => x.Name.ToUpper())
                .Contains(sortColumn.ToUpper());
        }
    }
}

