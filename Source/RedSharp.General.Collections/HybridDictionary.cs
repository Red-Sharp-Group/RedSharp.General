using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using RedSharp.General.Helpers;

namespace RedSharp.General.Collections
{
    public class HybridDictionary<TKey, TValue> : IDictionary<TKey, TValue>
    {
        private class KeyValueEnumerator : IEnumerator<KeyValuePair<TKey, TValue>>
        {
            private List<TKey> _keys;
            private List<TValue> _values;
            private int _index;

            public KeyValueEnumerator(List<TKey> keys, List<TValue> values)
            {
                _keys = keys;
                _values = values;

                Reset();
            }

            public KeyValuePair<TKey, TValue> Current
            {
                get => new KeyValuePair<TKey, TValue>(_keys[_index], _values[_index]);
            }

            object IEnumerator.Current => Current;

            public bool MoveNext()
            {
                _index++;

                return _index < _keys.Count;
            }

            public void Reset()
            {
                _index = -1;
            }

            public void Dispose() => Reset();
        }


        public const int DefaultListLimit = 13;

        private int _listLimit;

        private IEqualityComparer<TKey> _comparer;

        private List<TKey> _keys;
        private List<TValue> _values;

        private Dictionary<TKey, TValue> _dictionary;

        public HybridDictionary(IEqualityComparer<TKey> comparer = null, int listLimit = DefaultListLimit)
        {
            NumericGuard.ThrowIfLessOrEqualZero(listLimit, nameof(listLimit));

            _listLimit = listLimit;
            _comparer = comparer;

            _keys = new List<TKey>(_listLimit);
            _values = new List<TValue>(_listLimit);
        }

        public TValue this[TKey key] 
        { 
            get
            {
                if (_dictionary != null)
                    return _dictionary[key];
                else
                    return _values[GetIndexOrThrow(key)];
            }
            set
            {
                if (_dictionary != null)
                {
                    _dictionary[key] = value;
                }
                else
                {
                    var index = IndexOf(key);

                    if (index == -1)
                        Add(key, value);
                    else
                        _values[index] = value;
                }
            }
        }

        public ICollection<TKey> Keys
        {
            get
            {
                if(_dictionary != null)
                    return _dictionary.Keys;
                else
                    return _keys;
            }
        }

        public ICollection<TValue> Values
        {
            get
            {
                if (_dictionary != null)
                    return _dictionary.Values;
                else
                    return _values;
            }
        }

        public int Count
        {
            get
            {
                if (_dictionary != null)
                    return _dictionary.Count;
                else
                    return _values.Count;
            }
        }

        public bool IsReadOnly => false;

        public void Add(TKey key, TValue value)
        {
            if (_keys.Count == _listLimit)
            {
                _dictionary = new Dictionary<TKey, TValue>(_comparer);

                for (int i = 0; i < _keys.Count; i++)
                    _dictionary.Add(_keys[i], _values[i]);

                _keys.Clear();
                _values.Clear();
            }

            if (_dictionary != null)
            {
                _dictionary.Add(key, value);
            }
            else
            {
                ArgumentsGuard.ThrowIfNull(key, nameof(key));

                if (IndexOf(key) != -1)
                    throw new ArgumentException("The key is already presented.");

                _keys.Add(key);
                _values.Add(value);
            }
        }

        public void Add(KeyValuePair<TKey, TValue> item) => Add(item.Key, item.Value);


        public bool ContainsKey(TKey key)
        {
            if (_dictionary != null)
                return _dictionary.ContainsKey(key);
            else
                return IndexOf(key) != -1;
        }

        public bool Contains(KeyValuePair<TKey, TValue> item) => ContainsKey(item.Key);

        public bool TryGetKey(TKey key, out TKey value)
        {
            value = default;

            if (_dictionary != null)
            {
                if (!_dictionary.ContainsKey(key))
                    return false;

                if (_comparer == null)
                {
                    value = key;
                }
                else
                {
                    foreach (var item in _dictionary.Keys)
                    {
                        if (_comparer.Equals(key, item))
                        {
                            value = item;

                            break;
                        }
                    }
                }
            }
            else
            {
                var index = IndexOf(key);

                if (index == -1)
                    return false;

                value = _keys[index];
            }

            return true;
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            if (_dictionary != null)
            {
                return _dictionary.TryGetValue(key,out value);
            }
            else
            {
                value = default;

                var index = IndexOf(key);

                if (index == -1)
                    return false;

                value = _values[index];

                return true;
            }
        }


        public bool Remove(TKey key)
        {
            var result = false;

            if (_dictionary != null)
            {
                result = _dictionary.Remove(key);
            }
            else
            {
                var index = IndexOf(key);

                if (index != -1)
                {
                    _keys.RemoveAt(index);
                    _values.RemoveAt(index);

                    result = true;
                }
            }

            if (result && _dictionary != null && _dictionary.Count == _listLimit)
            {
                foreach (var item in _dictionary)
                {
                    _keys.Add(item.Key);
                    _values.Add(item.Value);
                }

                _dictionary = null;
            }

            return result;
        }

        public bool Remove(KeyValuePair<TKey, TValue> item) => Remove(item.Key);


        public void Clear()
        {
            _keys.Clear();
            _values.Clear();

            _dictionary = null;
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int index)
        {
            if (_dictionary != null)
            {
                ((IDictionary<TKey, TValue>)_dictionary).CopyTo(array, index);
            }
            else
            {
                ArgumentsGuard.ThrowIfNull(array, nameof(array));
                NumericGuard.ThrowIfLessZero(index, nameof(index));
                NumericGuard.ThrowIfGreaterOrEqual(index, array.Length, nameof(index));
                NumericGuard.ThrowIfLess(array.Length - index, _keys.Count, "array_length");

                var amount = array.Length - index;

                for (int i = 0; i < amount; i++)
                    array[i + index] = new KeyValuePair<TKey, TValue>(_keys[i], _values[i]);
            }
        }


        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            if (_dictionary != null)
                return _dictionary.GetEnumerator();
            else
                return new KeyValueEnumerator(_keys, _values);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            if (_dictionary != null)
                return _dictionary.GetEnumerator();
            else
                return new KeyValueEnumerator(_keys, _values);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private int IndexOf(TKey key)
        {
            if (_comparer != null)
            {
                for (int i = 0; i < _keys.Count; i++)
                    if (_comparer.Equals(key, _keys[i]))
                        return i;
            }
            else
            {
                for (int i = 0; i < _keys.Count; i++)
                    if (_keys[i].Equals(key))
                        return i;
            }

            return -1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private int GetIndexOrThrow(TKey key)
        {
            var result = IndexOf(key);

            if (result == -1)
                throw new KeyNotFoundException();

            return result;
        }
    }
}
