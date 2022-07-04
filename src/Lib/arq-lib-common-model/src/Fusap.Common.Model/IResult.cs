namespace Fusap.Common.Model
{
    public interface IResult
    {
        object? Value { get; }
        Error? Error { get; }
    }
}