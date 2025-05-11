using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using RedSharp.General.Properties;

namespace RedSharp.General.Helpers
{
    public static class NumericGuard
    {
#if NETCOREAPP
        public const string DefaultParameterName = "value";
#endif
        //=========================================================================//
        //CHECK VALUE IS NOT EQUAL TO DESIRED

        /// <summary>
        /// Throws an <see cref="ArgumentOutOfRangeException"/> if the input signed integer number is not equal to input.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#if NETCOREAPP
        [StackTraceHidden]
        public static void ThrowIfNotEqual(long value, long input, [CallerArgumentExpression(nameof(value))] string name = DefaultParameterName)
#else
        public static void ThrowIfNotEqual(long value, long input, string name)
#endif
        {
            if (value != input)
                throw new ArgumentOutOfRangeException(name, string.Format(Resource.Exception_ValueIsEqual, value, input));
        }

        /// <summary>
        /// Throws an <see cref="ArgumentOutOfRangeException"/> if the input unsigned integer number is not equal to input.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#if NETCOREAPP
        [StackTraceHidden]
        public static void ThrowIfNotEqual(ulong value, ulong input, [CallerArgumentExpression(nameof(value))] string name = DefaultParameterName)
#else
        public static void ThrowIfNotEqual(ulong value, ulong input, string name)
#endif
        {
            if (value != input)
                throw new ArgumentOutOfRangeException(name, string.Format(Resource.Exception_ValueIsEqual, value, input));
        }

        /// <summary>
        /// Throws an <see cref="ArgumentOutOfRangeException"/> if the input float number is not equal to input.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#if NETCOREAPP
        [StackTraceHidden]
        public static void ThrowIfNotEqual(double value, double input, [CallerArgumentExpression(nameof(value))] string name = DefaultParameterName)
#else
        public static void ThrowIfNotEqual(double value, double input, string name)
#endif
        {
            if (Math.Abs(value - input) >= double.Epsilon)
                throw new ArgumentOutOfRangeException(name, string.Format(Resource.Exception_ValueIsEqual, value, input));
        }

        //=========================================================================//
        //CHECK VALUE IS EQUAL TO DESIRED

        /// <summary>
        /// Throws an <see cref="ArgumentOutOfRangeException"/> if the input signed integer number is equal to input.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#if NETCOREAPP
        [StackTraceHidden]
        public static void ThrowIfEqual(long value, long input, [CallerArgumentExpression(nameof(value))] string name = DefaultParameterName)
#else
        public static void ThrowIfEqual(long value, long input, string name)
#endif
        {
            if (value == input)
                throw new ArgumentOutOfRangeException(name, string.Format(Resource.Exception_ValueIsEqual, value, input));
        }

        /// <summary>
        /// Throws an <see cref="ArgumentOutOfRangeException"/> if the input unsigned integer number is equal to input.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#if NETCOREAPP
        [StackTraceHidden]
        public static void ThrowIfEqual(ulong value, ulong input, [CallerArgumentExpression(nameof(value))] string name = DefaultParameterName)
#else
        public static void ThrowIfEqual(ulong value, ulong input, string name)
#endif
        {
            if (value == input)
                throw new ArgumentOutOfRangeException(name, string.Format(Resource.Exception_ValueIsEqual, value, input));
        }

        /// <summary>
        /// Throws an <see cref="ArgumentOutOfRangeException"/> if the input float number is equal to input.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#if NETCOREAPP
        [StackTraceHidden]
        public static void ThrowIfEqual(double value, double input, [CallerArgumentExpression(nameof(value))] string name = DefaultParameterName)
#else
        public static void ThrowIfEqual(double value, double input, string name)
#endif
        {
            if (Math.Abs(value - input) < double.Epsilon)
                throw new ArgumentOutOfRangeException(name, string.Format(Resource.Exception_ValueIsEqual, value, input));
        }

        //=========================================================================//
        //CHECK VALUE IS LESS THAN DESIRED

        /// <summary>
        /// Throws an <see cref="ArgumentOutOfRangeException"/> if the input signed integer number is less then desired.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#if NETCOREAPP
        [StackTraceHidden]
        public static void ThrowIfLess(long value, long desired, [CallerArgumentExpression(nameof(value))] string name = DefaultParameterName)
#else
        public static void ThrowIfLess(long value, long desired, string name)
#endif
        {
            if (value < desired)
                throw new ArgumentOutOfRangeException(name, string.Format(Resource.Exception_ValueIsLess, value, desired));
        }

        /// <summary>
        /// Throws an <see cref="ArgumentOutOfRangeException"/> if the input unsigned integer number is less then desired.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#if NETCOREAPP
        [StackTraceHidden]
        public static void ThrowIfLess(ulong value, ulong desired, [CallerArgumentExpression(nameof(value))] string name = DefaultParameterName)
#else
        public static void ThrowIfLess(ulong value, ulong desired, string name)
#endif
        {
            if (value < desired)
                throw new ArgumentOutOfRangeException(name, string.Format(Resource.Exception_ValueIsLess, value, desired));
        }

