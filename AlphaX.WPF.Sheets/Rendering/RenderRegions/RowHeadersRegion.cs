using AlphaX.WPF.Sheets.Enums;
using AlphaX.WPF.Sheets.Extensions;
using AlphaX.WPF.Sheets.UI;

namespace AlphaX.WPF.Sheets.Rendering.RenderRegions;

internal class RowHeadersRegion : AlphaXSheetViewRegion
{
    private WorkSheet _workSheet;
    private ViewPort _viewPort;
    private readonly int _resizeDelta;

    public RowHeadersRegion() => _resizeDelta = 5;

    public override void AttachSheet(AlphaXSheetView sheetView)
    {
        base.AttachSheet(sheetView);
        _workSheet = sheetView.WorkSheet;
        _viewPort = sheetView.ViewPort.As<ViewPort>();
    }

    protected override Drawing GetDrawing() => SheetView.Spread.RenderEngine.RowHeadersRenderer.Drawing;

    protected override SpreadHitTestResult HitTestCore(AlphaXSheetView sheetView, Point hitPoint)
    {
        var hitTestInfo = new SpreadHitTestResult
        {
            Element = VisualElement.RowHeader,
            Sheet = sheetView,
            ActualHitTestPoint = hitPoint
        };
        var rows = _workSheet.Rows.As<Rows>();
        var columns = _workSheet.RowHeaders.Columns.As<Columns>();
        var viewRange = sheetView.ViewPort.ViewRange;

        var point = new Point(hitPoint.X + _viewPort.LeftColumnLocation,
            hitPoint.Y + _viewPort.TopRowLocation);

        double x = 0, y = 0;

        for (var row = viewRange.TopRow; row <= viewRange.BottomRow; row++)
        {
            var rowLocation = rows.GetLocation(row);
            double rowHeight = _workSheet.Rows.GetRowHeight(row);

            if (point.Y >= rowLocation && point.Y < rowLocation + rowHeight)
            {
                if (point.Y > rowLocation + rowHeight - _resizeDelta)
                {
                    hitTestInfo.Element = VisualElement.RowHeaderResizeBar;
                }

                hitTestInfo.Row = row;
                y = rowLocation;
                break;
            }
        }

        for (var col = viewRange.LeftColumn; col <= viewRange.RightColumn; col++)
        {
            var colLocation = columns.GetLocation(col);
            var sheetColumn = columns.GetItem(col, false);
            double columnWidth = sheetColumn == null ? _workSheet.DefaultColumnWidth : sheetColumn.Width;

            if (point.X >= colLocation && point.X < colLocation + columnWidth)
            {
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
