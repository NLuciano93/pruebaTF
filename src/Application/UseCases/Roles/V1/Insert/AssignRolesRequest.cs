using System.Collections.Generic;
using Fusap.Common.Mediator;

namespace Fusap.TimeSheet.Application.UseCases.Roles.V1.Insert
{
    public class AssignRolesRequest : Request
    {
        public int RegistrationID { get; set; }
        public List<UserModel> ListOfUsers { get; set; } = default!;
        public int? CreatedBy { get; set; }

        public class UserModel
        {
            public int RegistrationID { get; set; }
            public bool SelectedUsers { get; set; }
        }
    }
}
