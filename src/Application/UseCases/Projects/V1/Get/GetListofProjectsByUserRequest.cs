using Fusap.Common.Mediator;
using Fusap.TimeSheet.Domain.TimeSheetAggregate.Response;

namespace Fusap.TimeSheet.Application.UseCases.Projects.V1.Get
{
    public class GetListofProjectsByUserRequest : Request<ProjectListResponse>
    {
        public int UserId { get; set; }

        public GetListofProjectsByUserRequest(int userId)
        {
            UserId = userId;
        }
    }
}
