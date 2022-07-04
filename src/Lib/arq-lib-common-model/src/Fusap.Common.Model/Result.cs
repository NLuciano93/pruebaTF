using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fusap.Common.Model
{
    public readonly struct Result : IResult, IEquatable<Result>
    {
        // ReSharper disable once InconsistentNaming
        internal readonly object? _value;

        public object? Value
        {
            get
            {
                this.EnsureSuccess();
                return _value;
            }
        }

        public Error? Error { get; }

        public Result(Error? error)
        {
            _value = null;
            Error = error;
        }

        public Result(object? value, Error? error = null)
        {
            _value = value;
            Error = error;
        }

        public static Result Success()
        {
            return new Result();
        }

        public static Result Success(object value)
        {
            return new Result(value);
        }

        public static Result<TValue> Success<TValue>(TValue value)
        {
            return new Result<TValue>(value);
        }

        public static Result Failure(Error error)
        {
            return new Result(null, error);
        }

        public override bool Equals(object? obj)
        {
            return obj is Result result && Equals(result);
        }

        public bool Equals(Result other)
        {
            return EqualityComparer<object?>.Default.Equals(_value, other._value) &&
                   EqualityComparer<Error?>.Default.Equals(Error, other.Error);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_value, Error);
        }

        public static implicit operator Result(Error error)
        {
            return new Result(error);
        }

        public static implicit operator Result(Exception exception)
        {
            return new Result(exception);
        }

        public static implicit operator Task<Result>(Result result)
        {
            return Task.FromResult(result);
        }

        public static bool operator ==(Result left, Result right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Result left, Result right)
        {
            return !(left == right);
        }
    }
}
