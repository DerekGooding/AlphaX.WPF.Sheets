namespace AlphaX.WPF.Sheets.Model;

public class CellsSelectionEventArgs : EventArgs
{
    public IAlphaXSheetView SheetView { get; internal set; }
    public CellRange Selection { get; internal set; }
}
