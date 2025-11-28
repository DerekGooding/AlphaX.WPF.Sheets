using AlphaX.Sheets.Abstractions;

namespace AlphaX.Sheets.Model;

public class CellChangedEventArgs : EventArgsBase
{
    public int Row { get; internal set; }
    public int Column { get; internal set; }
}
