using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using RedSharp.General.Interfaces;
using RedSharp.General.Properties;

namespace RedSharp.General.Helpers
{
    public static class ArgumentsGuard
    {
#if NETCOREAPP
        public const string DefaultParameterName = "value";
#endif

        //=========================================================================//
        //CHECK OBJECT STATE

        /// <summary>
        /// Throws an <see cref="ArgumentNullException"/> if the input object link is null.
        /// </summary>
        /// <exception cref="ArgumentNullException"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#if NETCOREAPP
        [StackTraceHidden]
        public static void ThrowIfNull(object value, [CallerArgumentExpression(nameof(value))] string name = DefaultParameterName)
#else
        public static void ThrowIfNull(object value, string name)
#endif
        {
            if (value == null)
                throw new ArgumentNullException(name);
        }

        /// <summary>
        /// Throws an <see cref="ObjectDisposedException"/> if the input object is disposed.
        /// </summary>
        /// <remarks>
        /// Method doesn't check object on null.
        /// <br/>Object has to implement <see cref="IIndicateObjectDisposed"/>
        /// </remarks>
        /// <exception cref="ObjectDisposedException"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#if NETCOREAPP
        [StackTraceHidden]
        public static void ThrowIfDisposed(IIndicateObjectDisposed value, [CallerArgumentExpression(nameof(value))] string name = DefaultParameterName)
#else
        public static void ThrowIfDisposed(IIndicateObjectDisposed value, string name)
#endif
        {
            if (value.IsDisposed)
                throw new ObjectDisposedException(name, string.Format(Resource.Exception_ObjectIsDisposed, value.GetType().Name));
        }

        /// <summary>
        /// Throws an <see cref="ArgumentException"/> if the input object has wrong type.
        /// </summary>
        /// <remarks>
        /// Method doesn't check object on null.
        /// </remarks>
        /// <exception cref="ArgumentException"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#if NETCOREAPP
        [StackTraceHidden]
        public static void ThrowIfNotType<TType>(object value, [CallerArgumentExpression(nameof(value))] string name = DefaultParameterName)
#else
        public static void ThrowIfNotType<TType>(object value, string name)
#endif
        {
            if (!(value is TType))
                throw new ArgumentException(Resource.Exception_WrongObjectType, name);
        }

        /// <summary>
        /// Throws an <see cref="ArgumentException"/> if the input object has wrong type.
        /// <br/> Can return casted object as out parameter
        /// </summary>
        /// <remarks>
        /// Method doesn't check object on null.
        /// </remarks>
        /// <exception cref="ArgumentException"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#if NETCOREAPP
        [StackTraceHidden]
        public static void ThrowIfNotType<TType>(object value, out TType casted, [CallerArgumentExpression(nameof(value))] string name = DefaultParameterName)
#else
        public static void ThrowIfNotType<TType>(object value, out TType casted, string name)
#endif
        {
            if (!(value is TType))
            {
                casted = default;

                throw new ArgumentException(Resource.Exception_WrongObjectType, name);
            }
            else
            {
                casted = (TType)value;
            }
        }

        //=========================================================================//
        //CHECK STRING STATE

        /// <summary>
        /// Throws an <see cref="ArgumentException"/> if the input string is null or empty.
        /// </summary>
        /// <exception cref="ArgumentException"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#if NETCOREAPP
        [StackTraceHidden]
        public static void ThrowIfNullOrEmpty(string value, [CallerArgumentExpression(nameof(value))] string name = DefaultParameterName)
#else
        public static void ThrowIfNullOrEmpty(string value, string name)
#endif
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentException(name, Resource.Exception_StringIsNullOrEmpty);
        }

        /// <summary>
        /// Throws an <see cref="ArgumentException"/> if the input string is null or white space.
        /// </summary>
        /// <exception cref="ArgumentException"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#if NETCOREAPP
        [StackTraceHidden]
        public static void ThrowIfNullOrWhiteSpace(string value, [CallerArgumentExpression(nameof(value))] string name = DefaultParameterName)
#else
        public static void ThrowIfNullOrWhiteSpace(string value, string name)
#endif
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException(name, Resource.Exception_StringIsNullOrWhiteSpace);
        }

        //=========================================================================//
        //CHECK COLLECTIONS STATE

        /// <summary>
        /// Throws an <see cref="ArgumentException"/> if the input collection is null or empty.
        /// </summary>
        /// <exception cref="ArgumentException"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#if NETCOREAPP
        [StackTraceHidden]
        public static void ThrowIfNullOrEmpty<TItem>(IEnumerable<TItem> value, [CallerArgumentExpression(nameof(value))] string name = DefaultParameterName)
#else
        public static void ThrowIfNullOrEmpty<TItem>(IEnumerable<TItem> value, string name)
#endif
        {
            if (value == null || !value.Any())
                throw new ArgumentException(name, Resource.Exception_CollectionIsNullOrEmpty);
        }

        /// <summary>
        /// Throws an <see cref="ArgumentException"/> if the input collection is null or empty.
        /// </summary>
        /// <exception cref="ArgumentException"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#if NETCOREAPP
        [StackTraceHidden]
        public static void ThrowIfNullOrWhiteSpace<TItem>(IEnumerable<TItem> value, [CallerArgumentExpression(nameof(value))] string name = DefaultParameterName)
#else
        public static void ThrowIfNullOrWhiteSpace<TItem>(IEnumerable<TItem> value, string name)
#endif
            where TItem : class
        {
            if (value == null || value.All(item => item == null))
                throw new ArgumentException(name, Resource.Exception_CollectionIsNullOrWhiteSpace);
        }
    }
}
