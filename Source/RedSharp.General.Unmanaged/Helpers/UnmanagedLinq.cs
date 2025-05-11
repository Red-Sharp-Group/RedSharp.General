using System.Collections.Generic;
using RedSharp.General.Helpers;
using RedSharp.General.Unmanaged;

namespace System.Linq
{
    public static class UnmanagedLinq
    {
        /// <summary>
        /// Allocates new <see cref="UnmanagedBuffer{TItem}"/> and fill it with input items.
        /// </summary>
        /// <exception cref="ArgumentNullException"/>
        public static UnmanagedBuffer<TItem> ToUnmanagedBuffer<TItem>(this IEnumerable<TItem> items) where TItem : unmanaged
        {
            ArgumentsGuard.ThrowIfNull(items, nameof(items));

            var length = items.Count();
            var result = new UnmanagedBuffer<TItem>(length);
            var index = 0;

            foreach (var item in items)
                result[index++] = item;

            return result;
        }

        /// <summary>
        /// Allocates new <see cref="UnmanagedList{TItem}"/> and fill it with input items.
        /// </summary>
        /// <exception cref="ArgumentNullException"/>
        public static UnmanagedList<TItem> ToUnmanagedList<TItem>(this IEnumerable<TItem> items) where TItem : unmanaged
        {
            ArgumentsGuard.ThrowIfNull(items, nameof(items));

            var length = items.Count();
            var result = new UnmanagedList<TItem>(length);
            var index = 0;

            foreach (var item in items)
                result[index++] = item;

            return result;
        }
    }
}
