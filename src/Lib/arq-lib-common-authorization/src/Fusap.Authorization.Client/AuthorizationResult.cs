using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Fusap.Common.Authorization.Client
{
    public class AuthorizationResult : IEnumerable<RequirementResult>
    {
        private readonly IReadOnlyDictionary<Requirement, RequirementResult> _results;

        public AuthorizationResult(params RequirementResult[] results)
        {
            _results = results.ToDictionary(x => x.Requirement, x => x);
        }

        public AuthorizationResult(IEnumerable<RequirementResult> results)
        {
            _results = results.ToDictionary(x => x.Requirement, x => x);
        }

        public IEnumerator<RequirementResult> GetEnumerator()
        {
            return _results.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public RequirementResult this[Requirement requirement] => _results[requirement];

        public bool Successful => _results.Any() && _results.All(kv => kv.Value.Successful);

        public static implicit operator bool(AuthorizationResult results)
        {
            return results.Successful;
        }
    }
}