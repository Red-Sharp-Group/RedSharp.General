using System;
using System.Runtime.InteropServices;

namespace RedSharp.General.Unmanaged.Helpers
{
    public static class Kernel32
    {
        public const string LibraryName = "Kernel32.dll";

        public const uint PAGE_READWRITE = 0x04;
        public const uint FILE_MAP_ALL_ACCESS = 0xF001F;

        [DllImport(LibraryName, SetLastError = true)]
        public static extern IntPtr CreateFileMapping(IntPtr hFile,
                                                      IntPtr lpFileMappingAttributes,
                                                      uint flProtect,
                                                      uint dwMaximumSizeHigh,
                                                      uint dwMaximumSizeLow,
                                                      string lpName);

        [DllImport(LibraryName, SetLastError = true)]
        public static extern IntPtr MapViewOfFile(IntPtr hFileMappingObject,
                                                  uint dwDesiredAccess,
                                                  uint dwFileOffsetHigh,
                                                  uint dwFileOffsetLow,
                                                  uint dwNumberOfBytesToMap);

        [DllImport(LibraryName, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool CloseHandle(IntPtr handle);
    }
}
