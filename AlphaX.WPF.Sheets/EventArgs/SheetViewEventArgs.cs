namespace AlphaX.WPF.Sheets
{
    public class SheetViewEventArgs
    {
        public IAlphaXSheetView OldSheetView { get; internal set; }
        public IAlphaXSheetView NewSheetView { get; internal set; }
    }
}
