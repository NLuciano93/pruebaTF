using System;

namespace Fusap.TimeSheet.Domain.TimeSheetAggregate.DataTranferObject
{
    public class RegistrationDto
    {
        public string Name { get; set; } = default!;
        public string Mobileno { get; set; } = default!;
        public string EmailId { get; set; } = default!;
        public string Username { get; set; } = default!;
        public string Password { get; set; } = default!;
        public string ConfirmPassword { get; set; } = default!;
        public string Gender { get; set; } = default!;
        public DateTime? Birthdate { get; set; }
        public int? RoleId { get; set; }
        public DateTime? DateofJoining { get; set; }
        public int? ForceChangePassword { get; set; }
    }
}
