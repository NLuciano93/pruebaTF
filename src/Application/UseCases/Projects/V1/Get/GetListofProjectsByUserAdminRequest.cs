using Fusap.Common.Mediator;
using Fusap.TimeSheet.Domain.TimeSheetAggregate.Response;

namespace Fusap.TimeSheet.Application.UseCases.Projects.V1.Get
{
    public class GetListofProjectsByUserAdminRequest : Request<ProjectListResponse>
    {
        public int UserId { get; set; }

        public GetListofProjectsByUserAdminRequest(int userId)
        {
            UserId = userId;
        }
    }
}
