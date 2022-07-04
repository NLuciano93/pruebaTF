using FluentValidation;

namespace Fusap.Common.Mediator.FluentValidation
{
    public static class ValidationContextExtensions
    {
        public static TValue GetData<TValue>(this IValidationContext context)
        {
            return context.RootContextData.ContainsKey(nameof(TValue)) ?
                (TValue)context.RootContextData[nameof(TValue)] :
                default!;
        }

        public static TValue GetData<TValue>(this IValidationContext context, string name)
        {
            return context.RootContextData.ContainsKey(name) ?
                (TValue)context.RootContextData[name] :
                default!;
        }
        public static void SetData<TValue>(this IValidationContext context, TValue data)
        {
            context.RootContextData[nameof(TValue)] = data;
        }

        public static void SetData(this IValidationContext context, string name, object data)
        {
            context.RootContextData[name] = data;
        }
    }
}
