using FluentValidation;
using Fusap.TimeSheet.Application.Notifications;

namespace Fusap.TimeSheet.Application.UseCases.Projects.V1.Insert
{
    public class AddProjectMasterValidator : AbstractValidator<AddProjectMasterRequest>
    {
        public AddProjectMasterValidator()
        {
            RuleFor(x => x.ProjectName)
                .NotNull()
                .WithErrorCatalog(ErrorCatalog.TimeSheet.ProjectNameIsNull)
                .NotEmpty()
                .WithErrorCatalog(ErrorCatalog.TimeSheet.ProjectNameIsEmpty)
                ;
        }
    }
}
