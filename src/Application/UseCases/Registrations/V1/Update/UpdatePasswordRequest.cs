using Fusap.Common.Mediator;
using Fusap.TimeSheet.Domain.TimeSheetAggregate.Input;

namespace Fusap.TimeSheet.Application.UseCases.Registrations.V1.Update
{
    public class UpdatePasswordRequest : Request
    {
        public int RegistrationId { get; set; }
        public string Password { get; set; }

        public UpdatePasswordRequest(int registrationId, UpdatePasswordInput input)
        {
            RegistrationId = registrationId;
            Password = input.Password;
        }
    }
}
