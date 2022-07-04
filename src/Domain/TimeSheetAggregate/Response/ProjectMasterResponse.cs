namespace Fusap.TimeSheet.Domain.TimeSheetAggregate.Response
{
    public class ProjectMasterResponse
    {
        public int ProjectId { get; set; }
        public string ProjectCode { get; set; } = default!;
        public string NatureofIndustry { get; set; } = default!;
        public string ProjectName { get; set; } = default!;
    }
}
