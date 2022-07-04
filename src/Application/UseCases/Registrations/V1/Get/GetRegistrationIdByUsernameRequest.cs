using Fusap.Common.Mediator;

namespace Fusap.TimeSheet.Application.UseCases.Registrations.V1.Get
{
    public class GetRegistrationIdByUsernameRequest : Request<int>
    {
        public string UserName { get; set; }

        public GetRegistrationIdByUsernameRequest(string userName)
        {
            UserName = userName;
        }
    }
}
