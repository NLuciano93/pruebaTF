using Fusap.Common.Mediator;
using Fusap.TimeSheet.Domain.TimeSheetAggregate.Response;

namespace Fusap.TimeSheet.Application.UseCases.Token.V1.GetById
{
    public class GetUserRequest : Request<UserDetailResponse>
    {
        public string Username { get; set; } = default!;
    }
}
