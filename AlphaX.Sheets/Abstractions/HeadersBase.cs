namespace AlphaX.Sheets.Abstractions;

public abstract class HeadersBase : IHeaders
{
    public Rows Rows { get; }
    public Columns Columns { get; }
    public Cells Cells { get; }
    public WorkSheet WorkSheet { get; }

    internal HeadersBase(WorkSheet workSheet)
    {
        WorkSheet = workSheet;
        Rows = new Rows(this);
        Columns = new Columns(this);
        Cells = new Cells(this);
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
        Cells.Dispose();
        Columns.Dispose();
        Rows.Dispose();
    }
}
