using System;
using System.Runtime.CompilerServices;
using RedSharp.General.Helpers;
using RedSharp.General.Unmanaged.Helpers;
using RedSharp.General.Unmanaged.Interfaces;
using RedSharp.General.Unmanaged.Properties;

namespace RedSharp.General.Unmanaged.Abstracts
{
    public abstract unsafe class UnmanagedStructureWrapperBase<TStructure> : UnmanagedMemoryWrapperBase where TStructure : unmanaged
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void ThrowIfSizeIsLessThanStructure(ulong value, string name)
        {
            if (value < (ulong)sizeof(TStructure))
                throw new ArgumentOutOfRangeException(name, Resource.Exception_SizesAreNotCorrect);
        }

        /// <summary>
        /// Returns ref to the structure internally without any checks.
        /// </summary>
        protected ref TStructure InternalValue
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => ref *(TStructure*)UnsafeHandle.ToPointer();
        }

        /// <summary>
        /// Initializes object with default structure size.
        /// </summary>
        protected virtual void Initialize()
        {
            Initialize((ulong)sizeof(TStructure));
        }

        /// <inheritdoc/>
        /// <exception cref="ArgumentOutOfRangeException">If input size is less then structure size.</exception>
        protected override void Initialize(ulong size)
        {
            NumericGuard.ThrowIfLessZero(size, nameof(size));
            ThrowIfSizeIsLessThanStructure(size, nameof(size));

            base.Initialize(size);
        }

        /// <inheritdoc/>
        /// <exception cref="ArgumentOutOfRangeException">If input size is less then structure size.</exception>
        protected override void Initialize(IntPtr pointer, ulong size, bool makeCopy = true)
        {
            NumericGuard.ThrowIfLessZero(size, nameof(size));
            ThrowIfSizeIsLessThanStructure(size, nameof(size));

            base.Initialize(pointer, size, makeCopy);
        }

        /// <inheritdoc/>
        /// <exception cref="ArgumentOutOfRangeException">If input size is less then structure size.</exception>
        protected override void Initialize(IUnmanagedMemory basic, ulong offset, ulong size, bool makeCopy = true)
        {
            NumericGuard.ThrowIfLessZero(size, nameof(size));
            ThrowIfSizeIsLessThanStructure(size, nameof(size));

            base.Initialize(basic, offset, size, makeCopy);
        }

        /// <inheritdoc/>
        /// <exception cref="ArgumentOutOfRangeException">If input size is less then structure size.</exception>
        protected override void ReInitialize(ulong newSize)
        {
            NumericGuard.ThrowIfLessZero(newSize, nameof(newSize));
            ThrowIfSizeIsLessThanStructure(newSize, nameof(newSize));

            base.ReInitialize(newSize);
        }
    }
}
