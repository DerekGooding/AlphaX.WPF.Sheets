namespace AlphaXSpreadSamplesExplorer.Samples;

/// <summary>
/// Interaction logic for ScrollModes.xaml
/// </summary>
public partial class ScrollModes : UserControl
{
    public ScrollModes()
    {
        InitializeComponent();
        var worksheet = spread.SheetViews.ActiveSheetView.WorkSheet;
        for (var row = 0; row < 100; row++)
        {
            for (var col = 0; col < 10; col++)
            {
                worksheet.Cells[row, col].Value = $"abc{col}";
            }
        }
    }
}
