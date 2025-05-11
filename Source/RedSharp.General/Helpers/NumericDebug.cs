using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace RedSharp.General.Helpers
{
    public static class NumericDebug
    {
#if NETCOREAPP
        public const string DefaultParameterName = "value";
#endif

        /// <summary>
        /// Has no actual sense, simply to not write DEBUG every time
        /// </summary>
        public const string ConditionDebug = "DEBUG";

        /// <summary>
        /// Forces working of numeric check methods with <see cref="ArgumentOutOfRangeException"/> exceptions
        /// </summary>
        public const string ConditionForce = "FORCE_NUMERIC_GUARD";

        //=========================================================================//
        //CHECK VALUE IS NOT EQUAL TO DESIRED

        /// <inheritdoc cref="NumericGuard.ThrowIfNotEqual(long, long, string)"/>
        [Conditional(ConditionDebug)]
        [Conditional(ConditionForce)]
#if NETCOREAPP
        [StackTraceHidden]
        public static void ThrowIfNotEqual(long value, long input, [CallerArgumentExpression(nameof(value))] string name = DefaultParameterName)
#else
        public static void ThrowIfNotEqual(long value, long input, string name)
#endif
            => NumericGuard.ThrowIfNotEqual(value, input, name);

        /// <inheritdoc cref="NumericGuard.ThrowIfNotEqual(ulong, ulong, string)"/>        
        [Conditional(ConditionDebug)]
        [Conditional(ConditionForce)]
#if NETCOREAPP
        [StackTraceHidden]
        public static void ThrowIfNotEqual(ulong value, ulong input, [CallerArgumentExpression(nameof(value))] string name = DefaultParameterName)
#else
        public static void ThrowIfNotEqual(ulong value, ulong input, string name)
#endif
            => NumericGuard.ThrowIfNotEqual(value, input, name);

        /// <inheritdoc cref="NumericGuard.ThrowIfNotEqual(double, double, string)"/>        
        [Conditional(ConditionDebug)]
        [Conditional(ConditionForce)]
#if NETCOREAPP
        [StackTraceHidden]
        public static void ThrowIfNotEqual(double value, double input, [CallerArgumentExpression(nameof(value))] string name = DefaultParameterName)
#else
        public static void ThrowIfNotEqual(double value, double input, string name)
#endif
            => NumericGuard.ThrowIfNotEqual(value, input, name);

        //=========================================================================//
        //CHECK VALUE IS EQUAL TO DESIRED

        /// <inheritdoc cref="NumericGuard.ThrowIfEqual(long, long, string)"/>
        [Conditional(ConditionDebug)]
        [Conditional(ConditionForce)]
#if NETCOREAPP
        [StackTraceHidden]
        public static void ThrowIfEqual(long value, long input, [CallerArgumentExpression(nameof(value))] string name = DefaultParameterName)
#else
        public static void ThrowIfEqual(long value, long input, string name)
#endif
            => NumericGuard.ThrowIfEqual(value, input, name);

        /// <inheritdoc cref="NumericGuard.ThrowIfEqual(ulong, ulong, string)"/>        
        [Conditional(ConditionDebug)]
        [Conditional(ConditionForce)]
#if NETCOREAPP
        [StackTraceHidden]
        public static void ThrowIfEqual(ulong value, ulong input, [CallerArgumentExpression(nameof(value))] string name = DefaultParameterName)
#else
        public static void ThrowIfEqual(ulong value, ulong input, string name)
#endif
            => NumericGuard.ThrowIfEqual(value, input, name);

        /// <inheritdoc cref="NumericGuard.ThrowIfEqual(double, double, string)"/>        
        [Conditional(ConditionDebug)]
        [Conditional(ConditionForce)]
#if NETCOREAPP
        [StackTraceHidden]
        public static void ThrowIfEqual(double value, double input, [CallerArgumentExpression(nameof(value))] string name = DefaultParameterName)
#else
        public static void ThrowIfEqual(double value, double input, string name)
#endif
            => NumericGuard.ThrowIfEqual(value, input, name);

        //=========================================================================//
        //CHECK VALUE LESS THAN DESIRED

        /// <inheritdoc cref="NumericGuard.ThrowIfLess(long, long, string)"/>        
        [Conditional(ConditionDebug)]
        [Conditional(ConditionForce)]
#if NETCOREAPP
        [StackTraceHidden]
        public static void ThrowIfLess(long value, long desired, [CallerArgumentExpression(nameof(value))] string name = DefaultParameterName)
#else
        public static void ThrowIfLess(long value, long desired, string name)
#endif
            => NumericGuard.ThrowIfLess(value, desired, name);

        /// <inheritdoc cref="NumericGuard.ThrowIfLess(ulong, ulong, string)"/>        
        [Conditional(ConditionDebug)]
        [Conditional(ConditionForce)]
#if NETCOREAPP
        [StackTraceHidden]
        public static void ThrowIfLess(ulong value, ulong desired, [CallerArgumentExpression(nameof(value))] string name = DefaultParameterName)
#else
        public static void ThrowIfLess(ulong value, ulong desired, string name)
#endif
            => NumericGuard.ThrowIfLess(value, desired, name);

        /// <inheritdoc cref="NumericGuard.ThrowIfLess(double, double, string)"/>        
        [Conditional(ConditionDebug)]
        [Conditional(ConditionForce)]
#if NETCOREAPP
        [StackTraceHidden]
        public static void ThrowIfLess(double value, double desired, [CallerArgumentExpression(nameof(value))] string name = DefaultParameterName)
#else
        public static void ThrowIfLess(double value, double desired, string name)
#endif
            => NumericGuard.ThrowIfLess(value, desired, name);

        /// <inheritdoc cref="NumericGuard.ThrowIfLessOrEqual(long, long, string)"/>        
        [Conditional(ConditionDebug)]
        [Conditional(ConditionForce)]
#if NETCOREAPP
        [StackTraceHidden]
        public static void ThrowIfLessOrEqual(long value, long desired, [CallerArgumentExpression(nameof(value))] string name = DefaultParameterName)
#else
        public static void ThrowIfLessOrEqual(long value, long desired, string name)
#endif
            => NumericGuard.ThrowIfLessOrEqual(value, desired, name);

        /// <inheritdoc cref="NumericGuard.ThrowIfLessOrEqual(ulong, ulong, string)"/>        
        [Conditional(ConditionDebug)]
        [Conditional(ConditionForce)]
#if NETCOREAPP
        [StackTraceHidden]
        public static void ThrowIfLessOrEqual(ulong value, ulong desired, [CallerArgumentExpression(nameof(value))] string name = DefaultParameterName)
#else
        public static void ThrowIfLessOrEqual(ulong value, ulong desired, string name)
#endif
            => NumericGuard.ThrowIfLessOrEqual(value, desired, name);

        /// <inheritdoc cref="NumericGuard.ThrowIfLessOrEqual(double, double, string)"/>        
        [Conditional(ConditionDebug)]
        [Conditional(ConditionForce)]
#if NETCOREAPP
        [StackTraceHidden]
        public static void ThrowIfLessOrEqual(double value, double desired, [CallerArgumentExpression(nameof(value))] string name = DefaultParameterName)
#else
        public static void ThrowIfLessOrEqual(double value, double desired, string name)
#endif
            => NumericGuard.ThrowIfLessOrEqual(value, desired, name);

        //=========================================================================//
        //CHECK VALUE IS GREATER THAN DESIRED

        /// <inheritdoc cref="NumericGuard.ThrowIfGreater(long, long, string)"/>        
        [Conditional(ConditionDebug)]
        [Conditional(ConditionForce)]
#if NETCOREAPP
        [StackTraceHidden]
        public static void ThrowIfGreater(long value, long desired, [CallerArgumentExpression(nameof(value))] string name = DefaultParameterName)
#else
        public static void ThrowIfGreater(long value, long desired, string name)
#endif
            => NumericGuard.ThrowIfGreater(value, desired, name);

        /// <inheritdoc cref="NumericGuard.ThrowIfGreater(ulong, ulong, string)"/>        
        [Conditional(ConditionDebug)]
        [Conditional(ConditionForce)]
#if NETCOREAPP
        [StackTraceHidden]
        public static void ThrowIfGreater(ulong value, ulong desired, [CallerArgumentExpression(nameof(value))] string name = DefaultParameterName)
#else
        public static void ThrowIfGreater(ulong value, ulong desired, string name)
#endif
            => NumericGuard.ThrowIfGreater(value, desired, name);

        /// <inheritdoc cref="NumericGuard.ThrowIfGreater(double, double, string)"/>        
        [Conditional(ConditionDebug)]
        [Conditional(ConditionForce)]
#if NETCOREAPP
        [StackTraceHidden]
        public static void ThrowIfGreater(double value, double desired, [CallerArgumentExpression(nameof(value))] string name = DefaultParameterName)
#else
        public static void ThrowIfGreater(double value, double desired, string name)
#endif
            => NumericGuard.ThrowIfGreater(value, desired, name);

        /// <inheritdoc cref="NumericGuard.ThrowIfGreaterOrEqual(long, long, string)"/>        
        [Conditional(ConditionDebug)]
        [Conditional(ConditionForce)]
#if NETCOREAPP
        [StackTraceHidden]
        public static void ThrowIfGreaterOrEqual(long value, long desired, [CallerArgumentExpression(nameof(value))] string name = DefaultParameterName)
#else
        public static void ThrowIfGreaterOrEqual(long value, long desired, string name)
#endif
            => NumericGuard.ThrowIfGreaterOrEqual(value, desired, name);

        /// <inheritdoc cref="NumericGuard.ThrowIfGreaterOrEqual(ulong, ulong, string)"/>        
        [Conditional(ConditionDebug)]
        [Conditional(ConditionForce)]
#if NETCOREAPP
        [StackTraceHidden]
        public static void ThrowIfGreaterOrEqual(ulong value, ulong desired, [CallerArgumentExpression(nameof(value))] string name = DefaultParameterName)
#else
        public static void ThrowIfGreaterOrEqual(ulong value, ulong desired, string name)
#endif
            => NumericGuard.ThrowIfGreaterOrEqual(value, desired, name);

        /// <inheritdoc cref="NumericGuard.ThrowIfGreaterOrEqual(double, double, string)"/>        
        [Conditional(ConditionDebug)]
        [Conditional(ConditionForce)]
#if NETCOREAPP
        [StackTraceHidden]
        public static void ThrowIfGreaterOrEqual(double value, double desired, [CallerArgumentExpression(nameof(value))] string name = DefaultParameterName)
#else
        public static void ThrowIfGreaterOrEqual(double value, double desired, string name)
#endif
            => NumericGuard.ThrowIfGreaterOrEqual(value, desired, name);

        //=========================================================================//
        //CHECK VALUE IN COMPARE TO ZERO

        /// <inheritdoc cref="NumericGuard.ThrowIfLessZero(long, string)"/>        
        [Conditional(ConditionDebug)]
        [Conditional(ConditionForce)]
#if NETCOREAPP
        [StackTraceHidden]
        public static void ThrowIfLessZero(long value, [CallerArgumentExpression(nameof(value))] string name = DefaultParameterName)
#else
        public static void ThrowIfLessZero(long value, string name)
#endif
            => NumericGuard.ThrowIfLessZero(value, name);

        /// <inheritdoc cref="NumericGuard.ThrowIfLessZero(double, string)"/>        
        [Conditional(ConditionDebug)]
        [Conditional(ConditionForce)]
#if NETCOREAPP
        [StackTraceHidden]
        public static void ThrowIfLessZero(double value, [CallerArgumentExpression(nameof(value))] string name = DefaultParameterName)
#else
        public static void ThrowIfLessZero(double value, string name)
#endif
            => NumericGuard.ThrowIfLessZero(value, name);

        /// <inheritdoc cref="NumericGuard.ThrowIfLessOrEqualZero(long, string)"/>        
        [Conditional(ConditionDebug)]
        [Conditional(ConditionForce)]
#if NETCOREAPP
        [StackTraceHidden]
        public static void ThrowIfLessOrEqualZero(long value, [CallerArgumentExpression(nameof(value))] string name = DefaultParameterName)
#else
        public static void ThrowIfLessOrEqualZero(long value, string name)
#endif
            => NumericGuard.ThrowIfLessOrEqualZero(value, name);

        /// <inheritdoc cref="NumericGuard.ThrowIfLessOrEqualZero(double, string)"/>        
        [Conditional(ConditionDebug)]
        [Conditional(ConditionForce)]
#if NETCOREAPP
        [StackTraceHidden]
        public static void ThrowIfLessOrEqualZero(double value, [CallerArgumentExpression(nameof(value))] string name = DefaultParameterName)
#else
        public static void ThrowIfLessOrEqualZero(double value, string name)
#endif
            => NumericGuard.ThrowIfLessOrEqualZero(value, name);

        /// <inheritdoc cref="NumericGuard.ThrowIfEqualZero(long, string)"/>        
        [Conditional(ConditionDebug)]
        [Conditional(ConditionForce)]
#if NETCOREAPP
        [StackTraceHidden]
        public static void ThrowIfEqualZero(long value, [CallerArgumentExpression(nameof(value))] string name = DefaultParameterName)
#else
        public static void ThrowIfEqualZero(long value, string name)
#endif
            => NumericGuard.ThrowIfEqualZero(value, name);

        /// <inheritdoc cref="NumericGuard.ThrowIfEqualZero(ulong, string)"/>        
        [Conditional(ConditionDebug)]
        [Conditional(ConditionForce)]
#if NETCOREAPP
        [StackTraceHidden]
        public static void ThrowIfEqualZero(ulong value, [CallerArgumentExpression(nameof(value))] string name = DefaultParameterName)
#else
        public static void ThrowIfEqualZero(ulong value, string name)
#endif
            => NumericGuard.ThrowIfEqualZero(value, name);

        /// <inheritdoc cref="NumericGuard.ThrowIfEqualZero(double, string)"/>        
        [Conditional(ConditionDebug)]
        [Conditional(ConditionForce)]
#if NETCOREAPP
        [StackTraceHidden]
        public static void ThrowIfEqualZero(double value, [CallerArgumentExpression(nameof(value))] string name = DefaultParameterName)
#else
        public static void ThrowIfEqualZero(double value, string name)
#endif
            => NumericGuard.ThrowIfEqualZero(value, name);
    }
}
