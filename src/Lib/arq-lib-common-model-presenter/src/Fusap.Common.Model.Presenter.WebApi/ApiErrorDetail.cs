using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Fusap.Common.Model.Presenter.WebApi
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
        public string ErrorCode { get; set; } = default!;

        /// <summary>
        /// The specific error message.
        /// </summary>
        /// <example>Error message</example>
        public string Message { get; set; } = default!;

        /// <summary>
        /// A path that further qualifies this issue.
        /// </summary>
        /// <example></example>
        public string? Path { get; set; }

        /// <summary>
        /// An url that further qualifies this issue.
        /// </summary>
        /// <example>https://example.com/errors/err-code-xxx</example>
        public string? Url { get; set; }

        /// <summary>
        /// A list of properties that can convey extra information to the caller.
        /// </summary>
        public IEnumerable<ApiErrorDetailProperty>? Properties { get; set; }
    }
}
