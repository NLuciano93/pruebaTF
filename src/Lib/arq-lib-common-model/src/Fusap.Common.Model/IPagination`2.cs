using System.Collections.Generic;

namespace Fusap.Common.Model
{
    /// <summary>
    /// A paginated result.
    /// </summary>
    /// <typeparam name="TItem">The type of item to be paginated</typeparam>
    /// <typeparam name="TNext">The type of token to represent the next page</typeparam>
    public interface IPagination<out TItem, out TNext>
    {
        /// <summary>
        /// The items on this page.
        /// </summary>
        IEnumerable<TItem> Items { get; }

        /// <summary>
        /// Indicates the next page.
        /// </summary>
        TNext Next { get; }

        /// <summary>
        /// Indicates an estimate of how many items are there in total. The value can be null if no estimate is available.
        /// </summary>
        long? EstimatedCount { get; }

        // Must not implement IEnumerable, otherwise swagger and json serialization will output
        // this object as the list it contains, ignoring the Next and EstimatedCount properties.
        // By implementing an GetEnumerator, the compiler can still enumerate the items.
        IEnumerator<TItem> GetEnumerator();
    }
}
