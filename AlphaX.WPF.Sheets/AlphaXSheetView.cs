using AlphaX.WPF.Sheets.Enums;
using AlphaX.WPF.Sheets.Extensions;
using AlphaX.WPF.Sheets.Rendering.Renderers;
using AlphaX.WPF.Sheets.Styling;
using AlphaX.WPF.Sheets.UI;
using AlphaX.WPF.Sheets.UndoRedo.Actions;
using System.Text;

namespace AlphaX.WPF.Sheets;

internal class AlphaXSheetView : IAlphaXSheetView
{
    private readonly ViewPort _viewPort;

    #region Properties
    public GridLineVisibility GridLineVisibility { get; set; }
    public HeadersVisibility HeadersVisibility
    {
        get;
        set
        {
            field = value;
            SetHeadersVisibility();
        }
    }
    public IViewPort ViewPort => _viewPort;
    public Point ScrollPosition { get; private set; }
    public SelectionMode SelectionMode { get; set; }
    public MouseWheelScrollDirection MouseWheelScrollDirection { get; set; }
    public AlphaXSpread Spread { get; }
    public int ActiveRow { get; internal set; }
    public int ActiveColumn { get; internal set; }
    public CellRange Selection { get; }
    public WorkSheet WorkSheet { get; }
    #endregion

    public AlphaXSheetView(AlphaXSpread spread, WorkSheet worksheet)
    {
        Spread = spread;
        WorkSheet = worksheet;
        GridLineVisibility = GridLineVisibility.Both;
        SelectionMode = SelectionMode.CellRange;
        MouseWheelScrollDirection = MouseWheelScrollDirection.Vertical;
        ScrollPosition = new Point(0, 0);
        _viewPort = new ViewPort(this);
        HeadersVisibility = HeadersVisibility.Both;
        Selection = new CellRange(0, 0);
    }

    #region Public
    public void CopyToClipboard() => CopyToClipboard(Selection);

    public void PasteFromClipboard()
    {
        var dataObject = Clipboard.GetDataObject();

        if (dataObject == null)
            return;

        if (dataObject.GetDataPresent("InternalDataObject"))
        {
            var data = (object[,])dataObject.GetData("InternalDataObject");

            if (data == null)
                return;

            Spread.WorkBook.UpdateProvider.SuspendUpdates = true;

            var pasteAction = new ClipboardPasteAction() { SheetView = this };
            pasteAction.OldState.Value = WorkSheet.WorkBook.DataProvider.GetRangeValue(WorkSheet.Name, ActiveRow, ActiveColumn, data.GetLength(0), data.GetLength(1));
            pasteAction.OldState.Row = ActiveRow;
            pasteAction.OldState.Column = ActiveColumn;
            pasteAction.OldState.Selection = Selection.Clone();

            for (var row = 0; row < data.GetLength(0); row++)
            {
                for (var column = 0; column < data.GetLength(1); column++)
                {
                    var value = data[row, column];
                    WorkSheet.Cells[ActiveRow + row, ActiveColumn + column].Value = value;
                }
            }

            Spread.SelectionManager.SelectRange(ActiveRow, ActiveColumn, data.GetLength(0), data.GetLength(1));

            pasteAction.NewState.Value = data;
            pasteAction.NewState.Row = ActiveRow;
            pasteAction.NewState.Column = ActiveColumn;
            pasteAction.NewState.Selection = Selection.Clone();

            Spread.UndoRedoManager.AddAction(pasteAction);
            Spread.WorkBook.UpdateProvider.SuspendUpdates = false;
        }
    }

    public void CopyToClipboard(CellRange range)
    {
        var stringBuilder = new StringBuilder();

        var data = WorkSheet.WorkBook.DataProvider.GetRangeValue(WorkSheet.Name, range.TopRow, range.LeftColumn, range.RowCount, range.ColumnCount);

        for (var row = 0; row < data.GetLength(0); row++)
        {
            for (var column = 0; column < data.GetLength(1); column++)
            {
                stringBuilder.Append(data[row, column]);
                if (column < range.ColumnCount - 1)
                    stringBuilder.Append(SheetUtils.Tab);
            }

            if (row < range.RowCount - 1)
                stringBuilder.Append(SheetUtils.NextLine);
        }

        var dataObject = new DataObject();
        dataObject.SetData(DataFormats.Text, stringBuilder.ToString());
        dataObject.SetData("InternalDataObject", data);
        Clipboard.SetDataObject(dataObject);
    }

