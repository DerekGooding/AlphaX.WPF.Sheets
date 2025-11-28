using AlphaX.Sheets.Enums;

namespace AlphaX.Sheets.Abstractions;

public abstract class EventArgsBase : EventArgs
{
    /// <summary>
    /// Performed action.
    /// </summary>
    public SheetAction Action { get; internal set; }
    /// <summary>
    /// Change type
    /// </summary>
    public ChangeType ChangeType { get; internal set; }
    public object OldValue { get; internal set; }
    public object NewValue { get; internal set; }
}
