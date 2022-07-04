using System.Collections.Generic;

namespace Fusap.TimeSheet.Domain.TimeSheetAggregate.DataTranferObject
{
    public class AssignProjectToUsersDto
    {
        public int ProjectId { get; set; }
        public List<UserProjectModel> ListOfUsers { get; set; } = default!;
        public int? RegistrationId { get; set; }

        public class UserProjectModel
        {
            public int RegistrationId { get; set; }
            public string Name { get; set; } = default!;
            public bool SelectedUsers { get; set; }
            public string AssignToProject { get; set; } = default!;
            public int ProjectId { get; set; }
        }
    }
}
