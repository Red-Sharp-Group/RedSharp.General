using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using RedSharp.General.Helpers;

namespace RedSharp.General.Collections.Abstracts
{
    /// <summary>
    /// Base class that accumulates comparing functionality in one object.
    /// </summary>
    public abstract class ComparerBase<TItem> : IComparer<TItem>, IEqualityComparer<TItem>, IComparer, IEqualityComparer
    {
        public ComparerBase(bool isAscending = true)
        {
            IsAscending = isAscending;
        }

        public bool IsAscending { get; private set; }


        /// <inheritdoc/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int Compare(TItem first, TItem second)
        {
            var result = InternalCompare(first, second);

            if (!IsAscending)
                result *= -1;

            return result;
        }

        int IComparer.Compare(object first, object second)
        {
            return Compare((TItem)first, (TItem)second);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(TItem first, TItem second)
        {
            return Compare(first, second) == 0;
        }

        bool IEqualityComparer.Equals(object first, object second)
        {
            return Equals((TItem)first, (TItem)second);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetHashCode(TItem obj)
        {
            return InternalGetHashCode(obj);
        }

        int IEqualityComparer.GetHashCode(object obj)
        {
            ArgumentsGuard.ThrowIfNotType(obj, out TItem item, nameof(obj));

            return GetHashCode(item);
        }


        /// <inheritdoc cref="Compare"/>
        protected abstract int InternalCompare(TItem first, TItem second);

        /// <inheritdoc cref="GetHashCode"/>
        protected virtual int InternalGetHashCode(TItem item)
        {
            ArgumentsGuard.ThrowIfNull(item, nameof(item));

            return item.GetHashCode();
        }
    }
}
