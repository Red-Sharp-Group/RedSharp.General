using System;
using RedSharp.General.Models;

namespace RedSharp.General.Interfaces
{
    /// <summary>
    /// Special interface for the cases when you need to know when object was disposed.
    /// </summary>
    public interface INotifyObjectDisposed
    {
        /// <summary>
        /// Invokes after disposing process
        /// </summary>
        event EventHandler<DisposeEventArgs> Disposed;
    }
}