    public void Invalidate(bool rowHeaders = true, bool columnHeaders = true, bool cells = true, bool topLeft = true)
    {
        var pane = Spread.SheetViewPane;

        pane.Draw(cells, rowHeaders, columnHeaders, cells, cells, topLeft);

        if (cells)
        {
            var interactionLayer = pane.CellsRegion.GetInteractionLayer();
            interactionLayer?.InvalidateVisual();
        }

        if (rowHeaders)
        {
            var interactionLayer = pane.RowHeadersRegion.GetInteractionLayer();
            interactionLayer?.InvalidateVisual();
        }

        if (columnHeaders)
        {
            var interactionLayer = pane.ColumnHeadersRegion.GetInteractionLayer();
            interactionLayer?.InvalidateVisual();
        }

        if (topLeft)
            pane.TopLeftRegion.InvalidateVisual();
    }

    public void InvalidateCellRange(CellRange range)
    {
        range = _viewPort.ShrinkRangeToViewPort(range);
        if (range.IsValid)
            InvalidateCellRange(range.TopRow, range.LeftColumn, range.BottomRow, range.RightColumn);
    }

    public void InvalidateCellRange(int topRow, int leftCol, int bottomRow, int rightCol)
    {
        var pane = Spread.SheetViewPane;
        pane.DrawRange(topRow, leftCol, bottomRow, rightCol);
    }

    public void ScrollToHorizontalOffset(double offset)
    {
        var delta = offset - ScrollPosition.X;
        ScrollPosition = new Point(offset, ScrollPosition.Y);
        _viewPort.CalculateLeftColumn(delta);
        _viewPort.CalculateVisibleRange();
        Invalidate(false, true, true);
    }

    public void ScrollToVerticalOffset(double offset)
    {
        var delta = offset - ScrollPosition.Y;
        ScrollPosition = new Point(ScrollPosition.X, offset);
        _viewPort.CalculateTopRow(delta);
        _viewPort.CalculateVisibleRange();
        Invalidate(true, false, true);
    }
    #endregion

    #region Private
    private void SetHeadersVisibility() => Spread.SheetViewPane.UpdateHeadersSize();
    #endregion

    #region Internal
    internal double GetRowHeaderWidth() => HeadersVisibility is HeadersVisibility.Row or HeadersVisibility.Both ? WorkSheet.RowHeaders.Width : 0;

    internal double GetColumnHeaderHeight() => HeadersVisibility is HeadersVisibility.Column or HeadersVisibility.Both
            ? WorkSheet.ColumnHeaders.Height
            : 0;

    #endregion

    public override string ToString() => WorkSheet.Name;

    public void AutoSizeColumn(int column)
    {
        var sheetColumn = WorkSheet.Columns.GetItem(column, false);
        var width = 0;
        var cellValues = WorkSheet.Cells.GetCellValues(column);

        foreach (var cellValue in cellValues)
        {
            if (cellValue.Value != null)
            {
                var style = WorkSheet.WorkBook.PickStyle(WorkSheet.Cells.GetCell(cellValue.Key, column, false), sheetColumn, WorkSheet.Rows.GetItem(cellValue.Key, false));
                style ??= WorkSheet.WorkBook.GetNamedStyle(StyleKeys.DefaultRowHeaderStyleKey).As<Style>();
                var textWidth = TextRenderingExtensions.ComputeTextWidth(cellValue.Value.ToString(), style.FontSize, style.As<Style>().GlyphTypeface);
                width = Math.Max(width, textWidth + 11);
            }
        }

        if (width != WorkSheet.Columns.GetColumnWidth(column))
        {
            WorkSheet.Columns[column].Width = width;
        }

        _viewPort.CalculateVisibleRange();
        Spread.SheetTabControl.UpdateScrollbars();
        Invalidate();
    }
}
