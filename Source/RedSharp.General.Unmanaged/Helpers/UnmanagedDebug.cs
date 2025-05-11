using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace RedSharp.General.Unmanaged.Helpers
{
    public static unsafe class UnmanagedDebug
    {
#if NETCOREAPP
        public const string DefaultParameterName = "value";
        public const string VerifiablePointerName = "verifiablePointer";
#endif

        /// <summary>
        /// Has no actual sense, simply to not write DEBUG every time
        /// </summary>
        public const string ConditionDebug = "DEBUG";

        /// <summary>
        /// Forces working of all check methods
        /// </summary>
        public const string ConditionForce = "FORCE_UNMANAGED_GUARD";

        /// <inheritdoc cref="UnmanagedGuard.ThrowIfNull(IntPtr, string)"/>
        [Conditional(ConditionDebug)]
        [Conditional(ConditionForce)]
#if NETCOREAPP
        [StackTraceHidden]
        public static void ThrowIfNull(IntPtr value, [CallerArgumentExpression(nameof(value))] string name = DefaultParameterName)
#else
        public static void ThrowIfNull(IntPtr value, string name)
#endif
            => UnmanagedGuard.ThrowIfNull(value, name);

        /// <inheritdoc cref="UnmanagedGuard.ThrowIfNull(void*, string)"/>
        [Conditional(ConditionDebug)]
        [Conditional(ConditionForce)]
#if NETCOREAPP
        [StackTraceHidden]
        public static void ThrowIfNull(void* value, [CallerArgumentExpression(nameof(value))] string name = DefaultParameterName)
#else
        public static void ThrowIfNull(void* value, string name)
#endif
            => UnmanagedGuard.ThrowIfNull(value, name);

        /// <inheritdoc cref="UnmanagedGuard.ThrowIfSizeIsTooBig(ulong, string)"/>
        [Conditional(ConditionDebug)]
        [Conditional(ConditionForce)]
#if NETCOREAPP
        [StackTraceHidden]
        public static void ThrowIfSizeIsTooBig(ulong value, [CallerArgumentExpression(nameof(value))] string name = DefaultParameterName)
#else
        public static void ThrowIfSizeIsTooBig(ulong value, string name)
#endif
            => UnmanagedGuard.ThrowIfSizeIsTooBig(value, name);

        /// <inheritdoc cref="UnmanagedGuard.ThrowIfPointerIsOutOfRange(nint, ulong, nint, ulong, string)"/>
        [Conditional(ConditionDebug)]
        [Conditional(ConditionForce)]
#if NETCOREAPP
        [StackTraceHidden]
        public static void ThrowIfPointerIsOutOfRange(void* targetPointer, ulong targetSize, void* verifiablePointer, ulong verifiableSize, 
            [CallerArgumentExpression(nameof(verifiablePointer))] string name = VerifiablePointerName)
#else
        public static void ThrowIfPointerIsOutOfRange(void* targetPointer, ulong targetSize, void* verifiablePointer, ulong verifiableSize, string name)
#endif
            => UnmanagedGuard.ThrowIfPointerIsOutOfRange(targetPointer, targetSize, verifiablePointer, verifiableSize, name);

        /// <inheritdoc cref="UnmanagedGuard.ThrowIfPointerIsOutOfRange(void*, ulong, void*, ulong, string)"/>
        [Conditional(ConditionDebug)]
        [Conditional(ConditionForce)]
#if NETCOREAPP
        [StackTraceHidden]
        public static void ThrowIfPointerIsOutOfRange(IntPtr targetPointer, ulong targetSize, IntPtr verifiablePointer, ulong verifiableSize,
            [CallerArgumentExpression(nameof(verifiablePointer))] string name = VerifiablePointerName)
#else
        public static void ThrowIfPointerIsOutOfRange(IntPtr targetPointer, ulong targetSize, IntPtr verifiablePointer, ulong verifiableSize, string name)
#endif
            => UnmanagedGuard.ThrowIfPointerIsOutOfRange(targetPointer, targetSize, verifiablePointer, verifiableSize, name);
    }
}
