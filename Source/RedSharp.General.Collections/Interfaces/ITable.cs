using System.Collections.Generic;

namespace RedSharp.General.Collections.Interfaces
{
    public interface ITable<TColumn, TRow, TItem> : IReadOnlyCollection<TItem>
    {
        TItem this[int column, int row] { get; set; }


        TItem this[TColumn column, TRow row] { get; set; }


        ITableHeader<TColumn, TRow, TItem> Columns { get; }

        ITableHeader<TRow, TColumn, TItem> Rows { get; }
    }
}
