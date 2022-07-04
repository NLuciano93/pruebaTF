using FluentValidation;
using Fusap.TimeSheet.Application.Notifications;

namespace Fusap.TimeSheet.Application.UseCases.TimeSheets.V1.Get
{
    public class GetTimeSheetMasterByIdValidator : AbstractValidator<GetTimeSheetMasterByIdRequest>
    {
        public GetTimeSheetMasterByIdValidator()
        {
            RuleFor(x => x)
                .Must(x => x.TimeSheetMasterId > 0)
                .WithErrorCatalog(ErrorCatalog.TimeSheet.InvalidTimeSheetMasterId);
        }
    }
}
