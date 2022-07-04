using System;
using System.Collections.Generic;

namespace Fusap.Common.Model
{
    /// <inheritdoc cref="IPagination{TItem,TNext}" />
    public readonly struct Pagination<TItem, TNext> : IPagination<TItem, TNext>
    {
        public TNext Next { get; }
        public long? EstimatedCount { get; }
        public IEnumerable<TItem> Items { get; }

        public Pagination(IEnumerable<TItem> items, TNext next)
        {
            Items = items;
            Next = next;
            EstimatedCount = null;
        }

        public Pagination(IEnumerable<TItem> items, TNext next, long? estimatedCount)
        {
            Items = items;
            Next = next;
            EstimatedCount = estimatedCount;
        }

        public IEnumerator<TItem> GetEnumerator()
        {
            return Items.GetEnumerator();
        }

        /// <summary>
        /// Creates an empty pagination.
        /// </summary>
        public static Pagination<TItem, TNext> Empty()
        {
            return new Pagination<TItem, TNext>(Array.Empty<TItem>(), default!);
        }
    }
}
