using System.Collections.Generic;

namespace Fusap.TimeSheet.Domain.TimeSheetAggregate.Response
{
    public class ProjectListResponse
    {
        public List<ProjectMasterResponse> ListofProjects { get; set; } = default!;
    }
}
