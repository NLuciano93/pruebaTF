using System;

namespace Fusap.TimeSheet.Domain.TimeSheetAggregate.Response
{
    public class UserDetailResponse
    {
        public string Name { get; set; } = default!;
        public string Mobileno { get; set; } = default!;
        public string EmailID { get; set; } = default!;
        public string Username { get; set; } = default!;
        public string Gender { get; set; } = default!;
        public DateTime Birthdate { get; set; } = default!;
        public string Rolename { get; set; } = default!;
    }
}
