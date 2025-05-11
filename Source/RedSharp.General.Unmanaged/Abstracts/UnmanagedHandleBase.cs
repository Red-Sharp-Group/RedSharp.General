using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using RedSharp.General.Abstracts;
using RedSharp.General.Unmanaged.Helpers;
using RedSharp.General.Unmanaged.Interfaces;
using RedSharp.General.Unmanaged.Properties;

namespace RedSharp.General.Unmanaged.Abstracts
{
    public abstract class UnmanagedHandleBase : DisposableBase, IUnmanagedHandle
    {
        /// <inheritdoc/>
        public bool IsHandleOwner { get; private set; }

        /// <inheritdoc/>
        public IntPtr UnsafeHandle { get; private set; }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#if NETCOREAPP
        [StackTraceHidden]
#endif
        private void ThrowIfInitialized()
        {
            if (UnsafeHandle != IntPtr.Zero)
                throw new Exception(Resource.Exception_AlreadyHasHandle);
        }

        /// <summary>
        /// Sets handle internally.
        /// Can be invoked only once.
        /// Sets <see cref="IsHandleOwner"/> in false state.
        /// </summary>
        /// <exception cref="Exception">If was initialized before.</exception>
        /// <exception cref="ObjectDisposedException"/>
        protected void SetHandle(IntPtr handle)
        {
            ThrowIfDisposed();
            ThrowIfInitialized();

            UnmanagedGuard.ThrowIfNull(handle, nameof(handle));

            UnsafeHandle = handle;

            IsHandleOwner = false;
        }

        /// <summary>
        /// Has to frees created handle.
        /// Algorithm guarantees that the handle is not <see cref="IntPtr.Zero"/>
        /// </summary>
        protected abstract void FreeHandle(IntPtr handle);

        protected override void InternalDispose(bool manual)
        {
            if (UnsafeHandle != IntPtr.Zero || !IsHandleOwner)
                FreeHandle(UnsafeHandle);

            UnsafeHandle = IntPtr.Zero;

            base.InternalDispose(manual);
        }
    }
}
