using System;
using RedSharp.General.Models;

namespace RedSharp.General.Interfaces
{
    /// <summary>
    /// Special interface for the cases when you need to know when object started disposing.
    /// </summary>
    public interface INotifyObjectDisposing
    {
        /// <summary>
        /// Invokes before disposing process
        /// </summary>
        event EventHandler<DisposeEventArgs> Disposing;
    }
}
