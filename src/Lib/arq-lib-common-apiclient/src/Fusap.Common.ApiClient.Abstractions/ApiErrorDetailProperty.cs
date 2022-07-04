using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

namespace Fusap.Common.ApiClient.Abstractions
{
    /// <summary>
    /// A property that can convey extra information when an error happen.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class ApiErrorDetailProperty
    {
        /// <summary>
        /// The property key.
        /// </summary>
        /// <example></example>
        [JsonProperty("key", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string? Key { get; set; } = default!;

        /// <summary>
        /// The property value.
        /// </summary>
        /// <example></example>
        [JsonProperty("value", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string? Value { get; set; } = default!;
    }
}
