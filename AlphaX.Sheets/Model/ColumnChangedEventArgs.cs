using AlphaX.Sheets.Abstractions;

namespace AlphaX.Sheets.Model;

public class ColumnChangedEventArgs : EventArgsBase
{
    /// <summary>
    /// Index of the first column.
    /// </summary>
    public int Index { get; internal set; }
    /// <summary>
    /// Afftected column count.
    /// </summary>
    public int Count { get; internal set; }
}
