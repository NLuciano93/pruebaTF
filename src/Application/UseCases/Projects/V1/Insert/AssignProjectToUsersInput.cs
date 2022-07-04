using System.Collections.Generic;

namespace Fusap.TimeSheet.Application.UseCases.Projects.V1.Insert
{
    public class AssignProjectToUsersInput
    {
        public List<ProjectModel> ListOfProjects { get; set; } = default!;
        public List<UserProjectModel> ListOfUsers { get; set; } = default!;
        public int? RegistrationId { get; set; }
        public int? CreatedBy { get; set; }

        public class UserProjectModel
        {
            public int RegistrationId { get; set; }
            public string Name { get; set; } = default!;
            public bool SelectedUsers { get; set; }
            public string AssignToProject { get; set; } = default!;
            public int ProjectId { get; set; }
        }

        public class ProjectModel
        {
            public string ProjectId { get; set; } = default!;
            public string ProjectName { get; set; } = default!;
        }
    }
}
