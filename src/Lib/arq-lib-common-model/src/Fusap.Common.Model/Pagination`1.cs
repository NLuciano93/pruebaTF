using System;
using System.Collections.Generic;

namespace Fusap.Common.Model
{
    /// <inheritdoc cref="IPagination{TItem}" />
    public readonly struct Pagination<TItem> : IPagination<TItem>
    {
        public long? Next { get; }
        public long? EstimatedCount { get; }
        public IEnumerable<TItem> Items { get; }

        public Pagination(IEnumerable<TItem> items, long? next)
        {
            Items = items;
            Next = next;
            EstimatedCount = null;
        }

        public Pagination(IEnumerable<TItem> items, long? next, long? estimatedCount)
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
        public static Pagination<TItem> Empty()
        {
            return new Pagination<TItem>(Array.Empty<TItem>(), null);
        }
    }
}
