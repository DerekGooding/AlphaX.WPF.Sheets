using AlphaX.Sheets.Cells;
using AlphaX.Sheets.Enums;
using System;

namespace AlphaX.Sheets.EventArgs
{
    public class RangeChangedEventArgs : BaseEventArgs
    {
        public CellRange CellRange { get; internal set; }
        public SortState SortState { get; internal set; }
    }
}
