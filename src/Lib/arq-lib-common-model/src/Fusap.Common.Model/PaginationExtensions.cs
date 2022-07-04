using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Fusap.Common.Model
{
    [ExcludeFromCodeCoverage]
    public static class PaginationExtensions
    {
        public static Pagination<TItem> ToPagination<TItem>(this IEnumerable<TItem> items)
        {
            return new Pagination<TItem>(items, null);
        }

        public static Pagination<TItem> ToPagination<TItem>(this IEnumerable<TItem> items, int next)
        {
            return new Pagination<TItem>(items, next);
        }

        public static Pagination<TItem> ToPagination<TItem>(this IEnumerable<TItem> items, int next, long? estimatedCount)
        {
            return new Pagination<TItem>(items, next, estimatedCount);
        }

        public static Pagination<TItem> ToPagination<TItem>(this IEnumerable<TItem> items, int? next)
        {
            return new Pagination<TItem>(items, next);
        }

        public static Pagination<TItem> ToPagination<TItem>(this IEnumerable<TItem> items, int? next, long? estimatedCount)
        {
            return new Pagination<TItem>(items, next, estimatedCount);
        }

        public static Pagination<TItem> ToPagination<TItem>(this IEnumerable<TItem> items, long next)
        {
            return new Pagination<TItem>(items, next);
        }

        public static Pagination<TItem> ToPagination<TItem>(this IEnumerable<TItem> items, long next, long? estimatedCount)
        {
            return new Pagination<TItem>(items, next, estimatedCount);
        }

        public static Pagination<TItem> ToPagination<TItem>(this IEnumerable<TItem> items, long? next)
        {
            return new Pagination<TItem>(items, next);
        }

        public static Pagination<TItem> ToPagination<TItem>(this IEnumerable<TItem> items, long? next, long? estimatedCount)
        {
            return new Pagination<TItem>(items, next, estimatedCount);
        }

        public static Pagination<TItem, TNext> ToPagination<TItem, TNext>(this IEnumerable<TItem> items, TNext next)
        {
            return new Pagination<TItem, TNext>(items, next);
        }

        public static Pagination<TItem, TNext> ToPagination<TItem, TNext>(this IEnumerable<TItem> items, TNext next, long? estimatedCount)
        {
            return new Pagination<TItem, TNext>(items, next, estimatedCount);
        }
    }
}
