namespace Fusap.Common.Model.Tests
{
    public class CompatibleResult<T> : IResult<T>
    {
        object? IResult.Value => Value;

        public T Value { get; set; } = default!;
        public Error? Error { get; set; }
    }
}
