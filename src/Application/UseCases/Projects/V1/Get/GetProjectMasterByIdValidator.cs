using FluentValidation;
using Fusap.TimeSheet.Application.Notifications;

namespace Fusap.TimeSheet.Application.UseCases.Projects.V1.Get
{
    public class GetProjectMasterByIdValidator : AbstractValidator<GetProjectMasterByIdRequest>
    {
        public GetProjectMasterByIdValidator()
        {
            RuleFor(x => x)
                .Must(x => x.ProjectId > 0)
                .WithErrorCatalog(ErrorCatalog.TimeSheet.InvalidProjectId);
        }
    }
}
