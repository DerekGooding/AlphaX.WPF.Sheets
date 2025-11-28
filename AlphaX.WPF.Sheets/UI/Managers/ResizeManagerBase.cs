using System.Windows.Shapes;

namespace AlphaX.WPF.Sheets.UI.Managers;

internal abstract class ResizeManagerBase : UIManager
{
    public Line ResizeLine { get; }

    protected ResizeManagerBase(AlphaXSpread spread) : base(spread) => ResizeLine = new Line
    {
        Stroke = Brushes.Black,
        StrokeThickness = 0.75,
        StrokeDashArray = new DoubleCollection([5, 2]),
        Visibility = Visibility.Collapsed
    };
}
