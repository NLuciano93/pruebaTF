using System;
using System.Reflection;

// ReSharper disable once CheckNamespace
namespace Fusap.Common.Mediator
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public class AuthorizeAttribute : Attribute
    {
        public string[] Actions { get; }

        public AuthorizeAttribute(params string[] actions)
        {
            Actions = actions;
        }

        public static string[]? ActionsFor(Type type)
        {
            return type.GetCustomAttribute<AuthorizeAttribute>()?.Actions;
        }

        public static string[]? ActionsFor<T>()
        {
            return typeof(T).GetCustomAttribute<AuthorizeAttribute>()?.Actions;
        }
    }
}
