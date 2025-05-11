using System;

namespace RedSharp.General.Models
{
    /// <summary>
    /// Information about disposing process
    /// </summary>
    public class DisposeEventArgs : EventArgs
    {
        public static readonly DisposeEventArgs Manually = new DisposeEventArgs(true);

        public static readonly DisposeEventArgs Finalized = new DisposeEventArgs(false);

        public DisposeEventArgs(bool isManual)
        {
            IsManual = isManual;
        }

        /// <summary>
        /// True if <see cref="IDisposable.Dispose"/> was invoked manually, not by a finalizer
        /// </summary>
        public bool IsManual { get; }
    }
}
