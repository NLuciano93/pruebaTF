using System;

namespace Fusap.Common.Model
{
    public static class FusapModelExtensions
    {
        public static bool IsSuccessful(this IResult? result)
        {
            if (result == null)
            {
                throw new ArgumentNullException(nameof(result));
            }

            return result.Error is null;
        }

        public static void EnsureSuccess(this IResult? result)
        {
            if (result == null)
            {
                throw new ArgumentNullException(nameof(result));
            }

            if (result.Error != null)
            {
                throw new FailedResultException(result.Error);
            }
        }

        public static Result AsResult(this IResult result)
        {
            if (result is Result typedResult)
            {
                return typedResult;
            }

            return result.Error != null
                ? new Result(result.Error)
                : new Result(result.Value);
        }

        public static Result<TValue> AsResult<TValue>(this IResult<TValue> result)
        {
            if (result is Result<TValue> typedResult)
            {
                return typedResult;
            }

            return result.Error != null
                ? new Result<TValue>(result.Error)
                : new Result<TValue>(result.Value);
        }
    }
}
