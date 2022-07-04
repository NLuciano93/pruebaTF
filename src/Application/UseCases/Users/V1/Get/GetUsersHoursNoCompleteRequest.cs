using System;
using Fusap.Common.Mediator;
using Fusap.Common.Model;
using Fusap.TimeSheet.Domain.TimeSheetAggregate.Response;

namespace Fusap.TimeSheet.Application.UseCases.Users.V1.Get
{
    public class GetUsersHoursNoCompleteRequest : Request<IPagination<UsersHoursNoCompleteResponse>>
    {
        public DateTime? Date { get; set; }
        public string? SortColumn { get; set; }
        public string? SortDirection { get; set; }
        public int Offset { get; set; }
    }
}
