namespace AlphaX.WPF.Sheets.Model;

public class SheetViewEventArgs
{
    public IAlphaXSheetView OldSheetView { get; internal set; }
    public IAlphaXSheetView NewSheetView { get; internal set; }
}
