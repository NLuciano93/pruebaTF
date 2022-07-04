using Fusap.Common.Mediator;
using Fusap.Common.Model;
using Fusap.TimeSheet.Domain.TimeSheetAggregate.Response;

namespace Fusap.TimeSheet.Application.UseCases.Projects.V1.Get
{
    public class ShowAllProjectsRequest : Request<IPagination<ProjectMasterResponse>>
    {
        public string? ProjectCode { get; set; }
        public string? ProjectName { get; set; }
        public string? SortColumn { get; set; }
        public string? SortDirection { get; set; }
        public int Offset { get; set; }
    }
}
