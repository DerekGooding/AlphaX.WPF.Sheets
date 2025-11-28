using AlphaX.Sheets.Enums;
using AlphaX.WPF.Sheets.Extensions;

namespace AlphaX.WPF.Sheets.UI.Managers;

public class UIUpdateProvider(AlphaXSpread spread) : IUpdateProvider
{
    private readonly AlphaXSpread _spread = spread;
    private bool _suspendUpdates;

    bool IUpdateProvider.SuspendUpdates
    {
        get => _suspendUpdates;
        set
        {
            _suspendUpdates = value;

            if (!_suspendUpdates && _spread.IsLoaded)
            {
                _spread.SheetViews.ActiveSheetView.Invalidate();
            }
        }
    }

    void IUpdateProvider.CellChanged(WorkSheet worksheet, int row, int column, object oldValue, object newValue, AlphaX.Sheets.Enums.SheetAction action, ChangeType changeType)
    {
        if (!_spread.IsLoaded)
            return;

        _spread.Dispatcher.BeginInvoke(new Action(() =>
        {

            var sheetView = _spread.SheetViews.GetSheetView(worksheet);

            if (!sheetView.ViewPort.ViewRange.ContainsCell(row, column))
                return;

            if (sheetView.ViewPort.ViewRange.ContainsCell(row, column))
            {
                switch (changeType)
                {
                    case ChangeType.Value:
                        sheetView.Invalidate();
                        break;

                    case ChangeType.Formula:
                        sheetView.Invalidate();
                        break;

                    case ChangeType.Style:
                        sheetView.InvalidateCellRange(row, column, row, column);
                        break;
                }
            }
        }));
    }

    void IUpdateProvider.ColumnsChanged(WorkSheet worksheet, int index, int count, AlphaX.Sheets.Enums.SheetAction action, ChangeType changeType)
    {
        if (!_spread.IsLoaded)
            return;

        _spread.Dispatcher.BeginInvoke(new Action(() =>
        {
            var sheetView = _spread.SheetViews.GetSheetView(worksheet);
            sheetView.ViewPort.As<ViewPort>().CalculateVisibleRange();
            if (sheetView.ViewPort.ViewRange.ContainsColumn(index))
            {
                _spread.SheetTabControl.UpdateScrollbars();
                sheetView.Invalidate(false, true, true, false);
            }
        }));
    }

    void IUpdateProvider.RangeChanged(WorkSheet worksheet, CellRange range, AlphaX.Sheets.Enums.SheetAction action, ChangeType changeType)
    {
        if (!_spread.IsLoaded)
            return;

        _spread.Dispatcher.BeginInvoke(new Action(() =>
        {
            var sheetView = _spread.SheetViews.GetSheetView(worksheet);
            if (sheetView.ViewPort.ViewRange.Intersects(range))
            {
                sheetView.InvalidateCellRange(range);
            }
        }));
    }

    void IUpdateProvider.RowsChanged(WorkSheet worksheet, int index, int count, AlphaX.Sheets.Enums.SheetAction action, ChangeType changeType)
    {
        if (!_spread.IsLoaded)
            return;

        _spread.Dispatcher.BeginInvoke(new Action(() =>
        {
            var sheetView = _spread.SheetViews.GetSheetView(worksheet);
            sheetView.ViewPort.As<ViewPort>().CalculateVisibleRange();
            if (sheetView.ViewPort.ViewRange.ContainsRow(index))
            {
                _spread.SheetTabControl.UpdateScrollbars();
                sheetView.Invalidate(true, false, true, false);
            }
        }));
    }
}
