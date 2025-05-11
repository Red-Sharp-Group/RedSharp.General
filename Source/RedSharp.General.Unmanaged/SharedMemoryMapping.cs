using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using RedSharp.General.Abstracts;
using RedSharp.General.Helpers;
using RedSharp.General.Unmanaged.Helpers;
using RedSharp.General.Unmanaged.Interfaces;

namespace RedSharp.General.Unmanaged
{
    public unsafe class SharedMemoryMapping : DisposableBase, IUnmanagedMemory
    {
        private SharedMemoryObject _owner;

        public SharedMemoryMapping(SharedMemoryObject owner)
        {
            ArgumentsGuard.ThrowIfNull(owner, nameof(owner));
            ArgumentsGuard.ThrowIfDisposed(owner, nameof(owner));

            _owner = owner;

            Initialize();
        }

        public override bool IsDisposed => base.IsDisposed || _owner.IsDisposed;

        /// <inheritdoc/>
        public ulong Size => _owner.Capacity;

        /// <inheritdoc/>
        public bool IsHandleOwner => false;

        /// <inheritdoc/>
        public IntPtr UnsafeHandle { get; private set; }

        private void Initialize()
        {
#if NETCOREAPP
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
#endif
                var pointer = Kernel32.MapViewOfFile(_owner.UnsafeHandle, Kernel32.FILE_MAP_ALL_ACCESS, 0, 0, 0);

                if (pointer == IntPtr.Zero)
                    throw new Win32Exception(Marshal.GetLastWin32Error());

                UnsafeHandle = pointer;
#if NETCOREAPP
            }
            else
            {
                var pointer = Libc.MMap(nint.Zero, _owner.Capacity, Libc.PROT_READ | Libc.PROT_WRITE, Libc.MAP_SHARED, _owner.UnsafeHandle, 0);

                if (pointer == nint.Zero)
                    throw new Exception($"Failed to map shared memory object: {Libc.ErrNo()}");

                UnsafeHandle = pointer;
            }
#endif
        }
    }
}
