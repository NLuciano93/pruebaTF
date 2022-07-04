using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using JsonConverter = System.Text.Json.Serialization.JsonConverter;
using JsonConverterAttribute = System.Text.Json.Serialization.JsonConverterAttribute;

namespace Fusap.TimeSheet.Domain.TimeSheetAggregate.DataTranferObject
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum EnabledRoles
    {
        [EnumMember(Value = "Leader")]
        Leader = 1,
        [EnumMember(Value = "User")]
        User,
        [EnumMember(Value = "SuperAdmin")]
        SuperAdmin
    };
}
