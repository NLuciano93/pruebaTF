using Fusap.Common.Mediator;

namespace Fusap.TimeSheet.Application.UseCases.Roles.V1.Get
{
    public class GetRoleIdbyRolenameRequest : Request<int>
    {
        public string RoleName { get; set; }

        public GetRoleIdbyRolenameRequest(string roleName)
        {
            RoleName = roleName;
        }
    }
}
