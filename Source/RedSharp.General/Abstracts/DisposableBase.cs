using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using RedSharp.General.Interfaces;
using RedSharp.General.Properties;

namespace RedSharp.General.Abstracts
{
    public class DisposableBase : IDisposable, IIndicateObjectDisposed
    {
        ~DisposableBase()
        {
            SafeDispose(false);
        }

        /// <inheritdoc/>
        public virtual bool IsDisposed { get; private set; }

        /// <inheritdoc/>
        public void Dispose()
        {
            if (IsDisposed)
                return;

            SafeDispose(true);

            GC.SuppressFinalize(this);

            IsDisposed = true;
        }

        /// <summary>
        /// Special private method that guaranties that dispose will not throw an exception in finalizer.
        /// </summary>
        /// <remarks>
        /// Do not change the method signature.
        /// </remarks>
        protected internal virtual void SafeDispose(bool manual)
        {
            try
            {
                InternalDispose(manual);
            }
            catch (Exception exception)
            {
                Trace.WriteLine(exception.Message);
                Trace.WriteLine(exception.StackTrace);
            }
        }

        /// <summary>
        /// Override this method to define disposing logic.
        /// </summary>
        protected virtual void InternalDispose(bool manual)
        { }

        /// <summary>
        /// Helper method that throws <see cref="ObjectDisposedException"/> if this object is disposed.
        /// </summary>
        /// <exception cref="ObjectDisposedException"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#if NETCOREAPP
        [StackTraceHidden]
#endif
        protected virtual void ThrowIfDisposed()
        {
            if (IsDisposed)
                throw new ObjectDisposedException(ToString(), Resource.Exception_ThisObjectIsDisposed);
        }
    }
}
