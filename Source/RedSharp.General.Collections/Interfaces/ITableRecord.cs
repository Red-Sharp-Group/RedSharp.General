using System.Collections.Generic;

namespace RedSharp.General.Collections.Interfaces
{
    public interface ITableRecord<TName, TKey, TItem> : IReadOnlyList<TItem>
    {
        TItem this[TKey key] { get; set; }

        new TItem this[int index] { get; set; }


        TName Name { get; }

        int TargetIndex { get; }


        bool Contains(TKey key);

        bool TryGetValue(TKey key, out TItem value);


        int IndexOf(TKey key);
    }
}
