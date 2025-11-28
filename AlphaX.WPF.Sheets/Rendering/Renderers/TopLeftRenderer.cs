using AlphaX.WPF.Sheets.Extensions;
using AlphaX.WPF.Sheets.Styling;

namespace AlphaX.WPF.Sheets.Rendering.Renderers;

internal class TopLeftRenderer : Renderer
{
    protected override void OnRender(DrawingContext context, int topRow, int leftColumn, int bottomRow, int rightColumn)
    {
        var workSheet = SheetView.WorkSheet;
        var width = workSheet.RowHeaders.Width;
        var height = workSheet.ColumnHeaders.Height;
        var topLeft = workSheet.TopLeft;
        var style = workSheet.WorkBook.GetNamedStyle(string.IsNullOrEmpty(topLeft.StyleName) ?
            StyleKeys.DefaultTopLeftStyleKey : topLeft.StyleName);
        var wpfStyle = style.As<Style>();
        if (Engine.RenderInfo.PartialRender)
            Engine.EnsureNewCacheDrawing(this, -1, -1);
        var drawingGroup = Engine.CreateDrawingObject(this, -1, -1);
        var halfPenWidth = (SheetView.Spread.GridLinePen.Thickness * SheetView.Spread.PixelPerDip) / 2;
        var rect = new Rect(-SheetView.Spread.GridLinePen.Thickness, -SheetView.Spread.GridLinePen.Thickness, width, height);

        var guidelines = new GuidelineSet();
        guidelines.GuidelinesX.Add(rect.Left + halfPenWidth);
        guidelines.GuidelinesX.Add(rect.Right + halfPenWidth);
        guidelines.GuidelinesY.Add(rect.Top + halfPenWidth);
        guidelines.GuidelinesY.Add(rect.Bottom - halfPenWidth);

        var ctx = drawingGroup.Open();
        ctx.PushGuidelineSet(guidelines);
        ctx.DrawRectangle(wpfStyle.Background, SheetView.Spread.GridLinePen, rect);

        var pathGeometry = new PathGeometry();
        pathGeometry.Figures.Add(new PathFigure(new Point(5, height - 5),
        [
            new LineSegment(new Point(width - 5, 5), false),
            new LineSegment(new Point(width - 5, height - 5), false),
            new LineSegment(new Point(5,height - 5), false)
        ], true));

        ctx.DrawGeometry(wpfStyle.Foreground, null, pathGeometry);
        ctx.Pop();
        ctx.Close();
        context.DrawDrawing(drawingGroup);
    }
}
