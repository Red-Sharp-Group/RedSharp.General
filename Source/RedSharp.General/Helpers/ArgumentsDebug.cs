using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using RedSharp.General.Interfaces;

namespace RedSharp.General.Helpers
{
    public static class ArgumentsDebug
    {
#if NETCOREAPP
        public const string DefaultParameterName = "value";
#endif

        /// <summary>
        /// Has no actual sense, simply to not write DEBUG every time
        /// </summary>
        public const string ConditionDebug = "DEBUG";

        /// <summary>
        /// Forces working of all check methods
        /// </summary>
        public const string ConditionForce = "FORCE_ARGUMENTS_GUARD";

        //=========================================================================//
        //CHECK OBJECT STATE

        /// <inheritdoc cref="ArgumentsGuard.ThrowIfNull"/>
        [Conditional(ConditionDebug)]
        [Conditional(ConditionForce)]
#if NETCOREAPP
        [StackTraceHidden]
        public static void ThrowIfNull(object value, [CallerArgumentExpression(nameof(value))] string name = DefaultParameterName)
#else
        public static void ThrowIfNull(object value, string name)
#endif
            => ArgumentsGuard.ThrowIfNull(value, name);

        /// <inheritdoc cref="ArgumentsGuard.ThrowIfDisposed"/>
        [Conditional(ConditionDebug)]
        [Conditional(ConditionForce)]
#if NETCOREAPP
        [StackTraceHidden]
        public static void ThrowIfDisposed(IIndicateObjectDisposed value, [CallerArgumentExpression(nameof(value))] string name = DefaultParameterName)
#else
        public static void ThrowIfDisposed(IIndicateObjectDisposed value, string name)
#endif
            => ArgumentsGuard.ThrowIfDisposed(value, name);

        /// <inheritdoc cref="ArgumentsGuard.ThrowIfNotType"/>
        [Conditional(ConditionDebug)]
        [Conditional(ConditionForce)]
#if NETCOREAPP
        [StackTraceHidden]
        public static void ThrowIfNotType<TType>(object value, [CallerArgumentExpression(nameof(value))] string name = DefaultParameterName)
#else
        public static void ThrowIfNotType<TType>(object value, string name)
#endif
            => ArgumentsGuard.ThrowIfNotType<TType>(value, name);

        //=========================================================================//
        //CHECK STRING STATE

        /// <inheritdoc cref="ArgumentsGuard.ThrowIfNullOrEmpty"/>
        [Conditional(ConditionDebug)]
        [Conditional(ConditionForce)]
#if NETCOREAPP
        [StackTraceHidden]
        public static void ThrowIfNullOrEmpty(string value, [CallerArgumentExpression(nameof(value))] string name = DefaultParameterName)
#else
        public static void ThrowIfNullOrEmpty(string value, string name)
#endif
            => ArgumentsGuard.ThrowIfNullOrEmpty(value, name);

        /// <inheritdoc cref="ArgumentsGuard.ThrowIfNullOrWhiteSpace"/>
        [Conditional(ConditionDebug)]
        [Conditional(ConditionForce)]
#if NETCOREAPP
        [StackTraceHidden]
        public static void ThrowIfNullOrWhiteSpace(string value, [CallerArgumentExpression(nameof(value))] string name = DefaultParameterName)
#else
        public static void ThrowIfNullOrWhiteSpace(string value, string name)
#endif
            => ArgumentsGuard.ThrowIfNullOrWhiteSpace(value, name);

        //=========================================================================//
        //CHECK COLLECTIONS STATE

        /// <inheritdoc cref="ArgumentsGuard.ThrowIfNullOrEmpty{TItem}"/>
        [Conditional(ConditionDebug)]
        [Conditional(ConditionForce)]
#if NETCOREAPP
        [StackTraceHidden]
        public static void ThrowIfNullOrEmpty<TItem>(IEnumerable<TItem> value, [CallerArgumentExpression(nameof(value))] string name = DefaultParameterName)
#else
        public static void ThrowIfNullOrEmpty<TItem>(IEnumerable<TItem> value, string name)
#endif
            => ArgumentsGuard.ThrowIfNullOrEmpty(value, name);

        /// <inheritdoc cref="ArgumentsGuard.ThrowIfNullOrWhiteSpace{TItem}"/>
        [Conditional(ConditionDebug)]
        [Conditional(ConditionForce)]
#if NETCOREAPP
        [StackTraceHidden]
        public static void ThrowIfNullOrWhiteSpace<TItem>(IEnumerable<TItem> value, [CallerArgumentExpression(nameof(value))] string name = DefaultParameterName)
#else
        public static void ThrowIfNullOrWhiteSpace<TItem>(IEnumerable<TItem> value, string name)
#endif
            where TItem : class => ArgumentsGuard.ThrowIfNullOrWhiteSpace<TItem>(value, name);
    }
}
