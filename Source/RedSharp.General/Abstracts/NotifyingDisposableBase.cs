using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using RedSharp.General.Interfaces;
using RedSharp.General.Models;

namespace RedSharp.General.Abstracts
{
    public abstract class NotifyingDisposableBase : DisposableBase, INotifyObjectDisposing, INotifyObjectDisposed
    {
        /// <inheritdoc/>
        public event EventHandler<DisposeEventArgs> Disposing;

        /// <inheritdoc/>
        public event EventHandler<DisposeEventArgs> Disposed;

        /// <inheritdoc/>
        protected internal override void SafeDispose(bool manual)
        {
            try
            {
                Disposing.Invoke(this, manual ? DisposeEventArgs.Manually : DisposeEventArgs.Finalized);
            }
            catch (Exception exception)
            {
                Trace.WriteLine(exception.Message);
                Trace.WriteLine(exception.StackTrace);
            }

            base.SafeDispose(manual);

            try
            {
                Disposed.Invoke(this, manual ? DisposeEventArgs.Manually : DisposeEventArgs.Finalized);
            }
            catch (Exception exception)
            {
                Trace.WriteLine(exception.Message);
                Trace.WriteLine(exception.StackTrace);
            }
        }
    }
}
