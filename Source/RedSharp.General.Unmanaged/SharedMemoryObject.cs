using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using RedSharp.General.Helpers;
using RedSharp.General.Unmanaged.Abstracts;
using RedSharp.General.Unmanaged.Helpers;
using RedSharp.General.Unmanaged.Properties;

namespace RedSharp.General.Unmanaged
{
    public unsafe class SharedMemoryObject : UnmanagedHandleBase
    {
        public SharedMemoryObject(string name, uint capacity)
        {
            ArgumentsGuard.ThrowIfNullOrEmpty(name, nameof(name));
            NumericGuard.ThrowIfEqualZero(capacity, nameof(capacity));

            Name = name;
            Capacity = capacity;

            Initialize();
        }

        public string Name { get; private set; }

        public uint Capacity { get; private set; }

        private void Initialize()
        {
#if NETCOREAPP
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
#endif
                var handle = Kernel32.CreateFileMapping(IntPtr.Zero, IntPtr.Zero, Kernel32.PAGE_READWRITE, 0, Capacity, Name);

                if (handle == IntPtr.Zero)
                    throw new Win32Exception(Marshal.GetLastWin32Error());

                SetHandle(handle);
#if NETCOREAPP
            }
            else
            {
                //I do not know what is the flag is "0666" but BE AFRAID, MUHAHAHAHA
                var descriptor = Libc.ShmOpen(Name, Libc.O_CREAT | Libc.O_RDWR, 0666);

                if (descriptor == nint.Zero)
                    throw new Exception(string.Format(Resource.Exception_PosixCannotCreateSharedObject, Libc.StrError(Libc.ErrNo())));

                SetHandle(descriptor);

                var result = Libc.FTruncate(descriptor, Capacity);

                if (result == -1)
                    throw new Exception(string.Format(Resource.Exception_PosixCannotTruncateSharedObject, Libc.StrError(Libc.ErrNo())));
            }
#endif
        }

        protected override void FreeHandle(IntPtr handle)
        {
#if NETCOREAPP
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
#endif
                Kernel32.CloseHandle(handle);
#if NETCOREAPP
            else
                Libc.ShmUnlink(Name);
#endif
        }
    }
}
