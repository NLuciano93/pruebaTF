using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fusap.Common.Model
{
    public readonly struct Result<TValue> : IResult<TValue>, IEquatable<Result<TValue>>
    {
        // ReSharper disable once InconsistentNaming
        internal readonly TValue _value;

        public TValue Value
        {
            get
            {
                this.EnsureSuccess();
                return _value;
            }
        }

        object? IResult.Value => Value;

        public Error? Error { get; }

        public Result(TValue value)
        {
            _value = value;
            Error = null;
        }

        public Result(Error? error)
        {
            _value = default!;
            Error = error;
        }

        public static implicit operator TValue(Result<TValue> result)
        {
            return result.Value;
        }

        public static implicit operator Result<TValue>(TValue value)
        {
            return new Result<TValue>(value);
        }

        public static implicit operator Result(Result<TValue> result)
        {
            return new Result(result._value, result.Error);
        }

        public static implicit operator Result<TValue>(Result result)
        {
            if (result.Error != null)
            {
                return new Result<TValue>(result.Error);
            }

            if (result._value == null && !IsNullable(typeof(TValue)))
            {
#pragma warning disable S3877 // Exceptions should not be thrown from unexpected methods
                throw new ArgumentNullException(nameof(result), "You cannot use the implicit operator with a result containing no value.");
#pragma warning restore S3877 // Exceptions should not be thrown from unexpected methods
            }

            return new Result<TValue>((TValue)result._value!);
        }

        public static implicit operator Result<TValue>(Error error)
        {
            return new Result<TValue>(error);
        }

        public static implicit operator Result<TValue>(Exception exception)
        {
            return new Result<TValue>(exception);
        }

        public static implicit operator Task<Result>(Result<TValue> result)
        {
            return Task.FromResult((Result)result);
        }

        public static implicit operator Task<Result<TValue>>(Result<TValue> result)
        {
            return Task.FromResult(result);
        }

        public static bool operator ==(Result<TValue> left, Result<TValue> right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Result<TValue> left, Result<TValue> right)
        {
            return !(left == right);
        }

        public override bool Equals(object? obj)
        {
            return obj is Result<TValue> result && Equals(result);
        }

        public bool Equals(Result<TValue> other)
        {
            return EqualityComparer<TValue>.Default.Equals(_value, other._value) &&
                   EqualityComparer<Error?>.Default.Equals(Error, other.Error);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_value, Error);
        }

        private static bool IsNullable(Type type)
        {
            if (type.IsValueType)
            {
                return Nullable.GetUnderlyingType(type) != null;
            }

            return false;
        }
    }
}
