using System;
using System.Linq;
using System.Threading.Tasks;
using Polly;
using Polly.CircuitBreaker;
using Polly.Retry;

namespace Fusap.TimeSheet.Application.ServiceClient
{
    public interface IPolicyService
    {
        AsyncRetryPolicy CreateWaitAndRetryPolicyAsyncHandleException<TException>(int retryCount) where TException : Exception;
        AsyncRetryPolicy<TResult> CreateWaitAndRetryPolicyAsyncHandleResult<TResult>(Func<TResult, bool> predicate, int retryCount);
        AsyncCircuitBreakerPolicy CreateCircuitBreakerPolicyAsyncHandleException<TException>(int exceptionsAllowedBeforeBreaking,
            int durationOfBreak) where TException : Exception;
        AsyncCircuitBreakerPolicy<TResult> CreateCircuitBreakerPolicyAsyncHandleResult<TResult>(Func<TResult, bool> predicate,
            int handledEventsAllowedBeforeBreaking, int durationOfBreak);
        Task<TResult> ExecutePoliciesAsync<TResult>(Func<Task<TResult>> action, params AsyncPolicy<TResult>[] policies);
        Task<TResult> ExecutePoliciesAsync<TResult>(Func<Task<TResult>> action, params AsyncPolicy[] policies);
    }
    public class PolicyService : IPolicyService
    {
        private const int POLICIES_LENGTH = 2;
        private const int POLICIES_SKIP = 2;
        private const int TWO_FACTOR_POWER = 2;
        public AsyncRetryPolicy CreateWaitAndRetryPolicyAsyncHandleException<TException>(int retryCount) where TException : Exception
        {
            return Policy
                    .Handle<TException>()
                    .WaitAndRetryAsync(
                                        retryCount: retryCount,
                                        retryAttempt => TimeSpan.FromSeconds(Math.Pow(TWO_FACTOR_POWER, retryAttempt))
                                        );
        }

        public AsyncRetryPolicy<TResult> CreateWaitAndRetryPolicyAsyncHandleResult<TResult>(Func<TResult, bool> predicate, int retryCount)
        {
            return Policy
                    .HandleResult(predicate)
                    .WaitAndRetryAsync(
                                        retryCount: retryCount,
                                        retryAttempt => TimeSpan.FromSeconds(Math.Pow(TWO_FACTOR_POWER, retryAttempt))
                                      );
        }

        public AsyncCircuitBreakerPolicy CreateCircuitBreakerPolicyAsyncHandleException<TException>(int exceptionsAllowedBeforeBreaking,
            int durationOfBreak) where TException : Exception
        {
            return Policy
                    .Handle<TException>()
                    .CircuitBreakerAsync(
                                            exceptionsAllowedBeforeBreaking: exceptionsAllowedBeforeBreaking,
                                            durationOfBreak: TimeSpan.FromSeconds(durationOfBreak)
                                        );
        }

        public AsyncCircuitBreakerPolicy<TResult> CreateCircuitBreakerPolicyAsyncHandleResult<TResult>(Func<TResult, bool> predicate,
            int handledEventsAllowedBeforeBreaking, int durationOfBreak)
        {
            return Policy
                    .HandleResult(predicate)
                    .CircuitBreakerAsync(
                                            handledEventsAllowedBeforeBreaking: handledEventsAllowedBeforeBreaking,
                                            durationOfBreak: TimeSpan.FromSeconds(durationOfBreak)
                                        );
        }

        public Task<TResult> ExecutePoliciesAsync<TResult>(Func<Task<TResult>> action, params AsyncPolicy<TResult>[] policies)
        {
            var isNotNull = policies.All(x => x != null);
            if (isNotNull)
            {
                var asyncPolicy = WrapPolicies(policies);
                return asyncPolicy.ExecuteAsync(() => action());
            }
            return action();
        }

        public Task<TResult> ExecutePoliciesAsync<TResult>(Func<Task<TResult>> action, params AsyncPolicy[] policies)
        {
            var isNotNull = policies.All(x => x != null);
            if (isNotNull)
            {
                var asyncPolicy = WrapPolicies(policies);
                return asyncPolicy.ExecuteAsync(() => action());
            }
            return action();
        }

        private AsyncPolicy<TResult> WrapPolicies<TResult>(params AsyncPolicy<TResult>[] policies)
        {
            AsyncPolicy<TResult> asyncPolicy;

            if (policies.Length >= POLICIES_LENGTH)
            {
                asyncPolicy = Policy.WrapAsync(policies.FirstOrDefault(), policies.Skip(1).FirstOrDefault());
                foreach (var policy in policies.Skip(POLICIES_SKIP))
                {
                    asyncPolicy.WrapAsync(policy);
                }
            }
            else
            {
                asyncPolicy = policies.FirstOrDefault();
            }

            return asyncPolicy;
        }

        private AsyncPolicy WrapPolicies(params AsyncPolicy[] policies)
        {
            AsyncPolicy asyncPolicy;

            if (policies.Length >= POLICIES_LENGTH)
            {
                asyncPolicy = Policy.WrapAsync(policies.FirstOrDefault(), policies.Skip(1).FirstOrDefault());
                foreach (var policy in policies.Skip(POLICIES_SKIP))
                {
                    asyncPolicy.WrapAsync(policy);
                }
            }
            else
            {
                asyncPolicy = policies.FirstOrDefault();
            }

            return asyncPolicy;
        }
    }
}
