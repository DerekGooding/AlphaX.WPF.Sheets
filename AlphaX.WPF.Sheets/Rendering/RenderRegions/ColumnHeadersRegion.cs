using AlphaX.WPF.Sheets.Enums;
using AlphaX.WPF.Sheets.Extensions;
using AlphaX.WPF.Sheets.UI;

namespace AlphaX.WPF.Sheets.Rendering.RenderRegions;

internal class ColumnHeadersRegion : AlphaXSheetViewRegion
{
    private WorkSheet _workSheet;
    private ViewPort _viewPort;
    private readonly int _resizeDelta;

    public ColumnHeadersRegion() => _resizeDelta = 5;

    public override void AttachSheet(AlphaXSheetView sheetView)
    {
        base.AttachSheet(sheetView);
        _workSheet = sheetView.WorkSheet;
        _viewPort = sheetView.ViewPort.As<ViewPort>();
    }

    protected override Drawing GetDrawing() => SheetView.Spread.RenderEngine.ColumnHeadersRenderer.Drawing;

    protected override SpreadHitTestResult HitTestCore(AlphaXSheetView sheetView, Point hitPoint)
    {
        var hitTestInfo = new SpreadHitTestResult
        {
            Element = VisualElement.ColumnHeader,
            Sheet = sheetView,
            ActualHitTestPoint = hitPoint
        };
        var rows = _workSheet.ColumnHeaders.Rows.As<Rows>();
        var columns = _workSheet.Columns.As<Columns>();
        var viewRange = sheetView.ViewPort.ViewRange;

        var point = new Point(hitPoint.X + _viewPort.LeftColumnLocation,
            hitPoint.Y + _viewPort.TopRowLocation);

        double x = 0, y = 0;

        for (var row = viewRange.TopRow; row <= viewRange.BottomRow; row++)
        {
            var rowLocation = rows.GetLocation(row);
            var sheetRow = rows.GetItem(row, false);
            double rowHeight = sheetRow == null ? _workSheet.DefaultRowHeight : sheetRow.Height;

            if (point.Y >= rowLocation && point.Y < rowLocation + rowHeight)
            {
                hitTestInfo.Row = row;
                y = rowLocation;
                break;
            }
        }

        for (var col = viewRange.LeftColumn; col <= viewRange.RightColumn; col++)
        {
            var colLocation = columns.GetLocation(col);
            double columnWidth = _workSheet.Columns.GetColumnWidth(col);

            if (point.X >= colLocation && point.X < colLocation + columnWidth)
            {
                if (point.X > colLocation + columnWidth - _resizeDelta)
                {
                    hitTestInfo.Element = VisualElement.ColumnHeaderResizeBar;
                }

                hitTestInfo.Column = col;
                x = colLocation;
                break;
            }
        }

        hitTestInfo.Position = new Point(x - _viewPort.LeftColumnLocation,
            y - _viewPort.TopRowLocation);
        return hitTestInfo;
    }
}
