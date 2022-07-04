using System.Collections.Generic;

namespace Fusap.TimeSheet.Domain.TimeSheetAggregate.Response
{
    public class ErrorResponse
    {
        public string Code { get; set; } = default!;
        public string? Id { get; set; }
        public string Message { get; set; } = default!;
        public List<Error> Errors { get; set; } = default!;
    }

    public class Error
    {
        public string ErrorCode { get; set; } = default!;
        public string Message { get; set; } = default!;
        public string? Path { get; set; }
        public string? Url { get; set; }
    }
}
