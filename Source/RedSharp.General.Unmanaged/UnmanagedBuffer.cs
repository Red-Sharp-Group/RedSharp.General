using System;
using System.Collections;
using System.Collections.Generic;
using RedSharp.General.Helpers;
using RedSharp.General.Unmanaged.Abstracts;
using RedSharp.General.Unmanaged.Interfaces;

namespace RedSharp.General.Unmanaged
{
    public unsafe class UnmanagedBuffer<TItem> : UnmanagedMemoryWrapperBase, IEnumerable<TItem> where TItem : unmanaged
    {
        /// <summary>
        /// Just to have an ability to LINQ this thing.
        /// </summary>
        private class UnmanagedBufferEnumerator : IEnumerator<TItem>
        {
            private UnmanagedBuffer<TItem> _buffer;
            private int _index;

            public UnmanagedBufferEnumerator(UnmanagedBuffer<TItem> owner)
            {
                _buffer = owner;

                Reset();
            }

            public TItem Current => _buffer[_index];

            object IEnumerator.Current => Current;

            public bool MoveNext()
            {
                _index++;

                return _index < _buffer.Length;
            }

            public void Reset()
            {
                _index = -1;
            }

            public void Dispose()
            {
                Reset();
                _buffer = null;
            }
        }

        /// <summary>
        /// Initialize an empty buffer with desired length.
        /// </summary>
        /// <remarks>
        /// This is stupid remark but the LENGTH is not a SIZE.
        /// </remarks>
        /// <exception cref="ArgumentException">If length is less then zero.</exception>
        public UnmanagedBuffer(int length)
        {
            NumericGuard.ThrowIfLessZero(length, nameof(length));

            Initialize((ulong)(length * sizeof(TItem)));

            Length = length;
        }

        /// <summary>
        /// Initialize a buffer from the input pointer with desired length.
        /// </summary>
        /// <remarks>
        /// This is stupid remark but the LENGTH is not a SIZE.
        /// <br/> This is a pretty UNSAFE constructor. Think twice before using it.
        /// </remarks>
        /// <exception cref="ArgumentNullException">If pointer is null.</exception>
        /// <exception cref="ArgumentException">If length is less then zero.</exception>
        public UnmanagedBuffer(IntPtr pointer, int length, bool makeCopy = true)
        {
            NumericGuard.ThrowIfLessZero(length, nameof(length));

            Initialize(pointer, (ulong)(length * sizeof(TItem)), makeCopy);

            Length = length;
        }

        /// <summary>
        /// Creates a new buffer based on the input native structure with zero offset and input length.
        /// </summary>
        /// <remarks>
        /// This is stupid remark but the LENGTH is not a SIZE.
        /// </remarks>
        /// <exception cref="ArgumentException">If length is less then zero.</exception>
        /// <exception cref="ArgumentNullException">If the input structure is null.</exception>
        /// <exception cref="ObjectDisposedException">If the input structure is disposed.</exception>
        /// <exception cref="ArgumentOutOfRangeException">If a new buffer is bigger than the basic structure.</exception>
        public UnmanagedBuffer(IUnmanagedMemory basic, int length, bool makeCopy = true) : this(basic, 0, length, makeCopy)
        { }

        /// <summary>
        /// Creates a new buffer based on the input native structure with desired offset and input length.
        /// </summary>
        /// <remarks>
        /// This is stupid remark but the LENGTH is not a SIZE, but offset in bytes.
        /// </remarks>
        /// <exception cref="ArgumentException">If length is less then zero.</exception>
        /// <exception cref="ArgumentNullException">If the input structure is null.</exception>
        /// <exception cref="ObjectDisposedException">If the input structure is disposed.</exception>
        /// <exception cref="ArgumentOutOfRangeException">If a new buffer is bigger than the basic structure.</exception>
        public UnmanagedBuffer(IUnmanagedMemory basic, ulong offset, int length, bool makeCopy = true)
        {
            NumericGuard.ThrowIfLessZero(length, nameof(length));

            Initialize(basic, offset, (ulong)(length * sizeof(TItem)), makeCopy);

            Length = length;
        }

        /// <summary>
        /// Gets structure by index.
        /// </summary>
        /// <remarks>
        /// Still have no idea how it works, but it works.
        /// </remarks>
        /// <exception cref="ObjectDisposedException"/>
        /// <exception cref="ArgumentOutOfRangeException">If you tries to get something outside of structure.</exception>
        public ref TItem this[int index]
        {
            get
            {
                ThrowIfDisposed();

                if (index < 0 || index >= Length)
                    throw new ArgumentOutOfRangeException(nameof(index));

                return ref *(((TItem*)UnsafeHandle.ToPointer()) + index);
            }
        }

        /// <summary>
        /// Returns number of elements stored in buffer.
        /// </summary>
        /// <remarks>
        /// Do not confuse with size.
        /// </remarks>
        public int Length { get; private set; }

        /// <inheritdoc/>
        /// <exception cref="ObjectDisposedException"/>
        public IEnumerator<TItem> GetEnumerator()
        {
            ThrowIfDisposed();

            return new UnmanagedBufferEnumerator(this);
        }

        /// <inheritdoc/>
        /// <exception cref="ObjectDisposedException"/>
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        protected override void InternalDispose(bool manual)
        {
            Length = 0;

            base.InternalDispose(manual);
        }
    }
}
