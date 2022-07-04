using System.Collections.Generic;

namespace Fusap.TimeSheet.Domain.TimeSheetAggregate.Response
{
    public class AssignedRolesResponse
    {
        public List<AdminModel> ListofAdmins { get; set; } = default!;
        public List<UserModel> ListofUser { get; set; } = default!;

        public class AdminModel
        {
            public string RegistrationID { get; set; } = default!;
            public string Name { get; set; } = default!;
        }

        public class UserModel
        {
            public int RegistrationID { get; set; }
            public string Name { get; set; } = default!;
            public bool SelectedUsers { get; set; }
            public string AssignToAdmin { get; set; } = default!;
        }
    }
}
