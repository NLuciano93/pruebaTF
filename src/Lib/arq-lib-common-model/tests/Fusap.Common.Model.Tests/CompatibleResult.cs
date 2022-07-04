namespace Fusap.Common.Model.Tests
{
    public class CompatibleResult : IResult
    {
        public object? Value { get; set; }
        public Error? Error { get; set; }
    }
}