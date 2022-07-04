using Fusap.Common.Mediator;
using Fusap.TimeSheet.Domain.TimeSheetAggregate.Response;

namespace Fusap.TimeSheet.Application.UseCases.Projects.V1.Insert
{
    public class AddProjectMasterRequest : Request<ProjectIdResponse>
    {
        public string ProjectCode { get; set; } = default!;
        public string NatureofIndustry { get; set; } = default!;
        public string ProjectName { get; set; } = default!;
    }
}
