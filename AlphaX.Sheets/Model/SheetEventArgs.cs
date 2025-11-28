namespace AlphaX.Sheets.Model;

public class SheetEventArgs(WorkSheet workSheet) : EventArgs
{
    public WorkSheet WorkSheet { get; } = workSheet;
}
