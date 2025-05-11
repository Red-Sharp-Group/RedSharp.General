using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using RedSharp.General.Abstracts;
using RedSharp.General.Helpers;
using RedSharp.General.Unmanaged.Helpers;
using RedSharp.General.Unmanaged.Interfaces;
using RedSharp.General.Unmanaged.Properties;

namespace RedSharp.General.Unmanaged.Abstracts
{
    public abstract unsafe class UnmanagedMemoryBase : DisposableBase, IUnmanagedMemory
    {
        /// <inheritdoc/>
        public ulong Size { get; private set; }

        /// <inheritdoc/>
        public bool IsHandleOwner { get; private set; }

        /// <inheritdoc/>
        public IntPtr UnsafeHandle { get; private set; }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void ThrowIfInitialized()
        {
            if (UnsafeHandle != IntPtr.Zero)
                throw new Exception(Resource.Exception_IsAlreadyAllocated);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected static void Copy(void* destination, void* target, ulong size)
        {
            while (size > 0)
            {
#if NETCOREAPP
                var block = (uint)Math.Min(size, uint.MaxValue);

                Unsafe.CopyBlock(destination, target, block);

                size -= block;
#else
                var block = (int)Math.Min(size, int.MaxValue);

                var targetSpan = new Span<byte>(target, block);
                var destinationSpan = new Span<byte>(destination, block);

                destinationSpan.CopyTo(targetSpan);

                size -= (ulong)block;
#endif
            }
        }

        /// <summary>
        /// Allocates a new piece of memory by given size.
        /// </summary>
        /// <remarks>
        /// <see cref="IsHandleOwner"/> will be true.
        /// </remarks>
        /// <exception cref="Exception">In case when you try to allocate memory twice.</exception>
        /// <exception cref="ArgumentOutOfRangeException">If input size is less of equal zero.</exception>
        /// <exception cref="Exception">If <see cref="Allocate(int, out int)"/> returns an unexpected value.</exception>
        /// <exception cref="ObjectDisposedException">If object is disposed.</exception>
        protected virtual void Initialize(ulong size)
        {
            ThrowIfDisposed();
            ThrowIfInitialized();

            NumericGuard.ThrowIfLessOrEqualZero(size, nameof(size));
            UnmanagedGuard.ThrowIfSizeIsTooBig(size, nameof(size));

            UnsafeHandle = Allocate(size);

            if (UnsafeHandle == IntPtr.Zero)
                throw new Exception(Resource.Exception_AllocationFailed);

            Size = size;
            IsHandleOwner = false;
        }

        /// <summary>
        /// Copies or Sets memory pointer.
        /// </summary>
        /// <remarks>
        /// This method is quite UNSAFE. Thinks twice before using it.
        /// </remarks>
        /// <exception cref="Exception">In case when you try to set pointer twice.</exception>
        /// <exception cref="ArgumentNullException">If input pointer is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">If input size is less of equal zero.</exception>
        /// <exception cref="ObjectDisposedException">If object is disposed.</exception>
        /// <exception cref="Exception">If <see cref="Allocate(int, out int)"/> returns an unexpected value.</exception>
        protected virtual void Initialize(IntPtr pointer, ulong size, bool makeCopy = true)
        {
            ThrowIfDisposed();
            ThrowIfInitialized();

            UnmanagedGuard.ThrowIfNull(pointer, nameof(pointer));
            NumericGuard.ThrowIfLessOrEqualZero(size, nameof(size));
            UnmanagedGuard.ThrowIfSizeIsTooBig(size, nameof(size));

            if (makeCopy)
            {
                UnsafeHandle = pointer;
                IsHandleOwner = false;

                GC.SuppressFinalize(this);
            }
            else
            {
                UnsafeHandle = Allocate(size);

                if (UnsafeHandle == IntPtr.Zero)
                    throw new Exception(Resource.Exception_AllocationFailed);

                Copy(UnsafeHandle.ToPointer(), pointer.ToPointer(), size);

                IsHandleOwner = true;
            }

            Size = size;
        }

        /// <summary>
        /// Allocates new piece of memory and frees the previous one.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">If input size is less of equal zero.</exception>
        /// <exception cref="ObjectDisposedException">If object is disposed.</exception>
        /// <exception cref="Exception">If <see cref="Allocate(int, out int)"/> returns an unexpected value.</exception>
        /// <exception cref="Exception">You try reinitialize the piece of memory without owner rights.</exception>
        protected virtual void ReInitialize(ulong newSize)
        {
            ThrowIfDisposed();

            if (!IsHandleOwner)
                throw new Exception("This object is not an owner of memory handle.");

            NumericGuard.ThrowIfLessOrEqualZero(newSize, nameof(newSize));
            UnmanagedGuard.ThrowIfSizeIsTooBig(newSize, nameof(newSize));

            var newHandle = Reallocate(UnsafeHandle, newSize);

            if (newHandle == IntPtr.Zero)
                throw new Exception(Resource.Exception_AllocationFailed);

            UnsafeHandle = newHandle;
            Size = newSize;
        }

        /// <summary>
        /// Has to allocate new piece of memory for this object.
        /// </summary>
        /// <param name="size">Needed size.</param>
        /// <returns>Pointer to the allocated memory</returns>
        protected virtual IntPtr Allocate(ulong size)
        {
#if NETCOREAPP
            return new IntPtr(NativeMemory.AllocZeroed((nuint)size));
#else
            return Marshal.AllocHGlobal((IntPtr)size);
#endif
        }

        /// <summary>
        /// Reallocating existing piece of memory to desired size.
        /// </summary>
        /// <remarks>
        /// Algorithm guarantees that the pointer is valid. 
        /// </remarks>
        /// <param name="size">New size.</param>
        /// <returns>Pointer to the allocated memory</returns>
        protected virtual IntPtr Reallocate(IntPtr pointer, ulong size)
        {
#if NETCOREAPP
            return new IntPtr(NativeMemory.Realloc(pointer.ToPointer(), (nuint)size));
#else
            return Marshal.ReAllocHGlobal(pointer, (IntPtr)size);
#endif
        }

        /// <summary>
        /// Frees allocated piece of memory.
        /// </summary>
        /// <remarks>
        /// Algorithm guarantees that the pointer is valid. 
        /// </remarks>
        protected virtual void Free(IntPtr pointer)
        {
#if NETCOREAPP
            NativeMemory.Free(pointer.ToPointer());
#else
            Marshal.FreeHGlobal(pointer);
#endif
        }

        protected override void InternalDispose(bool manual)
        {
            if (IsHandleOwner)
                Free(UnsafeHandle);

            UnsafeHandle = IntPtr.Zero;
            Size = 0;

            base.InternalDispose(manual);
        }
    }
}
