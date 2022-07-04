using FluentValidation;
using Fusap.TimeSheet.Application.Notifications;

namespace Fusap.TimeSheet.Application.UseCases.DescriptionTb.V1.Get
{
    public class GetDescriptionTbValidator : AbstractValidator<GetDescriptionTbRequest>
    {
        public GetDescriptionTbValidator()
        {
            RuleFor(x => x)
                .Must(x => x.TimeSheetMasterId > 0)
                .WithErrorCatalog(ErrorCatalog.TimeSheet.InvalidTimeSheetMasterId);

            RuleFor(x => x)
                .Must(x => x.ProjectId > 0)
                .WithErrorCatalog(ErrorCatalog.TimeSheet.InvalidProjectId);
        }
    }
}
