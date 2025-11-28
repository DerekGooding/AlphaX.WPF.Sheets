namespace AlphaX.Sheets.Model;

public class SheetEventArgs : EventArgs
{
    public WorkSheet WorkSheet { get; }

    public SheetEventArgs(WorkSheet workSheet)
    {
        WorkSheet = workSheet;
    }
}
