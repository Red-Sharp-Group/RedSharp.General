using System;
using RedSharp.General.Helpers;
using RedSharp.General.Unmanaged.Interfaces;

namespace RedSharp.General.Unmanaged
{
    public unsafe static class UnmanagedHelper
    {
        /// <summary>
        /// Creates a new span from the native structure.
        /// </summary>
        /// <remarks>
        /// Be carefully, the result span is not owning the pointer.
        /// </remarks>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ObjectDisposedException"/>
        public static Span<byte> ToSpanOfBytes(this IUnmanagedMemory item, ulong offset = 0, int size = -1)
        {
            ArgumentsGuard.ThrowIfNull(item, nameof(item));
            ArgumentsGuard.ThrowIfDisposed(item, nameof(item));
            NumericGuard.ThrowIfLess(size, -1, nameof(size));

            if (size == -1)
                size = (int)Math.Min(int.MaxValue, item.Size);

            var truePosition = (byte*)item.UnsafeHandle.ToPointer() + offset;

            return new Span<byte>(truePosition, size);
        }

        /// <summary>
        /// Creates a new span from the native buffer.
        /// </summary>
        /// <remarks>
        /// Items of span will be the same as is in <see cref="UnmanagedBuffer{TItem}"/>
        /// Be carefully, the result span is not owning the pointer.
        /// </remarks>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ObjectDisposedException"/>
        public static Span<TItem> ToSpanOfItems<TItem>(this UnmanagedBuffer<TItem> item, int offset = 0, int size = -1) where TItem : unmanaged
        {
            ArgumentsGuard.ThrowIfNull(item, nameof(item));
            ArgumentsGuard.ThrowIfDisposed(item, nameof(item));
            NumericGuard.ThrowIfLessZero(offset, nameof(offset));
            NumericGuard.ThrowIfLess(size, -1, nameof(size));

            if (size == -1)
                size = item.Length;

            if (offset + size > item.Length)
                throw new ArgumentOutOfRangeException(nameof(offset));

            var truePosition = (TItem*)item.UnsafeHandle.ToPointer() + offset;

            return new Span<TItem>(truePosition, size);
        }
    }
}
