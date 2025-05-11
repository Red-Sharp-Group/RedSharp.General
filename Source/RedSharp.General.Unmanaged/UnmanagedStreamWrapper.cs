using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using RedSharp.General.Helpers;
using RedSharp.General.Interfaces;
using RedSharp.General.Unmanaged.Interfaces;
using RedSharp.General.Unmanaged.Properties;

namespace RedSharp.General.Unmanaged
{
    public class UnmanagedStreamWrapper : Stream, IIndicateObjectDisposed
    {
        private bool _leaveOpen;
        private IUnmanagedMemory _object;
        private long _currentPosition;

        public UnmanagedStreamWrapper(IUnmanagedMemory @object, bool leaveOpen = true)
        {
            ArgumentsGuard.ThrowIfDisposed(@object, nameof(@object));

            _object = @object;

            if (_object.Size > long.MaxValue)
                throw new ArgumentOutOfRangeException(nameof(_object.Size), Resource.Exception_SizeIsTooBigForStream);

            _leaveOpen = leaveOpen;
        }

        public override bool CanRead => true;

        public override bool CanSeek => false;

        public override bool CanWrite => true;

        public override long Length => (long)_object.Size;

        public override long Position 
        { 
            get => _currentPosition;
            set
            {
                NumericGuard.ThrowIfLessZero(value, nameof(value));
                NumericGuard.ThrowIfGreaterOrEqual(value, Length, nameof(value));

                _currentPosition = value;
            } 
        }

        public bool IsDisposed { get; private set; }

        public override int Read(byte[] buffer, int offset, int count)
        {
            ThrowIfDisposed();
            ArgumentsGuard.ThrowIfDisposed(_object, nameof(_object));
            NumericGuard.ThrowIfLessZero(offset, nameof(offset));
            NumericGuard.ThrowIfLessZero(count, nameof(count));

            if (count == 0)
                return 0;

            if (offset + count > buffer.Length)
                throw new ArgumentOutOfRangeException(nameof(count));

            var result = (int)Math.Min((long)_object.Size - _currentPosition, count);

            if (result == 0)
                return 0;

            var target = new Span<byte>(buffer, offset, result);
            var source = _object.ToSpanOfBytes((ulong)_currentPosition, result);

            source.CopyTo(target);

            _currentPosition += result;

            return result;
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            ThrowIfDisposed();
            ArgumentsGuard.ThrowIfDisposed(_object, nameof(_object));
            NumericGuard.ThrowIfLessZero(offset, nameof(offset));
            NumericGuard.ThrowIfLessZero(count, nameof(count));

            if (count == 0)
                return;

            if (offset + count > buffer.Length)
                throw new ArgumentOutOfRangeException(nameof(count));

            var result = (int)Math.Min((long)_object.Size - _currentPosition, count);

            if (result == 0)
                return;

            var source = new Span<byte>(buffer, offset, result);
            var target = _object.ToSpanOfBytes((ulong)_currentPosition, result);

            source.CopyTo(target);

            _currentPosition += result;
        }

        public override void Flush()
        { 
            ThrowIfDisposed();
            ArgumentsGuard.ThrowIfDisposed(_object, nameof(_object));
        }

        public override long Seek(long offset, SeekOrigin origin) => throw new NotSupportedException();

        public override void SetLength(long value) => throw new NotSupportedException();

        /// <summary>
        /// Helper method that throws <see cref="ObjectDisposedException"/> if this object is disposed.
        /// </summary>
        /// <exception cref="ObjectDisposedException"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#if NETCOREAPP
        [StackTraceHidden]
#endif
        private void ThrowIfDisposed()
        {
            if (IsDisposed)
                throw new ObjectDisposedException(ToString(), Resource.Exception_ThisObjectIsDisposed);
        }

        protected override void Dispose(bool disposing)
        {
            IsDisposed = true;

            base.Dispose(disposing);

            if (!_leaveOpen)
                _object.Dispose();
        }

    }
}
