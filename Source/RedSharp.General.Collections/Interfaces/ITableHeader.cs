using System.Collections.Generic;

namespace RedSharp.General.Collections.Interfaces
{
    public interface ITableHeader<TName, TKey, TItem> : IReadOnlyList<ITableRecord<TName, TKey, TItem>>
    {
        ITableRecord<TName, TKey, TItem> this[TName name] { get; }


        void Add(TName name);

        void Insert(int index, TName name);


        bool Contains(TName name);

        bool TryGetRecord(TName name, out ITableRecord<TName, TKey, TItem> record);


        int IndexOf(TName name);


        bool Remove(TName name); 

        void RemoveAt(int index);

        void Clear();
    }
}
