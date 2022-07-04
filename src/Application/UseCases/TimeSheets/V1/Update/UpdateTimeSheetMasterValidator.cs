using FluentValidation;
using Fusap.TimeSheet.Application.Notifications;

namespace Fusap.TimeSheet.Application.UseCases.TimeSheets.V1.Update
{
    public class UpdateTimeSheetMasterValidator : AbstractValidator<UpdateTimeSheetMasterRequest>
    {
        public UpdateTimeSheetMasterValidator()
        {
            RuleFor(x => x)
                .Must(x => x.TimeSheetMasterId > 0)
                .WithErrorCatalog(ErrorCatalog.TimeSheet.RegistrationIdNegative);

            RuleFor(x => x)
                .Must(x => x.TotalHours >= 0)
                .WithErrorCatalog(ErrorCatalog.TimeSheet.TotalHoursNegative);
        }
    }
}
