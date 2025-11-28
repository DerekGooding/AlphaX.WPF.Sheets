using AlphaX.WPF.Sheets.Enums;
using AlphaX.WPF.Sheets.Extensions;
using AlphaX.WPF.Sheets.UI;

namespace AlphaX.WPF.Sheets.Rendering.RenderRegions;

internal class CellsRegion : AlphaXSheetViewRegion
{
    private WorkSheet _workSheet;
    private readonly DrawingGroup _drawing;
    private ViewPort _viewPort;
    private readonly double _dragFillOffset;

    public CellsRegion()
    {
        _drawing = new DrawingGroup();
        _dragFillOffset = 5;
    }

    public override void AttachSheet(AlphaXSheetView sheetView)
    {
        base.AttachSheet(sheetView);
        _workSheet = sheetView.WorkSheet;
        _drawing.Children.Clear();
        _drawing.Children.Add(sheetView.Spread.RenderEngine.GridLinesRenderer.Drawing);
        _drawing.Children.Add(sheetView.Spread.RenderEngine.CellsRenderer.Drawing);

        _viewPort = sheetView.ViewPort.As<ViewPort>();
    }

    protected override Drawing GetDrawing() => _drawing;

    protected override SpreadHitTestResult HitTestCore(AlphaXSheetView sheetView, Point hitPoint)
    {
        var hitTestInfo = new SpreadHitTestResult
        {
            Element = VisualElement.Cell,
            Row = -1,
            Column = -1,
            Sheet = sheetView,
            ActualHitTestPoint = hitPoint
        };
        var rows = _workSheet.Rows.As<Rows>();
        var columns = _workSheet.Columns.As<Columns>();
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
                hitTestInfo.Row = row;
                y = rowLocation;
                for (var col = viewRange.LeftColumn; col <= viewRange.RightColumn; col++)
                {
                    var colLocation = columns.GetLocation(col);
                    double columnWidth = _workSheet.Columns.GetColumnWidth(col);

                    if (point.X >= colLocation && point.X < colLocation + columnWidth)
                    {
                        hitTestInfo.Column = col;
                        x = colLocation;

                        if (sheetView.Selection.RightColumn == hitTestInfo.Column
                            && sheetView.Selection.BottomRow == hitTestInfo.Row
                            && point.X > x + columnWidth - _dragFillOffset
                            && point.Y > y + rowHeight - _dragFillOffset
                            && point.X <= x + columnWidth
                            && point.Y <= y + rowHeight)
                        {
                            hitTestInfo.Element = VisualElement.DragFill;
                        }

                        break;
                    }
                }
                break;
            }
        }

        hitTestInfo.Position = new Point(x - _viewPort.LeftColumnLocation,
            y - _viewPort.TopRowLocation);
        return hitTestInfo;
    }
}