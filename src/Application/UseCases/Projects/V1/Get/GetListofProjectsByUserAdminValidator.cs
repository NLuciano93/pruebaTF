using FluentValidation;
using Fusap.TimeSheet.Application.Notifications;

namespace Fusap.TimeSheet.Application.UseCases.Projects.V1.Get
{
    public class GetListofProjectsByUserAdminValidator : AbstractValidator<GetListofProjectsByUserAdminRequest>
    {
        public GetListofProjectsByUserAdminValidator()
        {
            RuleFor(x => x)
                .Must(x => x.UserId > 0)
                .WithErrorCatalog(ErrorCatalog.TimeSheet.InvalidUserId);
        }
    }
}
