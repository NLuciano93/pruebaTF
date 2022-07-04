using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

namespace Fusap.Common.ApiClient.Abstractions
{
    /// <summary>
    /// Indicates details about an error.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class ApiErrorDetail
    {
        /// <summary>
        /// The specific error code.
        /// </summary>
        /// <example>ERR-CODE-XXX</example>
        [JsonProperty("errorCode", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string? ErrorCode { get; set; } = default!;

        /// <summary>
        /// The specific error message.
        /// </summary>
        /// <example>Error message</example>
        [JsonProperty("message", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string? Message { get; set; } = default!;

        /// <summary>
        /// A path that further qualifies this issue.
        /// </summary>
        /// <example></example>
        [JsonProperty("path", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string? Path { get; set; } = default!;

        /// <summary>
        /// An url that further qualifies this issue.
        /// </summary>
        /// <example>https://example.com/errors/err-code-xxx</example>
        [JsonProperty("url", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string? Url { get; set; } = default!;

        /// <summary>
        /// A list of properties that can convey extra information to the caller.
        /// </summary>
        [JsonProperty("properties", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public ICollection<ApiErrorDetailProperty>? Properties { get; set; } = default!;
    }
}
