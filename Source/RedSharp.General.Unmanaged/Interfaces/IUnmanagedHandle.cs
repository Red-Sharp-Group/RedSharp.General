using System;
using RedSharp.General.Interfaces;

namespace RedSharp.General.Unmanaged.Interfaces
{
    public interface IUnmanagedHandle : IDisposable, IIndicateObjectDisposed
    {
        /// <summary>
        /// UNSAFE to use, the direct handle/pointer to the structure.
        /// </summary>
        /// <remarks>
        /// The structure cannot guarantee that the handle will be the same for the whole object life.
        /// <br/>I do not recommend saving the value of the property somewhere else.
        /// <br/>Use it wisely.
        /// </remarks>
        IntPtr UnsafeHandle { get; }

        /// <summary>
        /// Shows that by <see cref="IDisposable.Dispose"/> the wrapped structure will be deleted too.
        /// </summary>
        bool IsHandleOwner { get; }
    }
}
