using FluentValidation;
using Fusap.TimeSheet.Application.Notifications;

namespace Fusap.TimeSheet.Application.UseCases.Expenses.V1.Insert
{
    public class AddExpenseValidator : AbstractValidator<AddExpenseRequest>
    {
        public AddExpenseValidator()
        {
            When(x => x.PurposeorReason != null, () =>
            {
                RuleFor(x => x.PurposeorReason)
                .NotEmpty()
                .WithErrorCatalog(ErrorCatalog.TimeSheet.InvalidPurposeorReason)
                .MaximumLength(50)
                .WithErrorCatalog(ErrorCatalog.TimeSheet.InvalidPurposeorReasonLength);
            });

            When(x => x.VoucherId != null, () =>
            {
                RuleFor(x => x.VoucherId)
                .NotEmpty()
                .WithErrorCatalog(ErrorCatalog.TimeSheet.InvalidVoucherId)
                .MaximumLength(50)
                .WithErrorCatalog(ErrorCatalog.TimeSheet.InvalidVoucherIdLength);
            });

            When(x => x.Comment != null, () =>
            {
                RuleFor(x => x.Comment)
                .NotEmpty()
                .WithErrorCatalog(ErrorCatalog.TimeSheet.InvalidComment)
                .MaximumLength(100)
                .WithErrorCatalog(ErrorCatalog.TimeSheet.InvalidCommentLength);
            });
        }
    }
}
