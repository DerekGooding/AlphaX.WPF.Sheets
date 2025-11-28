namespace AlphaX.Sheets.Model;

public class TopLeft : ITopLeft
{
    public IWorkSheet WorkSheet { get; }
    public string StyleName { get; set; }

    internal TopLeft(IWorkSheet workSheet)
    {
        WorkSheet = workSheet;
    }
}
