using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Runtime.CompilerServices;
using RedSharp.General.Collections.Interfaces;

namespace RedSharp.General.Collections
{
    public class Table<TColumn, TRow, TItem> : ITable<TColumn, TRow, TItem>
    {
        private class RangedDoubleArrayEnumerator : IEnumerator<TItem>
        {
            private TItem[,] _array;
            private int _width;
            private int _x;

            private int _height;
            private int _y;

            public RangedDoubleArrayEnumerator(TItem[,] array, int width, int height)
            {
                _array = array;
                _width = width;
                _height = height;

                Reset();
            }

            public TItem Current => _array[_x, _y];

            object IEnumerator.Current => Current;

            public bool MoveNext()
            {
                _y++;

                if (_y == _height)
                {
                    _x++;
                    _y = 0;
                }

                return _x < _width;
            }

            public void Reset()
            {
                _x = 0;
                _y = -1;
            }

            public void Dispose() => Reset();
        }

        private class RangedArrayEnumerator<TTemp> : IEnumerator<TTemp>
        {
            private TTemp[] _array;
            private int _count;
            private int _index;

            public RangedArrayEnumerator(TTemp[] array, int count)
            {
                _array = array;
                _count = count;

                Reset();
            }

            public TTemp Current => _array[_index];

            object IEnumerator.Current => Current;

            public bool MoveNext()
            {
                _index++;

                return _index < _count;
            }

            public void Reset() => _index = -1;

            public void Dispose() => Reset();
        }

        public class ColumnsRecordCollections : ITableHeader<TColumn, TRow, TItem>
        {
            private Table<TColumn, TRow, TItem> _owner;

            public ColumnsRecordCollections(Table<TColumn, TRow, TItem> owner)
            {
                _owner = owner;
            }

            public ITableRecord<TColumn, TRow, TItem> this[TColumn name]
            {
                get
                {
                    if (!Headers.TryGetValue(name, out int index))
                        throw new KeyNotFoundException();

                    return Records[index];
                }
            }

            public ITableRecord<TColumn, TRow, TItem> this[int index]
            {
                get => Records[index];
            }

            public int Count => Headers.Count;

            public bool IsReadOnly => false;


            public void Add(TColumn item) => _owner.InsertColumn(item, Headers.Count);

            public void Insert(int index, TColumn item) => _owner.InsertColumn(item, index);


            public bool Contains(TColumn item) => Headers.ContainsKey(item);

            public bool TryGetRecord(TColumn item, out ITableRecord<TColumn, TRow, TItem> record)
            {
                if (Headers.TryGetValue(item, out int index))
                {
                    record = Records[index];

                    return true;
                }
                else
                {
                    record= null;

                    return false;
                }

            }

            public int IndexOf(TColumn item)
            {
                if (!Headers.TryGetValue(item, out int result))
                    result = -1;

                return result;
            }


            public bool Remove(TColumn item) => _owner.RemoveColumn(item);

            public void RemoveAt(int index) => _owner.RemoveColumnAt(index);


            public void Clear()
            {
                Headers.Clear();

                Array.Clear(Records, 0, Records.Length);
                Array.Clear(Data, 0, Data.Length);
            }


            public IEnumerator<ITableRecord<TColumn, TRow, TItem>> GetEnumerator() => new RangedArrayEnumerator<ITableRecord<TColumn, TRow, TItem>>(Records, Count);

            IEnumerator IEnumerable.GetEnumerator() => new RangedArrayEnumerator<ITableRecord<TColumn, TRow, TItem>>(Records, Count);


            private IDictionary<TColumn, int> Headers
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => _owner._columnsMap;
            }

            private ITableRecord<TColumn, TRow, TItem>[] Records
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => _owner._columnRecords;
            }

