using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace RedSharp.General.Unmanaged.Helpers
{
    public static unsafe class UnmanagedGuard
    {
#if NETCOREAPP
        public const string DefaultParameterName = "value";
        public const string VerifiablePointerName = "verifiablePointer";
#endif

        /// <summary>
        /// Throws an <see cref="ArgumentNullException"/> if the input pointer is null.
        /// </summary>
        /// <exception cref="ArgumentNullException"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#if NETCOREAPP
        [StackTraceHidden]
        public static void ThrowIfNull(IntPtr value, [CallerArgumentExpression(nameof(value))] string name = DefaultParameterName)
#else
        public static void ThrowIfNull(IntPtr value, string name)
#endif
        {
            if (value == IntPtr.Zero)
                throw new ArgumentNullException(name);
        }

        /// <summary>
        /// Throws an <see cref="ArgumentNullException"/> if the input pointer is null.
        /// </summary>
        /// <exception cref="ArgumentNullException"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#if NETCOREAPP
        [StackTraceHidden]
        public static void ThrowIfNull(void* value, [CallerArgumentExpression(nameof(value))] string name = DefaultParameterName)
#else
        public static void ThrowIfNull(void* value, string name)
#endif
        {
            if (value == null)
                throw new ArgumentNullException(name);
        }

        /// <summary>
        /// Throws an <see cref="ArgumentNullException"/> if the input pointer is null.
        /// </summary>
        /// <exception cref="ArgumentNullException"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#if NETCOREAPP
        [StackTraceHidden]
        public static void ThrowIfSizeIsTooBig(ulong value, [CallerArgumentExpression(nameof(value))] string name = DefaultParameterName)
#else
        public static void ThrowIfSizeIsTooBig(ulong value, string name)
#endif
        {
            if (!Environment.Is64BitProcess && value > uint.MaxValue)
                throw new ArgumentNullException(name);
        }

        /// <summary>
        /// Throws an <see cref="ArgumentOutOfRangeException"/> if the verifiable structure is out of the range of the target structure.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#if NETCOREAPP
        [StackTraceHidden]
        public static void ThrowIfPointerIsOutOfRange(void* targetPointer,
                                                      ulong targetSize,
                                                      void* verifiablePointer,
                                                      ulong verifiableSize,
                                                      [CallerArgumentExpression(nameof(verifiablePointer))] string name = VerifiablePointerName)
#else
        public static void ThrowIfPointerIsOutOfRange(void* targetPointer, ulong targetSize, void* verifiablePointer, ulong verifiableSize, string name)
#endif
        {
            if ((ulong)verifiablePointer < (ulong)targetPointer ||
                (ulong)verifiablePointer + verifiableSize > (ulong)targetPointer + targetSize)
            {
                throw new ArgumentOutOfRangeException(name);
            }
        }

        /// <summary>
        /// Throws an <see cref="ArgumentOutOfRangeException"/> if the verifiable structure is out of the range of the target structure.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#if NETCOREAPP
        [StackTraceHidden]
        public static void ThrowIfPointerIsOutOfRange(IntPtr targetPointer,
                                                      ulong targetSize,
                                                      IntPtr verifiablePointer,
                                                      ulong verifiableSize,
                                                      [CallerArgumentExpression(nameof(verifiablePointer))] string name = VerifiablePointerName)
#else
        public static void ThrowIfPointerIsOutOfRange(IntPtr targetPointer, ulong targetSize, IntPtr verifiablePointer, ulong verifiableSize, string name)
#endif
        {
            if ((ulong)verifiablePointer < (ulong)targetPointer ||
                (ulong)verifiablePointer + verifiableSize > (ulong)targetPointer + targetSize)
            {
                throw new ArgumentOutOfRangeException(name);
            }
        }
    }
}
