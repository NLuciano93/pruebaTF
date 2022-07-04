using Fusap.Common.Mediator;
using Fusap.Common.Model;
using Fusap.TimeSheet.Domain.TimeSheetAggregate.Response;

namespace Fusap.TimeSheet.Application.UseCases.Users.V1.Get
{
    public class ShowAllUsersRequest : Request<IPagination<RegistrationSummaryResponse>>
    {
        public string? Name { get; set; }
        public string? SortColumn { get; set; }
        public string? SortDirection { get; set; }
        public int Offset { get; set; }
    }
}
