using AlphaX.Sheets.Abstractions;

namespace AlphaX.Sheets.Model;

public class RowChangedEventArgs : EventArgsBase
{
    /// <summary>
    /// Index of the first row.
    /// </summary>
    public int Index { get; internal set; }
    /// <summary>
    /// Afftected row count.
    /// </summary>
    public int Count { get; internal set; }
}
