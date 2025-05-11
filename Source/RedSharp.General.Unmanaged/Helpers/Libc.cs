using System;
using System.Runtime.InteropServices;

namespace RedSharp.General.Unmanaged.Helpers
{
#if NETCOREAPP
    public static class Libc
    {
        public const string LibraryName = "libc";

        public const int O_CREAT = 0x40;
        public const int O_RDWR = 0x02;

        public const int PROT_READ = 0x1;
        public const int PROT_WRITE = 0x2;
        public const int MAP_SHARED = 0x01;

        [DllImport(LibraryName, SetLastError = true, EntryPoint = "shm_open")]
        public static extern nint ShmOpen(string name,
                                          int oflag,
                                          uint mode);

        [DllImport(LibraryName, SetLastError = true, EntryPoint = "ftruncate")]
        public static extern nint FTruncate(nint descriptor, uint size);

        [DllImport(LibraryName, SetLastError = true, EntryPoint = "mmap")]
        public static extern nint MMap(nint addr,
                                       uint length,
                                       int prot,
                                       int flags,
                                       nint fd,
                                       uint offset);

        [DllImport(LibraryName, SetLastError = true, EntryPoint = "shm_unlink")]
        public static extern int ShmUnlink(string name);

        [DllImport(LibraryName, EntryPoint = "errno")]
        public static extern int ErrNo();

        [DllImport(LibraryName, EntryPoint = "strerror")]
        public static extern IntPtr StrError(int errnum);
    }
#endif
}
