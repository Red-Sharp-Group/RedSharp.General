using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using RedSharp.General.Collections.Properties;
using RedSharp.General.Helpers;

namespace RedSharp.General.Collections
{
    /// <summary>
    /// Shrinkable collection based on weak references.
    /// </summary>
    public class WeakRefCollection<TItem> : ICollection<TItem> where TItem : class
    {
        private class WeakShrinkableCollectionEnumerator : IEnumerator<TItem>
        {
            private WeakRefCollection<TItem> _collection;
            private int _index;

            public WeakShrinkableCollectionEnumerator(WeakRefCollection<TItem> collection)
            {
                _collection = collection;

                Reset();
            }

            /// <inheritdoc/>
            public TItem Current { get; private set; }

            /// <inheritdoc/>
            object IEnumerator.Current => Current;

            /// <inheritdoc/>
            public bool MoveNext()
            {
                _index++;

                for (; _index < _collection._buffer.Length; _index++)
                {
                    if (_collection._isAlive[_index])
                    {
                        if (_collection._buffer[_index].TryGetTarget(out TItem temp))
                        {
                            Current = temp;

                            return true;
                        }
                        else
                        {
                            _collection._isAlive[_index] = false;
                        }
                    }
                }

                return false;
            }

            /// <inheritdoc/>
            public void Reset()
            {
                _collection.TryDecreaseBuffer();

                Current = null;
                _index = -1;
            }

            public void Dispose()
            {
                Reset();
                _collection = null;
            }
        }

        private const byte DecreaseTryingMax = 8;

        private WeakReference<TItem>[] _buffer;
        private bool[] _isAlive;
        private int _count;
        private int _firstEmptyPosition;
        private byte _decreaseTrying;

        public WeakRefCollection()
        {
            Clear();
        }

        /// <summary>
        /// <inheritdoc/>
        /// May not correspond to the number of lived elements.
        /// </summary>
        public int Count => _count;

        /// <inheritdoc/>
        public bool IsReadOnly => false;

        /// <inheritdoc/>
        public void Add(TItem item)
        {
            ArgumentsGuard.ThrowIfNull(item, nameof(item));

            if (_firstEmptyPosition == _buffer.Length)
                IncreaseBuffer();

            if (_buffer[_firstEmptyPosition] == null)
                _buffer[_firstEmptyPosition] = new WeakReference<TItem>(item);
            else
                _buffer[_firstEmptyPosition].SetTarget(item);

            _isAlive[_firstEmptyPosition] = true;
            _count++;

            FindFirstEmptyPosition();
        }

        /// <inheritdoc/>
        public void Clear()
        {
            _buffer = new WeakReference<TItem>[1];
            _isAlive = new bool[1];

            _count = 0;
            _firstEmptyPosition = 0;
            _decreaseTrying = 0;
        }

        /// <inheritdoc/>
        public bool Contains(TItem item)
        {
            ArgumentsGuard.ThrowIfNull(item, nameof(item));

            bool result = false;

            int newEmptyPosition = -1;
            TItem tempItem;

            for (int i = 0; i < _buffer.Length; i++)
            {
                if (!_isAlive[i])
                    continue;

                if (!_buffer[i].TryGetTarget(out tempItem))
                {
                    _isAlive[i] = false;

                    if (newEmptyPosition == -1)
                        newEmptyPosition = i;

                    _count--;
                }
                else if (tempItem == item)
                {
                    result = true;

                    break;
                }
            }

            if (newEmptyPosition != -1 && newEmptyPosition < _firstEmptyPosition)
                _firstEmptyPosition = newEmptyPosition;

            return result;
        }

        /// <inheritdoc/>
        public bool Remove(TItem item)
        {
            ArgumentsGuard.ThrowIfNull(item, nameof(item));

            bool result = false;

            int newEmptyPosition = -1;
            TItem tempItem;

            for (int i = 0; i < _buffer.Length; i++)
            {
                if (!_isAlive[i])
                    continue;

                if (!_buffer[i].TryGetTarget(out tempItem))
                {
                    _isAlive[i] = false;

                    if (newEmptyPosition == -1)
                        newEmptyPosition = i;

                    _count--;
                }
                else if (tempItem == item)
                {
                    _isAlive[i] = false;

                    if (newEmptyPosition == -1)
                        newEmptyPosition = i;

                    _count--;

                    result = true;

                    break;
                }
            }

            if (newEmptyPosition != -1 && newEmptyPosition < _firstEmptyPosition)
                _firstEmptyPosition = newEmptyPosition;

            if (result && NeedToDecreaseBuffer())
                TryDecreaseBuffer();

            return result;
        }

        /// <inheritdoc/>
        public void CopyTo(TItem[] array, int arrayIndex)
        {
            ArgumentsGuard.ThrowIfNull(array, nameof(array));
            NumericGuard.ThrowIfLessZero(arrayIndex, nameof(arrayIndex));
            NumericGuard.ThrowIfGreaterOrEqual(arrayIndex, array.Length, nameof(arrayIndex));

            var tempCollection = new List<TItem>();

            for (int i = 0; i < _buffer.Length; i++)
            {
                if (_isAlive[i])
                {
                    if (_buffer[i].TryGetTarget(out TItem tempItem))
                        tempCollection.Add(tempItem);
                    else
                        _isAlive[i] = false;
                }
            }

            TryDecreaseBuffer();

            if (tempCollection.Count > array.Length - arrayIndex)
                throw new ArgumentException(Resource.Exception_ToSmallToCopyCollection, nameof(array));

            for (int i = 0; i < tempCollection.Count; i++)
                array[arrayIndex++] = tempCollection[i];
        }

        /// <inheritdoc/>
        public IEnumerator<TItem> GetEnumerator()
        {
            return new WeakShrinkableCollectionEnumerator(this);
        }

        /// <inheritdoc/>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void FindFirstEmptyPosition()
        {
            while (_firstEmptyPosition < _buffer.Length && _isAlive[_firstEmptyPosition])
                _firstEmptyPosition++;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void IncreaseBuffer()
        {
            Array.Resize(ref _buffer, _buffer.Length * 2);
            Array.Resize(ref _isAlive, _buffer.Length);
        }

        /// Returns true if buffer can be half smaller.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private bool NeedToDecreaseBuffer()
        {
            return _buffer.Length > 1 && _count <= _buffer.Length / 2;
        }

        /// <summary>
        /// Tries to decrease buffer, will not decrease the buffer for the first several times, 
        /// just to avoid the situation when we have to increase the buffer right after we decreased it.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void TryDecreaseBuffer()
        {
            if (_decreaseTrying < DecreaseTryingMax)
            {
                _decreaseTrying++;
            }
            else
            {
                _decreaseTrying = 0;

                var newBuffer = new WeakReference<TItem>[_buffer.Length / 2];
                var newIsAlive = new bool[newBuffer.Length];

                for (int x = 0, y = 0; x < _buffer.Length; x++)
                {
                    if (_isAlive[x])
                    {
                        newBuffer[y] = _buffer[x];
                        newIsAlive[y] = true;

                        y++;
                    }
                }

                _firstEmptyPosition = _count;
                _buffer = newBuffer;
                _isAlive = newIsAlive;
            }
        }
    }
}
