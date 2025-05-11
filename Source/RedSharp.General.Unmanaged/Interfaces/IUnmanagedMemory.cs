namespace RedSharp.General.Unmanaged.Interfaces
{
    /// <summary>
    /// Wraps the native structure. Extends the <see cref="IUnmanagedHandle"/> interface.
    /// </summary>
    public interface IUnmanagedMemory : IUnmanagedHandle
    {
        /// <summary>
        /// Shows the real size of the wrapped structure, which can be less than your expectation.
        /// </summary>
        /// <remarks>
        /// Structure size is in bytes.
        /// <br/>I understand that the ulong is not a convenient at all, but still it more likely to work with other than <see cref="nuint"/>.
        /// </remarks>
        ulong Size { get; }        
    }
}