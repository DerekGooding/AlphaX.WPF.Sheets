using AlphaX.WPF.Sheets.Enums;
using System.Windows;

namespace AlphaXSpreadSamplesExplorer;

/// <summary>
/// Interaction logic for Testing.xaml
/// </summary>Row
public partial class Testing : UserControl
{
    public Testing()
    {
        InitializeComponent();
        spread.MouseDoubleClick += Spread_MouseDoubleClick;
        var worksheet = spread.SheetViews.ActiveSheetView.WorkSheet;
    }

    private void Spread_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        var hitTest = spread.HitTest(e.GetPosition(spread));
        if (hitTest != null && hitTest.Element == VisualElement.ColumnHeader)
        {
            spread.SheetViews.ActiveSheetView.AutoSizeColumn(hitTest.Column);
        }
    }

    private void Testing_Loaded(object sender, RoutedEventArgs e)
    {

    }
}
