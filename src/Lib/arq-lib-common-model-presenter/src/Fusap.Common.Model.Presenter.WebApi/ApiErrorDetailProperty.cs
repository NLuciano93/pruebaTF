using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Fusap.Common.Model.Presenter.WebApi
{
    /// <summary>
    /// A property that can convey extra information when an error happen.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public readonly struct ApiErrorDetailProperty
    {
        /// <summary>
        /// The property key.
        /// </summary>
        /// <example></example>
        public string? Key { get; }

        /// <summary>
        /// The property value.
        /// </summary>
        /// <example></example>
        public string? Value { get; }

        public ApiErrorDetailProperty(KeyValuePair<string, string> kv)
        {
            Key = kv.Key;
            Value = kv.Value;
        }

        public ApiErrorDetailProperty(string key, string value)
        {
            Key = key;
            Value = value;
        }

        public static implicit operator ApiErrorDetailProperty(KeyValuePair<string, string> kv)
        {
            return new ApiErrorDetailProperty(kv);
        }

        public static implicit operator KeyValuePair<string, string>(ApiErrorDetailProperty property)
        {
            return new KeyValuePair<string, string>(property.Key!, property.Value!);
        }
    }
}
