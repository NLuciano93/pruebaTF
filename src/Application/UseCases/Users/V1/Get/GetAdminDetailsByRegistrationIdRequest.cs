using Fusap.Common.Mediator;
using Fusap.TimeSheet.Domain.TimeSheetAggregate.Response;

namespace Fusap.TimeSheet.Application.UseCases.Users.V1.Get
{
    public class GetAdminDetailsByRegistrationIdRequest : Request<RegistrationViewDetailsResponse>
    {
        public int RegistrationId { get; set; }

        public GetAdminDetailsByRegistrationIdRequest(int registrationId)
        {
            RegistrationId = registrationId;
        }
    }
}