            private TItem[,] Data
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => _owner._data;
            }
        }

        public class RowsRecordCollections : ITableHeader<TRow, TColumn, TItem>
        {
            private Table<TColumn, TRow, TItem> _owner;

            public RowsRecordCollections(Table<TColumn, TRow, TItem> owner)
            {
                _owner = owner;
            }

            public ITableRecord<TRow, TColumn, TItem> this[TRow name]
            {
                get
                {
                    if (!Headers.TryGetValue(name, out int index))
                        throw new KeyNotFoundException();

                    return Records[index];
                }
            }

            public ITableRecord<TRow, TColumn, TItem> this[int index]
            {
                get => Records[index];
            }


            public int Count => Headers.Count;

            public bool IsReadOnly => false;


            public void Add(TRow item) => _owner.InsertRow(item, Headers.Count);

            public void Insert(int index, TRow item) => _owner.InsertRow(item, index);


            public bool Contains(TRow item) => Headers.ContainsKey(item);

            public bool TryGetRecord(TRow item, out ITableRecord<TRow, TColumn, TItem> record)
            {
                if (Headers.TryGetValue(item, out int index))
                {
                    record = Records[index];

                    return true;
                }
                else
                {
                    record = null;

                    return false;
                }

            }

            public int IndexOf(TRow item)
            {
                if (!Headers.TryGetValue(item, out int result))
                    result = -1;

                return result;
            }


            public bool Remove(TRow item) => _owner.RemoveRow(item);

            public void RemoveAt(int index) => _owner.RemoveRowAt(index);


            public void Clear()
            {
                Headers.Clear();

                Array.Clear(Records, 0, Records.Length);
                Array.Clear(Data, 0, Data.Length);
            }


            public IEnumerator<ITableRecord<TRow, TColumn, TItem>> GetEnumerator() => new RangedArrayEnumerator<ITableRecord<TRow, TColumn, TItem>>(Records, Count);

            IEnumerator IEnumerable.GetEnumerator() => new RangedArrayEnumerator<ITableRecord<TRow, TColumn, TItem>>(Records, Count);


            private IDictionary<TRow, int> Headers
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => _owner._rowsMap;
            }

            private ITableRecord<TRow, TColumn, TItem>[] Records
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => _owner._rowRecords;
            }

            private TItem[,] Data
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => _owner._data;
            }
        }

        public class ColumnRecord : ITableRecord<TColumn, TRow, TItem>
        {
            private class ArrayEnumerator : IEnumerator<TItem>
            {
                private TItem[,] _array;
                private int _count;
                private int _target;
                private int _index;

                public ArrayEnumerator(TItem[,] array, int count, int target)
                {
                    _array = array;
                    _count = count;
                    _target = target;

                    Reset();
                }

                public TItem Current => _array[_target, _index];

                object IEnumerator.Current => Current;

                public bool MoveNext()
                {
                    _index++;

                    return _index < _count;
                }

                public void Reset() => _index = -1;

                public void Dispose() => Reset();
            }

            private Table<TColumn, TRow, TItem> _owner;

            private TColumn _name;

            public ColumnRecord(Table<TColumn, TRow, TItem> owner, TColumn name, int targetColumn)
            {
                _owner = owner;
                _name = name;
                TargetIndex = targetColumn;
            }

            public TItem this[TRow key]
            {
                get
                {
                    if (!Keys.TryGetValue(key, out int index))
                        throw new ArgumentOutOfRangeException("The input key is not found");

                    return Data[index, TargetIndex];
                }
                set
                {
                    if (!Keys.TryGetValue(key, out int index))
                        throw new ArgumentOutOfRangeException("The input key is not found");

                    Data[index, TargetIndex] = value;
                }
            }

            public TItem this[int index]
            {
                get => Data[TargetIndex, index];
                set => Data[TargetIndex, index] = value;
            }

            TItem IReadOnlyList<TItem>.this[int index] => this[index];

            public TColumn Name => _name;

            public int TargetIndex { get; internal set; }


            public bool Contains(TRow key)
            {
                return Keys.ContainsKey(key);
            }

            public bool TryGetValue(TRow key, out TItem value)
            {
                value = default;

                if (!Keys.TryGetValue(key, out int index))
                    return false;

                value = Data[TargetIndex, index];

                return true;
            }


            public int IndexOf(TRow key)
            {
                if (!Keys.TryGetValue(key, out int index))
                    index = -1;

                return index;
            }
            public int Count => _owner._rowsMap.Count;

            public IEnumerator<TItem> GetEnumerator() => new ArrayEnumerator(Data, Count, TargetIndex);

            IEnumerator IEnumerable.GetEnumerator() => new ArrayEnumerator(Data, Count, TargetIndex);

            private IDictionary<TRow, int> Keys
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => _owner._rowsMap;
            }

            private TItem[,] Data
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => _owner._data;
            }
        }

        public class RowRecord : ITableRecord<TRow, TColumn, TItem>
        {
            private class ArrayEnumerator : IEnumerator<TItem>
            {
                private TItem[,] _array;
                private int _count;
                private int _target;
                private int _index;

                public ArrayEnumerator(TItem[,] array, int count, int target)
                {
                    _array = array;
                    _count = count;
                    _target = target;

                    Reset();
                }

                public TItem Current => _array[_index, _target];

                object IEnumerator.Current => Current;

                public bool MoveNext()
                {
                    _index++;

                    return _index < _count;
                }

                public void Reset() => _index = -1;

                public void Dispose() => Reset();
            }

            private Table<TColumn, TRow, TItem> _owner;

            private TRow _name;

            public RowRecord(Table<TColumn, TRow, TItem> owner, TRow name, int targetRow)
            {
                _owner = owner;
                _name = name;
                TargetIndex = targetRow;
            }

            public TItem this[TColumn key] 
            { 
                get
                {
                    if (!Keys.TryGetValue(key, out int index))
                        throw new ArgumentOutOfRangeException("The input key is not found");

                    return Data[index, TargetIndex];
                }
                set
                {
                    if (!Keys.TryGetValue(key, out int index))
                        throw new ArgumentOutOfRangeException("The input key is not found");

                    Data[index, TargetIndex] = value;
                }
            }

            public TItem this[int index] 
            { 
                get => Data[index, TargetIndex];
                set => Data[index, TargetIndex] = value;
            }

            TItem IReadOnlyList<TItem>.this[int index] => this[index];

            public TRow Name => _name;

            public int Count => _owner._columnsMap.Count;

            public int TargetIndex { get; internal set; }


            public bool Contains(TColumn key)
            {
                return Keys.ContainsKey(key);
            }

            public bool TryGetValue(TColumn key, out TItem value)
            {
                value = default;

                if (!Keys.TryGetValue(key, out int index))
                    return false;

                value = Data[index, TargetIndex];

                return true;
            }


            public int IndexOf(TColumn key)
            {
                if (!Keys.TryGetValue(key, out int index))
                    index = -1;

                return index;
            }


            public IEnumerator<TItem> GetEnumerator() => new ArrayEnumerator(Data, Count, TargetIndex);

            IEnumerator IEnumerable.GetEnumerator() => new ArrayEnumerator(Data, Count, TargetIndex);

            private IDictionary<TColumn, int> Keys
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => _owner._columnsMap;
            }

            private TItem[,] Data
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => _owner._data;
            }
        }

        private static readonly TItem[,] _emptyData = new TItem[0, 0];

        private int _columnsCapasity;
        private int _rowsCapasity;

        private HybridDictionary<TColumn, int> _columnsMap;
        private HybridDictionary<TRow, int> _rowsMap;

        private ColumnRecord[] _columnRecords;
        private RowRecord[] _rowRecords;

        private TItem[,] _data;

        public Table(IEqualityComparer<TColumn> columnComparer = null, IEqualityComparer<TRow> rowComparer = null)
        {
            _columnsMap = new HybridDictionary<TColumn, int>(columnComparer);
            _rowsMap = new HybridDictionary<TRow, int>(rowComparer);

            _columnsCapasity = 0;
            _rowsCapasity = 0;

            _columnRecords = Array.Empty<ColumnRecord>();
            _rowRecords = Array.Empty<RowRecord>();

            _data = _emptyData;

            Columns = new ColumnsRecordCollections(this);
            Rows = new RowsRecordCollections(this);
        }

        public TItem this[int x, int y]
        {
            get => _data[x, y];
            set => _data[x, y] = value;
        }

        public TItem this[TColumn column, TRow row]
        {
            get
            {
                if (!_columnsMap.TryGetValue(column, out int columnIndex))
                    throw new ArgumentOutOfRangeException("The input column is not found");

                if (!_rowsMap.TryGetValue(row, out int rowIndex))
                    throw new ArgumentOutOfRangeException("The input row is not found");

                return _data[columnIndex, rowIndex];
            }
            set
            {
                if (!_columnsMap.TryGetValue(column, out int columnIndex))
                    throw new ArgumentOutOfRangeException("The input column is not found");

                if (!_rowsMap.TryGetValue(row, out int y))
                    throw new ArgumentOutOfRangeException("The input row is not found");

                _data[columnIndex, y] = value;
            }
        }


        public ITableHeader<TColumn, TRow, TItem> Columns { get; }

        public ITableHeader<TRow, TColumn, TItem> Rows { get; }


        public int Count => _columnsMap.Count * _rowsMap.Count;


        public IEnumerator<TItem> GetEnumerator()
        {
            return new RangedDoubleArrayEnumerator(_data, _columnsMap.Count, _rowsMap.Count);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new RangedDoubleArrayEnumerator(_data, _columnsMap.Count, _rowsMap.Count);
        }


        private void InsertColumn(TColumn column, int targetColumnIndex)
        {
            if (_columnsMap.ContainsKey(column))
                throw new ArgumentException("The input value already exists");

            if (targetColumnIndex < 0 || targetColumnIndex > _columnsMap.Count)
                throw new ArgumentOutOfRangeException("The input index is out of range");

            if (_columnsMap.Count + 1 > _columnsCapasity)
                ExtendColumns();

            if (targetColumnIndex != _columnsMap.Count)
            {
                for (int columnIndex = _columnsMap.Count; columnIndex > targetColumnIndex; columnIndex--)
                    for (int rowIndex = 0; rowIndex < _rowsMap.Count; rowIndex++)
                        _data[columnIndex, rowIndex] = _data[columnIndex - 1, rowIndex];

                FillColumn(targetColumnIndex, default);

                foreach (var item in _columnsMap.ToArray())
                    if (item.Value >= targetColumnIndex)
                        _columnsMap[item.Key]++;

                for (int i = 0; i < _columnsMap.Count; i++)
                    if (_columnRecords[i].TargetIndex >= targetColumnIndex)
                        _columnRecords[i].TargetIndex++;

                for (int i = _columnsMap.Count; i > targetColumnIndex; i--)
                    _columnRecords[i] = _columnRecords[i - 1];
            }

            _columnsMap.Add(column, targetColumnIndex);

            _columnRecords[targetColumnIndex] = new ColumnRecord(this, column, targetColumnIndex);
        }

        private void InsertRow(TRow row, int targetRowIndex)
        {
            if (_rowsMap.ContainsKey(row))
                throw new ArgumentException("The input value already exists");

            if (targetRowIndex < 0 || targetRowIndex > _rowsMap.Count)
                throw new ArgumentOutOfRangeException("The input index is out of range");

            if (_rowsMap.Count + 1 > _rowsCapasity)
                ExtendRows();

            if (targetRowIndex != _rowsMap.Count)
            {
                for (int columnIndex = 0; columnIndex < _columnsMap.Count; columnIndex++)
                    for (int rowIndex = _rowsMap.Count; rowIndex > targetRowIndex; rowIndex--)
                        _data[columnIndex, rowIndex] = _data[columnIndex, rowIndex - 1];

                FillRow(targetRowIndex, default);

                foreach (var item in _rowsMap.ToArray())
                    if (item.Value >= targetRowIndex)
                        _rowsMap[item.Key] = item.Value + 1;

                for (int i = 0; i < _rowsMap.Count; i++)
                    if (_rowRecords[i].TargetIndex >= targetRowIndex)
                        _rowRecords[i].TargetIndex++;

                for (int i = _rowsMap.Count; i > targetRowIndex; i--)
                    _rowRecords[i] = _rowRecords[i - 1];
            }

            _rowsMap.Add(row, targetRowIndex);

            _rowRecords[targetRowIndex] = new RowRecord(this, row, targetRowIndex);
        }

        private void FillRow(int targetRowIndex, TItem item)
        {
            if (targetRowIndex < 0 || targetRowIndex >= _rowsMap.Count)
                throw new ArgumentOutOfRangeException("The input index is out of range");

            for (int columnIndex = 0; columnIndex < _columnsMap.Count; columnIndex++)
                _data[columnIndex, targetRowIndex] = default;
        }

        private void FillColumn(int targetColumnIndex, TItem item)
        {
            if (targetColumnIndex < 0 || targetColumnIndex >= _columnsMap.Count)
                throw new ArgumentOutOfRangeException("The input index is out of range");

            for (int rowIndex = 0; rowIndex < _rowsMap.Count; rowIndex++)
                _data[targetColumnIndex, rowIndex] = default;
        }

        private bool RemoveColumn(TColumn column)
        {
            if (!_columnsMap.TryGetValue(column, out var index))
                return false;

            RemoveColumnAt(index);

            return true;
        }

        private bool RemoveRow(TRow row)
        {
            if (!_rowsMap.TryGetValue(row, out var index))
                return false;

            RemoveRowAt(index);

            return true;
        }

        private void RemoveColumnAt(int targetColumnIndex)
        {
            var column = _columnRecords[targetColumnIndex].Name;

            _columnRecords[targetColumnIndex] = null;

            if (targetColumnIndex + 1 != _columnsMap.Count)
            {
                for (int columnIndex = targetColumnIndex; columnIndex < _columnsMap.Count - 1; columnIndex++)
                    for (int rowIndex = 0; rowIndex < _rowsMap.Count; rowIndex++)
                        _data[columnIndex, rowIndex] = _data[columnIndex + 1, rowIndex];

                foreach (var item in _columnsMap.ToArray())
                    if (item.Value > targetColumnIndex)
                        _columnsMap[item.Key]--;

                for (int i = targetColumnIndex; i < _columnsMap.Count - 1; i++)
                    _columnRecords[i] = _columnRecords[i + 1];

                for (int i = 0; i < _columnsMap.Count - 1; i++)
                    if (_columnRecords[i].TargetIndex > targetColumnIndex)
                        _columnRecords[i].TargetIndex--;
            }

            FillColumn(_columnsMap.Count - 1, default);

            _columnRecords[_columnsMap.Count - 1] = null;

            _columnsMap.Remove(column);

        }

        private void RemoveRowAt(int targetRowIndex)
        {
            var row = _rowRecords[targetRowIndex].Name;

            _rowRecords[targetRowIndex] = null;

            if (targetRowIndex + 1 != _rowsMap.Count)
            {
                for (int columnIndex = 0; columnIndex < _columnsMap.Count; columnIndex++)
                    for (int rowIndex = targetRowIndex; rowIndex < _rowsMap.Count - 1; rowIndex++)
                        _data[columnIndex, rowIndex] = _data[columnIndex, rowIndex + 1];

                foreach (var item in _rowsMap.ToArray())
                    if (item.Value > targetRowIndex)
                        _rowsMap[item.Key]--;

                for (int i = targetRowIndex; i < _rowsMap.Count - 1; i++)
                    _rowRecords[i] = _rowRecords[i + 1];

                for (int i = 0; i < _rowsMap.Count - 1; i++)
                    if (_rowRecords[i].TargetIndex > targetRowIndex)
                        _rowRecords[i].TargetIndex--;
            }

            FillRow(_rowsMap.Count - 1, default);

            _rowRecords[_rowsMap.Count - 1] = null;

            _rowsMap.Remove(row);
        }

        private void ExtendColumns()
        {
            var previousData = _data;
            var previousRecords = _columnRecords;

            if (_columnsCapasity == 0)
                _columnsCapasity = 1;
            else
                _columnsCapasity *= 2;

            _data = new TItem[_columnsCapasity, _rowsCapasity];

            for (int columnIndex = 0; columnIndex < _columnsMap.Count; columnIndex++)
                for (int rowIndex = 0; rowIndex < _rowsMap.Count; rowIndex++)
                    _data[columnIndex, rowIndex] = previousData[columnIndex, rowIndex];

            _columnRecords = new ColumnRecord[_columnsCapasity];

            for (int i = 0; i < previousRecords.Length; i++)
                _columnRecords[i] = previousRecords[i];
        }

        private void ExtendRows()
        {
            var previousData = _data;
            var previousRecords = _rowRecords;

            if (_rowsCapasity == 0)
                _rowsCapasity = 1;
            else
                _rowsCapasity *= 2;

            _data = new TItem[_columnsCapasity, _rowsCapasity];

            for (int columnIndex = 0; columnIndex < _columnsMap.Count; columnIndex++)
                for (int rowIndex = 0; rowIndex < _rowsMap.Count; rowIndex++)
                    _data[columnIndex, rowIndex] = previousData[columnIndex, rowIndex];

            _rowRecords = new RowRecord[_rowsCapasity];

            for (int i = 0; i < previousRecords.Length; i++)
                _rowRecords[i] = previousRecords[i];
        }
    }
}