        /// <summary>
        /// Throws an <see cref="ArgumentOutOfRangeException"/> if the input float number is less then desired.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#if NETCOREAPP
        [StackTraceHidden]
        public static void ThrowIfLess(double value, double desired, [CallerArgumentExpression(nameof(value))] string name = DefaultParameterName)
#else
        public static void ThrowIfLess(double value, double desired, string name)
#endif
        {
            if (value < desired)
                throw new ArgumentOutOfRangeException(name, string.Format(Resource.Exception_ValueIsLess, value, desired));
        }

        /// <summary>
        /// Throws an <see cref="ArgumentOutOfRangeException"/> if the input signed integer number is less or equal to desired.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#if NETCOREAPP
        [StackTraceHidden]
        public static void ThrowIfLessOrEqual(long value, long desired, [CallerArgumentExpression(nameof(value))] string name = DefaultParameterName)
#else
        public static void ThrowIfLessOrEqual(long value, long desired, string name)
#endif
        {
            if (value <= desired)
                throw new ArgumentOutOfRangeException(name, string.Format(Resource.Exception_ValueIsLessOrEqual, value, desired));
        }

        /// <summary>
        /// Throws an <see cref="ArgumentOutOfRangeException"/> if the input unsigned integer number is less or equal to desired.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#if NETCOREAPP
        [StackTraceHidden]
        public static void ThrowIfLessOrEqual(ulong value, ulong desired, [CallerArgumentExpression(nameof(value))] string name = DefaultParameterName)
#else
        public static void ThrowIfLessOrEqual(ulong value, ulong desired, string name)
#endif
        {
            if (value <= desired)
                throw new ArgumentOutOfRangeException(name, string.Format(Resource.Exception_ValueIsLessOrEqual, value, desired));
        }

        /// <summary>
        /// Throws an <see cref="ArgumentOutOfRangeException"/> if the input float number is less or equal to desired.
        /// </summary>
        /// <remarks>
        /// Uses <see cref="double.Epsilon"/> for equal comparison.
        /// </remarks>
        /// <exception cref="ArgumentOutOfRangeException"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#if NETCOREAPP
        [StackTraceHidden]
        public static void ThrowIfLessOrEqual(double value, double desired, [CallerArgumentExpression(nameof(value))] string name = DefaultParameterName)
#else
        public static void ThrowIfLessOrEqual(double value, double desired, string name)
#endif
        {
            if (value < desired || Math.Abs(value - desired) < double.Epsilon)
                throw new ArgumentOutOfRangeException(name, string.Format(Resource.Exception_ValueIsLessOrEqual, value, desired));
        }

        //=========================================================================//
        //CHECK VALUE IS GREATER THAN DESIRED

        /// <summary>
        /// Throws an <see cref="ArgumentOutOfRangeException"/> if the input signed integer number is greater than desired.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#if NETCOREAPP
        [StackTraceHidden]
        public static void ThrowIfGreater(long value, long desired, [CallerArgumentExpression(nameof(value))] string name = DefaultParameterName)
#else
        public static void ThrowIfGreater(long value, long desired, string name)
#endif
        {
            if (value > desired)
                throw new ArgumentOutOfRangeException(name, string.Format(Resource.Exception_ValueIsGreater, value, desired));
        }

        /// <summary>
        /// Throws an <see cref="ArgumentOutOfRangeException"/> if the input unsigned integer number is greater than desired.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#if NETCOREAPP
        [StackTraceHidden]
        public static void ThrowIfGreater(ulong value, ulong desired, [CallerArgumentExpression(nameof(value))] string name = DefaultParameterName)
#else
        public static void ThrowIfGreater(ulong value, ulong desired, string name)
#endif
        {
            if (value > desired)
                throw new ArgumentOutOfRangeException(name, string.Format(Resource.Exception_ValueIsGreater, value, desired));
        }

        /// <summary>
        /// Throws an <see cref="ArgumentOutOfRangeException"/> if the input float number is greater than desired.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#if NETCOREAPP
        [StackTraceHidden]
        public static void ThrowIfGreater(double value, double desired, [CallerArgumentExpression(nameof(value))] string name = DefaultParameterName)
#else
        public static void ThrowIfGreater(double value, double desired, string name)
#endif
        {
            if (value > desired)
                throw new ArgumentOutOfRangeException(name, string.Format(Resource.Exception_ValueIsGreater, value, desired));
        }

        /// <summary>
        /// Throws an <see cref="ArgumentOutOfRangeException"/> if the input signed integer number is greater or equal to desired.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#if NETCOREAPP
        [StackTraceHidden]
        public static void ThrowIfGreaterOrEqual(long value, long desired, [CallerArgumentExpression(nameof(value))] string name = DefaultParameterName)
#else
        public static void ThrowIfGreaterOrEqual(long value, long desired, string name)
#endif
        {
            if (value >= desired)
                throw new ArgumentOutOfRangeException(name, string.Format(Resource.Exception_ValueIsGreaterOrEqual, value, desired));
        }

