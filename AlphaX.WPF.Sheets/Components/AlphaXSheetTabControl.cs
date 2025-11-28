using AlphaX.WPF.Sheets.Enums;
using AlphaX.WPF.Sheets.Extensions;
using System.Windows.Controls;
using System.Windows.Threading;

namespace AlphaX.WPF.Sheets.Components;

public class AlphaXSheetTabControl : Control, IDisposable
{
    static AlphaXSheetTabControl() => DefaultStyleKeyProperty.OverrideMetadata(typeof(AlphaXSheetTabControl), new FrameworkPropertyMetadata(typeof(AlphaXSheetTabControl)));

    private ListBox _sheetsListBox;
    private RepeatButton _nextButton;
    private RepeatButton _previousButton;
    private Button _addButton;
    private Border _sheetViewPaneBorder;
    private Grid _root;
    private bool _eventsRegistered;

    internal ScrollBar HScrollBar { get; private set; }
    internal ScrollBar VScrollBar { get; private set; }
    internal AlphaXSpread Spread { get; private set; }

    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();
        Spread = TemplatedParent.As<AlphaXSpread>();
        _root = GetTemplateChild("_root").As<Grid>();
        _sheetViewPaneBorder = GetTemplateChild("_sheetViewPaneBorder").As<Border>();
        _sheetViewPaneBorder.Child = Spread.SheetViewPane;
        _sheetViewPaneBorder.BorderBrush = Spread.BorderBrush;
        _sheetViewPaneBorder.BorderThickness = new Thickness(0, 0, 0.75, 0.75);
        HScrollBar = GetTemplateChild("_hScrollBar").As<ScrollBar>();
        VScrollBar = GetTemplateChild("_vScrollBar").As<ScrollBar>();
        _sheetsListBox = GetTemplateChild("_sheetsListBox").As<ListBox>();
        _previousButton = GetTemplateChild("_btnPrevious").As<RepeatButton>();
        _nextButton = GetTemplateChild("_btnNext").As<RepeatButton>();
        _addButton = GetTemplateChild("_btnAddSheet").As<Button>();
        RegisterInternalEventHandlers();
        _sheetsListBox.ItemsSource = Spread.SheetViews;
        _sheetsListBox.SelectedIndex = 0;
    }

    public void DisplayActiveSheet()
    {
        var sheetView = Spread.SheetViews.ActiveSheetView.As<AlphaXSheetView>();
        Spread.SheetViewPane.AttachSheet(sheetView);
        Spread.RenderEngine.SetRenderSheet(sheetView);
        UpdateScrollbars();
        HScrollBar.Value = sheetView.ScrollPosition.X;
        VScrollBar.Value = sheetView.ScrollPosition.Y;
        sheetView.ScrollToHorizontalOffset(sheetView.ScrollPosition.X);
        sheetView.ScrollToVerticalOffset(sheetView.ScrollPosition.Y);
    }

    private void OnAddSheetClick(object sender, RoutedEventArgs e)
    {
        Spread.WorkBook.WorkSheets.AddSheet($"Sheet{Spread.WorkBook.WorkSheets.Count + 1}");
        _sheetsListBox.SelectedIndex = _sheetsListBox.Items.Count - 1;
    }

    private void OnSheetSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (Spread.EditingManager.IsEditing)
            Spread.EditingManager.EndEdit(true);

        var sheetView = _sheetsListBox.SelectedItem.As<AlphaXSheetView>();
        Spread.WorkBook.WorkSheets.ActiveSheet = sheetView.WorkSheet;
        DisplayActiveSheet();
    }

    private void OnNextSheetClick(object sender, RoutedEventArgs e)
    {
        if (_sheetsListBox.SelectedIndex <= _sheetsListBox.Items.Count - 1)
        {
            _sheetsListBox.SelectedIndex++;
        }
    }

    private void OnPreviousSheetClick(object sender, RoutedEventArgs e)
    {
        if (_sheetsListBox.SelectedIndex > 0)
        {
            _sheetsListBox.SelectedIndex--;
        }
    }

    /// <summary>
    /// Register internal event handlers
    /// </summary>
    private void RegisterInternalEventHandlers()
    {
        if (_eventsRegistered)
            return;

        WeakEventManager<ScrollBar, RoutedPropertyChangedEventArgs<double>>.AddHandler(HScrollBar, "ValueChanged", OnHorizontalScrollBarValueChanged);
        WeakEventManager<ScrollBar, RoutedPropertyChangedEventArgs<double>>.AddHandler(VScrollBar, "ValueChanged", OnVerticalScrollBarValueChanged);
        WeakEventManager<Button, RoutedEventArgs>.AddHandler(_addButton, "Click", OnAddSheetClick);
        WeakEventManager<RepeatButton, RoutedEventArgs>.AddHandler(_nextButton, "Click", OnNextSheetClick);
        WeakEventManager<RepeatButton, RoutedEventArgs>.AddHandler(_previousButton, "Click", OnPreviousSheetClick);
        WeakEventManager<ListBox, SelectionChangedEventArgs>.AddHandler(_sheetsListBox, "SelectionChanged", OnSheetSelectionChanged);

        Dispatcher.BeginInvoke(new Action(() =>
        {
            WeakEventManager<Thumb, DragCompletedEventArgs>.AddHandler(HScrollBar.Track.Thumb, "DragCompleted", OnHorizontalScrollDragCompleted);
            WeakEventManager<Thumb, DragCompletedEventArgs>.AddHandler(VScrollBar.Track.Thumb, "DragCompleted", OnVerticalScrollDragCompleted);
        }), DispatcherPriority.Loaded);

        _eventsRegistered = true;
    }

    /// <summary>
    /// Unregister internal event handlers.
    /// </summary>
    private void UnRegisterInternalEventHandlers()
    {
        if (!_eventsRegistered)
            return;

        WeakEventManager<ScrollBar, RoutedPropertyChangedEventArgs<double>>.RemoveHandler(HScrollBar, "ValueChanged", OnHorizontalScrollBarValueChanged);
        WeakEventManager<Thumb, DragCompletedEventArgs>.RemoveHandler(HScrollBar.Track.Thumb, "DragCompleted", OnHorizontalScrollDragCompleted);
        WeakEventManager<ScrollBar, RoutedPropertyChangedEventArgs<double>>.RemoveHandler(VScrollBar, "ValueChanged", OnVerticalScrollBarValueChanged);
        WeakEventManager<Thumb, DragCompletedEventArgs>.RemoveHandler(VScrollBar.Track.Thumb, "DragCompleted", OnVerticalScrollDragCompleted);
        WeakEventManager<Button, RoutedEventArgs>.RemoveHandler(_addButton, "Click", OnAddSheetClick);
        WeakEventManager<ListBox, SelectionChangedEventArgs>.RemoveHandler(_sheetsListBox, "SelectionChanged", OnSheetSelectionChanged);
        WeakEventManager<RepeatButton, RoutedEventArgs>.RemoveHandler(_nextButton, "Click", OnNextSheetClick);
        WeakEventManager<RepeatButton, RoutedEventArgs>.RemoveHandler(_previousButton, "Click", OnPreviousSheetClick);
        _eventsRegistered = false;
    }

    /// <summary>
    /// Updates the scrollbars according to the sheet size and viewport.
    /// </summary>
    internal void UpdateScrollbars()
    {
        var sheet = Spread.SheetViews.ActiveSheetView.WorkSheet;
        var columns = sheet.Columns;
        var rows = sheet.Rows;
        HScrollBar.ViewportSize = Spread.SheetViewPane.CellsRegion.ActualWidth;
        HScrollBar.Maximum = HScrollBar.Minimum = VScrollBar.Maximum = VScrollBar.Minimum = 0;
        var lastColumnLocation = columns.GetLocation(sheet.ColumnCount - 1);

        for (var column = sheet.ColumnCount - 1; column >= 0; column--)
        {
            var location = columns.GetLocation(column);

            if (HScrollBar.ViewportSize != 0 && lastColumnLocation - location >= HScrollBar.ViewportSize)
            {
                HScrollBar.Maximum = columns.GetLocation(column + 3);
                break;
            }
        }

        VScrollBar.ViewportSize = Spread.SheetViewPane.CellsRegion.ActualHeight;
        var lastRowLocation = rows.GetLocation(sheet.RowCount - 1);

        for (var row = sheet.RowCount - 1; row >= 0; row--)
        {
            var location = rows.GetLocation(row);

            if (VScrollBar.ViewportSize != 0 && lastRowLocation - location >= VScrollBar.ViewportSize)
            {
                VScrollBar.Maximum = rows.GetLocation(row + 3);
                break;
            }
        }

        VScrollBar.Visibility = VScrollBar.Maximum == VScrollBar.Minimum ? Visibility.Hidden : Visibility.Visible;

        HScrollBar.Visibility = HScrollBar.Maximum == HScrollBar.Minimum ? Visibility.Hidden : Visibility.Visible;
    }

    #region Scrolling
    private void OnVerticalScrollBarValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        if (VScrollBar.Track.Thumb.IsDragging && Spread.ScrollMode == SheetScrollMode.Deferred)
            return;

        Spread.SheetViews.ActiveSheetView.ScrollToVerticalOffset(e.NewValue);
    }

    private void OnHorizontalScrollBarValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        if (HScrollBar.Track.Thumb.IsDragging && Spread.ScrollMode == SheetScrollMode.Deferred)
            return;

        Spread.SheetViews.ActiveSheetView.ScrollToHorizontalOffset(e.NewValue);
    }

    private void OnHorizontalScrollDragCompleted(object sender, DragCompletedEventArgs e)
    {
        if (Spread.ScrollMode == SheetScrollMode.Deferred)
        {
            Spread.SheetViews.ActiveSheetView.ScrollToHorizontalOffset(HScrollBar.Value);
        }
    }

    private void OnVerticalScrollDragCompleted(object sender, DragCompletedEventArgs e)
    {
        if (Spread.ScrollMode == SheetScrollMode.Deferred)
        {
            Spread.SheetViews.ActiveSheetView.ScrollToVerticalOffset(VScrollBar.Value);
        }
    }
    #endregion

    public void Dispose()
    {
        GC.SuppressFinalize(this);
        UnRegisterInternalEventHandlers();
        _sheetsListBox.ItemsSource = null;
        _root.Children.Clear();
        HScrollBar = null;
        VScrollBar = null;
        _sheetsListBox = null;
        _nextButton = null;
        _previousButton = null;
        _addButton = null;
        _sheetViewPaneBorder = null;
        _root = null;
    }
}
