using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Fusap.Common.Mediator;
using Fusap.TimeSheet.Domain.TimeSheetAggregate.DataTranferObject;
using Fusap.TimeSheet.Domain.TimeSheetAggregate.Response;
using JsonConverter = System.Text.Json.Serialization.JsonConverter;
using JsonConverterAttribute = System.Text.Json.Serialization.JsonConverterAttribute;

namespace Fusap.TimeSheet.Application.UseCases.Registrations.V1.Insert
{
    public class AddRegistrationRequest : Request<RegistrationIdResponse>
    {
        public string Name { get; set; } = default!;
        public string Mobileno { get; set; } = default!;
        public string EmailId { get; set; } = default!;
        public string Username { get; set; } = default!;
        public string Password { get; set; } = default!;
        public string ConfirmPassword { get; set; } = default!;
        public string Gender { get; set; } = default!;
        public DateTime? Birthdate { get; set; }

        [EnumDataType(typeof(EnabledRoles))]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public EnabledRoles Roles { get; set; }
    }
}
