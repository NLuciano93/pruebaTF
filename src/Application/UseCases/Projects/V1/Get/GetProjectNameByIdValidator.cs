using FluentValidation;
using Fusap.TimeSheet.Application.Notifications;

namespace Fusap.TimeSheet.Application.UseCases.Projects.V1.Get
{
    public class GetProjectNameByIdValidator : AbstractValidator<GetProjectNameByIdRequest>
    {
        public GetProjectNameByIdValidator()
        {
            RuleFor(x => x)
                .Must(x => x.ProjectId > 0)
                .WithErrorCatalog(ErrorCatalog.TimeSheet.InvalidProjectId);
        }
    }
}
