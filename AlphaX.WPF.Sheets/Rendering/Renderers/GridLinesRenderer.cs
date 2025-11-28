using AlphaX.WPF.Sheets.Enums;
using AlphaX.WPF.Sheets.Extensions;
using AlphaX.WPF.Sheets.UI;

namespace AlphaX.WPF.Sheets.Rendering.Renderers;

internal class GridLinesRenderer : Renderer
{
    /// <summary>
    /// Draws horizontal grid lines.
    /// </summary>
    /// <param name="context"></param>
    /// <param name="topRow"></param>
    /// <param name="bottomRow"></param>
    private void DrawHorizontalGridlines(DrawingContext context, int topRow, int leftColumn, int bottomRow, int rightColumn)
    {
        var workSheet = SheetView.WorkSheet;
        var rows = workSheet.Rows;
        var columns = workSheet.Columns;
        var viewport = SheetView.ViewPort.As<ViewPort>();

        for (var row = topRow; row <= bottomRow; row++)
        {
            if (Engine.RenderInfo.PartialRender)
                Engine.EnsureNewCacheDrawing(this, row, -1);
            var rowHeight = rows.GetRowHeight(row);
            if (rowHeight == 0)
                continue;

            var rowLocation = rows.GetLocation(row);
            var y = rowLocation - viewport.TopRowLocation + rowHeight;
            var halfPenWidth = (SheetView.Spread.GridLinePen.Thickness * SheetView.Spread.PixelPerDip) / 2;
            var drawing = Engine.CreateDrawingObject(this, row, -1);
            var guidelines = new GuidelineSet();
            guidelines.GuidelinesY.Add(y + halfPenWidth);
            drawing.GuidelineSet = guidelines;
            var ctx = drawing.Open();

            ctx.DrawLine(SheetView.Spread.GridLinePen, new Point(0, y),
                        new Point(columns.GetLocation(rightColumn) - viewport.LeftColumnLocation + columns.GetColumnWidth(rightColumn), y));
            ctx.Close();
            context.DrawDrawing(drawing);
        }
    }

    /// <summary>
    /// Draws vertical grid lines.
    /// </summary>
    /// <param name="context"></param>
    /// <param name="leftColumn"></param>
    /// <param name="rightColumn"></param>
    private void DrawVerticalGridlines(DrawingContext context, int topRow, int leftColumn, int bottomRow, int rightColumn)
    {
        var workSheet = SheetView.WorkSheet;
        var rows = workSheet.Rows;
        var columns = workSheet.Columns;
        var viewport = SheetView.ViewPort.As<ViewPort>();

        for (var col = leftColumn; col <= rightColumn; col++)
        {
            if (Engine.RenderInfo.PartialRender)
                Engine.EnsureNewCacheDrawing(this, -1, col);
            var columnWidth = columns.GetColumnWidth(col);
            if (columnWidth == 0)
                continue;

            var colLocation = columns.GetLocation(col);
            var x = colLocation - viewport.LeftColumnLocation + columnWidth;
            var halfPenWidth = (SheetView.Spread.GridLinePen.Thickness * SheetView.Spread.PixelPerDip) / 2;
            var drawing = Engine.CreateDrawingObject(this, -1, col);
            var guidelines = new GuidelineSet();
            guidelines.GuidelinesX.Add(x + halfPenWidth);
            drawing.GuidelineSet = guidelines;
            var ctx = drawing.Open();

            ctx.DrawLine(SheetView.Spread.GridLinePen, new Point(x, 0),
                new Point(x, rows.GetLocation(bottomRow) - viewport.TopRowLocation + rows.GetRowHeight(bottomRow)));
            ctx.Close();
            context.DrawDrawing(drawing);
        }
    }

    protected override void OnRender(DrawingContext context, int topRow, int leftColumn, int bottomRow, int rightColumn)
    {
        switch (SheetView.GridLineVisibility)
        {
            case GridLineVisibility.Vertical:
                DrawVerticalGridlines(context, topRow, leftColumn, bottomRow, rightColumn);
                break;

            case GridLineVisibility.Horizontal:
                DrawHorizontalGridlines(context, topRow, leftColumn, bottomRow, rightColumn);
                break;

            case GridLineVisibility.Both:
                DrawVerticalGridlines(context, topRow, leftColumn, bottomRow, rightColumn);
                DrawHorizontalGridlines(context, topRow, leftColumn, bottomRow, rightColumn);
                break;
        }
    }
}
