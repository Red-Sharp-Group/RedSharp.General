using System;
using RedSharp.General.Helpers;
using RedSharp.General.Unmanaged.Helpers;
using RedSharp.General.Unmanaged.Interfaces;

namespace RedSharp.General.Unmanaged.Abstracts
{
    public class UnmanagedMemoryWrapperBase : UnmanagedMemoryBase
    {
        private IUnmanagedMemory _basicStructure;

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <remarks>
        /// If structure is created from other <see cref="INativeStructure"/> without coping 
        /// then <see cref="IsDisposed"/> will be connected to the basic <see cref="INativeStructure"/>
        /// </remarks>
        public override bool IsDisposed
        {
            get
            {
                if (_basicStructure == null)
                    return base.IsDisposed;
                else
                    return base.IsDisposed || _basicStructure.IsDisposed;
            }
        }

        /// <summary>
        /// Sets/Copies a piece of memory based on the input native structure with desired offset and size.
        /// </summary>
        /// <exception cref="ArgumentException">If size is wrong.</exception>
        /// <exception cref="ArgumentNullException">If the input structure is null.</exception>
        /// <exception cref="ObjectDisposedException">If the input structure is disposed.</exception>
        /// <exception cref="ArgumentOutOfRangeException">If new structure is outside of the basic structure.</exception>
        protected virtual void Initialize(IUnmanagedMemory basic, ulong offset, ulong size, bool makeCopy = true)
        {
            ArgumentsGuard.ThrowIfNull(basic, nameof(basic));
            ArgumentsGuard.ThrowIfDisposed(basic, nameof(basic));
            NumericGuard.ThrowIfLessZero(size, nameof(size));

            var targetPointer = (IntPtr)((ulong)basic.UnsafeHandle + offset);

            UnmanagedGuard.ThrowIfPointerIsOutOfRange(basic.UnsafeHandle, basic.Size, targetPointer, size, "target pointer");

            Initialize(targetPointer, size, makeCopy);

            if (!makeCopy)
                _basicStructure = basic;
        }

        protected override void InternalDispose(bool manual)
        {
            _basicStructure = null;

            base.InternalDispose(manual);
        }
    }
}
