using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

namespace Fusap.Common.ApiClient.Abstractions
{
    /// <summary>
    /// Indicates that an error happened while processing the request.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class ApiError
    {
        /// <summary>
        /// The general error code.
        /// </summary>
        /// <example>ERR-CODE</example>
        [JsonProperty("code", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string? Code { get; set; } = default!;

        /// <summary>
        /// The trace id for this operation.
        /// </summary>
        /// <example>75c0e295a3c74e888f800292f1abeea9</example>
        [JsonProperty("id", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string? Id { get; set; } = default!;

        /// <summary>
        /// A general description of what happened.
        /// </summary>
        /// <example>General error message</example>
        [JsonProperty("message", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string? Message { get; set; } = default!;

        /// <summary>
        /// A detailed list of errors.
        /// </summary>
        [JsonProperty("errors", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public ICollection<ApiErrorDetail>? Errors { get; set; } = default!;
    }
}
