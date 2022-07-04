using System.Collections.Generic;
using System.Linq;

namespace Fusap.Common.Model
{
    public class ValidationError : Error
    {
        private const string ErrorCode = "VALIDATION_ERROR";
        private const string ErrorMessage = "One or more values were invalid";

        public IEnumerable<ValidationErrorDetail> Details { get; }

        public ValidationError(params ValidationErrorDetail[] details) : base(ErrorCode, ErrorMessage)
        {
            Details = details;
        }

        public override string ToString()
        {
            var details = string.Join(", ", Details.Select(x => x.ToString()));

            if (string.IsNullOrEmpty(details))
            {
                details = "no details";
            }

            return base.ToString() + " (" + details + ")";
        }
    }
}
