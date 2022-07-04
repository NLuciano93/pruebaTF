namespace Fusap.Common.Model
{
    public interface IResult<out TValue> : IResult
    {
        new TValue Value { get; }
    }
}