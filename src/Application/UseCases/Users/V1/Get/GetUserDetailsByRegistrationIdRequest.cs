using Fusap.Common.Mediator;
using Fusap.TimeSheet.Domain.TimeSheetAggregate.Response;

namespace Fusap.TimeSheet.Application.UseCases.Users.V1.Get
{
    public class GetUserDetailsByRegistrationIdRequest : Request<RegistrationViewDetailsResponse>
    {
        public int RegistrationId { get; set; }

        public GetUserDetailsByRegistrationIdRequest(int registrationId)
        {
            RegistrationId = registrationId;
        }
    }
}
