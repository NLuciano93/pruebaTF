using System;
using System.Collections.Generic;
using System.Linq;

namespace Fusap.Common.Authorization.Client
{
    public class Requirement
    {
        public Uri Identity { get; }
        public Uri[] Resources { get; }
        public string[] Actions { get; }

        public Requirement(Uri identity, Uri resource, params string[] actions)
        {
            Identity = ValidateInput(identity, nameof(identity));
            Resources = new[] { ValidateInput(resource, nameof(resource)) };
            Actions = ValidateInput(actions, nameof(actions));
        }

        public Requirement(Uri identity, Uri[] resources, params string[] actions)
        {
            Identity = ValidateInput(identity, nameof(identity));
            Resources = ValidateInput(resources, nameof(resources));
            Actions = ValidateInput(actions, nameof(actions));
        }

        private static T ValidateInput<T>(T input, string paramName)
        {
            switch (input)
            {
                case null:
                    throw new ArgumentNullException(paramName);
                case Array arr:
                    if (arr.Cast<object>().Any(item => item == null))
                    {
                        throw new ArgumentNullException(paramName);
                    }

                    break;
            }

            return input;
        }

        public override string ToString()
        {
            var res = string.Join(",", Resources.OrderBy(x => x.ToString()).Select(r => r.ToString()));
            var act = string.Join(",", Actions.OrderBy(x => x).Select(r => r.ToString()));

            return $"id={Identity};res={res};act={act}";
        }

        public override bool Equals(object? obj)
        {
            return obj is Requirement requirement &&
                   EqualityComparer<string>.Default.Equals(ToString(), requirement.ToString());
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }
    }
}