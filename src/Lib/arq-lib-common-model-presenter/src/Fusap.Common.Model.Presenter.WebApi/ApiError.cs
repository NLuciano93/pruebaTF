using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace Fusap.Common.Model.Presenter.WebApi
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
        public string Code { get; set; } = default!;

        /// <summary>
        /// The trace id for this operation.
        /// </summary>
        /// <example>75c0e295a3c74e888f800292f1abeea9</example>
        public string Id { get; set; } = Activity.Current?.RootId ?? Guid.NewGuid().ToString();

        /// <summary>
        /// A general description of what happened.
        /// </summary>
        /// <example>General error message</example>
        public string Message { get; set; } = default!;

        /// <summary>
        /// A detailed list of errors.
        /// </summary>
        public IEnumerable<ApiErrorDetail> Errors { get; set; } = default!;
    }
}
