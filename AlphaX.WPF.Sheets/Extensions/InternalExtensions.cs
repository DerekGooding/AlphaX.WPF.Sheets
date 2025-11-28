using AlphaX.WPF.Sheets.UI.Interaction;

namespace AlphaX.WPF.Sheets.Extensions;

internal static class InternalExtensions
{
    public static IFormatter DefaultFormatter { get; }

    static InternalExtensions() => DefaultFormatter = new GeneralFormatter();

    public static T As<T>(this object obj) => (T)obj;

    internal static bool ContainsOrIntersectsWith(this Rect source, Rect rect) => source.Contains(rect) || source.IntersectsWith(rect);

    /// <summary>
    /// Gets the style according to the priority.
    /// </summary>
    internal static IStyle PickStyle(this WorkBook workBook, ICell cell, IColumn column, IRow row)
        => cell != null && !string.IsNullOrEmpty(cell.StyleName)
            ? workBook.GetNamedStyle(cell.StyleName)
            : column != null && !string.IsNullOrEmpty(column.StyleName)
            ? workBook.GetNamedStyle(column.StyleName)
            : row != null && !string.IsNullOrEmpty(row.StyleName) ? workBook.GetNamedStyle(row.StyleName) : (IStyle?)null;

    /// <summary>
    /// Gets the formatter according to the priority.
    /// </summary>
    internal static IFormatter PickFormatter(this IWorkSheet sheet, ICell cell, IColumn column, IRow row)
        => cell?.Formatter != null
            ? cell.Formatter : column?.Formatter != null
            ? column.Formatter : row?.Formatter != null ? row.Formatter : DefaultFormatter;

    internal static void EnsureFree(this InteractionLayer layer)
    {
        if (!layer.IsAttached)
            return;

        layer.DetachFromRegion();
        layer.ReleaseMouseCapture();
        layer.InvalidateVisual();
    }
}
