using Fusap.Common.Mediator;
using Fusap.TimeSheet.Domain.TimeSheetAggregate.Input;

namespace Fusap.TimeSheet.Application.UseCases.Registrations.V1.Update
{
    public class ChangePasswordRequest : Request
    {
        public int RegistrationId { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }

        public ChangePasswordRequest(int registrationId, ChangePasswordInput input)
        {
            RegistrationId = registrationId;
            OldPassword = input.OldPassword;
            NewPassword = input.NewPassword;
        }
    }
}
