using System;
using Fusap.Common.Model;

// ReSharper disable once CheckNamespace
namespace FluentValidation
{
    public static class RuleBuilderOptionsExtensions
    {
        public static IRuleBuilderOptions<T, TProperty> WithErrorCode<T, TProperty>(
            this IRuleBuilderOptions<T, TProperty> rule, ErrorCatalogEntry errorCatalogEntry)
        {
            return rule.WithErrorCode(errorCatalogEntry.Code);
        }
        public static IRuleBuilderOptions<T, TProperty> WithMessage<T, TProperty>(
            this IRuleBuilderOptions<T, TProperty> rule, ErrorCatalogEntry errorCatalogEntry)
        {
            return rule.WithMessage(errorCatalogEntry.Message);
        }

        public static IRuleBuilderOptions<T, TProperty> WithErrorCatalog<T, TProperty>(
            this IRuleBuilderOptions<T, TProperty> rule, ErrorCatalogEntry errorCatalogEntry)
        {
            return rule.WithErrorCode(errorCatalogEntry.Code).WithMessage(errorCatalogEntry.Message);
        }

        public static IRuleBuilderOptions<T, TProperty> WithErrorCatalog<T, TProperty>(
            this IRuleBuilderOptions<T, TProperty> rule, ErrorCatalogEntry errorCatalogEntry, object arg0)
        {
            return rule.WithErrorCode(errorCatalogEntry.Code).WithMessage(errorCatalogEntry.Format(arg0));
        }

        public static IRuleBuilderOptions<T, TProperty> WithErrorCatalog<T, TProperty>(
            this IRuleBuilderOptions<T, TProperty> rule, ErrorCatalogEntry errorCatalogEntry, object arg0, object arg1)
        {
            return rule.WithErrorCode(errorCatalogEntry.Code).WithMessage(errorCatalogEntry.Format(arg0, arg1));
        }

        public static IRuleBuilderOptions<T, TProperty> WithErrorCatalog<T, TProperty>(
            this IRuleBuilderOptions<T, TProperty> rule, ErrorCatalogEntry errorCatalogEntry, object arg0, object arg1, object arg2)
        {
            return rule.WithErrorCode(errorCatalogEntry.Code).WithMessage(errorCatalogEntry.Format(arg0, arg1, arg2));
        }

        public static IRuleBuilderOptions<T, TProperty> WithErrorCatalog<T, TProperty>(
            this IRuleBuilderOptions<T, TProperty> rule, ErrorCatalogEntry errorCatalogEntry, params object[] args)
        {
            return rule.WithErrorCode(errorCatalogEntry.Code).WithMessage(errorCatalogEntry.Format(args));
        }

        public static IRuleBuilderOptions<T, TProperty> WithErrorCatalog<T, TProperty>(
            this IRuleBuilderOptions<T, TProperty> rule, ErrorCatalogEntry errorCatalogEntry,
            Func<ErrorCatalogEntry, T, ErrorCatalogEntry> errorFormatter)
        {
            return rule.WithErrorCode(errorCatalogEntry.Code).WithMessage((x) => errorFormatter(errorCatalogEntry, x).Message);
        }

        public static IRuleBuilderOptions<T, TProperty> WithErrorCatalog<T, TProperty>(
            this IRuleBuilderOptions<T, TProperty> rule, ErrorCatalogEntry errorCatalogEntry,
            Func<ErrorCatalogEntry, T, TProperty, ErrorCatalogEntry> errorFormatter)
        {
            return rule.WithErrorCode(errorCatalogEntry.Code).WithMessage((x, y) => errorFormatter(errorCatalogEntry, x, y).Message);
        }
    }
}