        /// <summary>
        /// Throws an <see cref="ArgumentOutOfRangeException"/> if the input unsigned integer number is greater or equal to desired.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#if NETCOREAPP
        [StackTraceHidden]
        public static void ThrowIfGreaterOrEqual(ulong value, ulong desired, [CallerArgumentExpression(nameof(value))] string name = DefaultParameterName)
#else
        public static void ThrowIfGreaterOrEqual(ulong value, ulong desired, string name)
#endif
        {
            if (value >= desired)
                throw new ArgumentOutOfRangeException(name, string.Format(Resource.Exception_ValueIsGreaterOrEqual, value, desired));
        }

        /// <summary>
        /// Throws an <see cref="ArgumentOutOfRangeException"/> if the input float number is greater or equal to desired.
        /// </summary>
        /// <remarks>
        /// Uses <see cref="double.Epsilon"/> for equal comparison.
        /// </remarks>
        /// <exception cref="ArgumentOutOfRangeException"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#if NETCOREAPP
        [StackTraceHidden]
        public static void ThrowIfGreaterOrEqual(double value, double desired, [CallerArgumentExpression(nameof(value))] string name = DefaultParameterName)
#else
        public static void ThrowIfGreaterOrEqual(double value, double desired, string name)
#endif
        {
            if (value > desired || Math.Abs(value - desired) < double.Epsilon)
                throw new ArgumentOutOfRangeException(name, string.Format(Resource.Exception_ValueIsGreaterOrEqual, value, desired));
        }

        //=========================================================================//
        //CHECK VALUE IN COMPARE TO ZERO

        /// <summary>
        /// Throws an <see cref="ArgumentOutOfRangeException"/> if the input signed integer number is less 0.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#if NETCOREAPP
        [StackTraceHidden]
        public static void ThrowIfLessZero(long value, [CallerArgumentExpression(nameof(value))] string name = DefaultParameterName)
#else
        public static void ThrowIfLessZero(long value, string name)
#endif
        {
            ThrowIfLess(value, 0, name);
        }

        /// <summary>
        /// Throws an <see cref="ArgumentOutOfRangeException"/> if the input float number is less 0.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#if NETCOREAPP
        [StackTraceHidden]
        public static void ThrowIfLessZero(double value, [CallerArgumentExpression(nameof(value))] string name = DefaultParameterName)
#else
        public static void ThrowIfLessZero(double value, string name)
#endif
        {
            ThrowIfLess(value, 0, name);
        }

        /// <summary>
        /// Throws an <see cref="ArgumentOutOfRangeException"/> if the input signed integer number is less or equal to 0.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#if NETCOREAPP
        [StackTraceHidden]
        public static void ThrowIfLessOrEqualZero(long value, [CallerArgumentExpression(nameof(value))] string name = DefaultParameterName)
#else
        public static void ThrowIfLessOrEqualZero(long value, string name)
#endif
        {
            ThrowIfLessOrEqual(value, 0, name);
        }

        /// <summary>
        /// Throws an <see cref="ArgumentOutOfRangeException"/> if the input float number is less or equal to 0.
        /// </summary>
        /// <remarks>
        /// Uses <see cref="ApproximationValue"/> for equal comparison.
        /// </remarks>
        /// <exception cref="ArgumentOutOfRangeException"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#if NETCOREAPP
        [StackTraceHidden]
        public static void ThrowIfLessOrEqualZero(double value, [CallerArgumentExpression(nameof(value))] string name = DefaultParameterName)
#else
        public static void ThrowIfLessOrEqualZero(double value, string name)
#endif
        {
            ThrowIfLessOrEqual(value, 0, name);
        }

        /// <summary>
        /// Throws an <see cref="ArgumentOutOfRangeException"/> if the input signed integer number is less or equal to 0.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#if NETCOREAPP
        [StackTraceHidden]
        public static void ThrowIfEqualZero(long value, [CallerArgumentExpression(nameof(value))] string name = DefaultParameterName)
#else
        public static void ThrowIfEqualZero(long value, string name)
#endif
        {
            ThrowIfEqual(value, 0, name);
        }

        /// <summary>
        /// Throws an <see cref="ArgumentOutOfRangeException"/> if the input unsigned integer number is less or equal to 0.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#if NETCOREAPP
        [StackTraceHidden]
        public static void ThrowIfEqualZero(ulong value, [CallerArgumentExpression(nameof(value))] string name = DefaultParameterName)
#else
        public static void ThrowIfEqualZero(ulong value, string name)
#endif
        {
            ThrowIfEqual(value, 0, name);
        }

        /// <summary>
        /// Throws an <see cref="ArgumentOutOfRangeException"/> if the input float number is less or equal to 0.
        /// </summary>
        /// <remarks>
        /// Uses <see cref="ApproximationValue"/> for equal comparison.
        /// </remarks>
        /// <exception cref="ArgumentOutOfRangeException"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#if NETCOREAPP
        [StackTraceHidden]
        public static void ThrowIfEqualZero(double value, [CallerArgumentExpression(nameof(value))] string name = DefaultParameterName)
#else
        public static void ThrowIfEqualZero(double value, string name)
#endif
        {
            ThrowIfEqual(value, 0, name);
        }
    }
}
