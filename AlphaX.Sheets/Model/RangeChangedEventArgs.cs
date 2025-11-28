using AlphaX.Sheets.Abstractions;
using AlphaX.Sheets.Enums;

namespace AlphaX.Sheets.Model;

public class RangeChangedEventArgs : EventArgsBase
{
    public CellRange CellRange { get; internal set; }
    public SortState SortState { get; internal set; }
}
