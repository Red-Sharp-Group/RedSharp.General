using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using RedSharp.General.Helpers;

namespace RedSharp.General.Collections
{
    public class StreamEnumerableWrapper : Stream
    {
        private IEnumerable<byte> _enumerable;
        private IEnumerator<byte> _enumerator;
        private long _position;

        public StreamEnumerableWrapper(IEnumerable<byte> enumerable)
        {
            ArgumentsGuard.ThrowIfNull(enumerable, nameof(enumerable));

            _enumerable = enumerable;
        }

        public override bool CanRead => true;

        public override bool CanSeek => false;

        public override bool CanWrite => false;

        public override long Length => _enumerable.Count();

        public override long Position
        {
            get => _position;
            set => Seek(value, SeekOrigin.Begin);
        }

        public override void Flush()
        { /* do nothing */ }

        public override int Read(byte[] buffer, int offset, int count)
        {
            ArgumentsGuard.ThrowIfNull(buffer, nameof(buffer));
            NumericGuard.ThrowIfLessZero(offset, nameof(offset));

            var length = count - offset;

            if (buffer.Length > count)
                throw new ArgumentOutOfRangeException(nameof(count));

            if (_enumerator == null)
                _enumerator = _enumerable.GetEnumerator();

            for (int i = offset; i < length; i++)
            {
                if (_enumerator.MoveNext())
                {
                    buffer[i] = _enumerator.Current;

                    _position++;
                }
                else
                {
                    return i - offset + 1;
                }
            }

            return length;
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotSupportedException();
            /*
            if (offset == 0 && origin == SeekOrigin.Current)
                return _position;

            if (offset == 0 && origin == SeekOrigin.Begin)
            {
                _enumerator = null;

                _position = 0;
            }

            //TODO

            return _position;
            */
        }

        public override void SetLength(long value)
        {
            throw new NotSupportedException();
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            throw new NotSupportedException();
        }
    }
}
