using AlphaX.Sheets.Abstractions;
using AlphaX.Sheets.Enums;

namespace AlphaX.Sheets.Model;

public class Rows : CollectionBase<Row>, IRows
{
    private Dictionary<int, double> _locationMap;

    protected override int Count => GetCount();

    internal Rows(object parent) : base(parent) => _locationMap = [];

    /// <summary>
    /// Gets the location of the row
    /// </summary>
    /// <param name="row">
    /// Row index.
    /// </param>
    /// <returns></returns>
    internal override double GetLocation(int row, bool recalculate = false)
    {
        if (_locationMap.TryGetValue(row, out var value) && !recalculate)
            return value;

        double yLocation = 0;
        double deltaHeight = 0;
        var count = 0;

        for (var index = row - 1; index >= 0; index--)
        {
            InternalCollection.TryGetValue(index, out var sheetRow);

            if (sheetRow != null)
                deltaHeight += sheetRow.Height;
            else
                count++;

            if (_locationMap.TryGetValue(index, out var value2))
            {
                yLocation = value2;
                break;
            }
        }

        var defaultRowHeight = 0;
        var sum = 0;

        if (Parent is IWorkSheet workSheet)
        {
            defaultRowHeight = workSheet.DefaultRowHeight;
            sum = workSheet.FilterProvider.FilteredRows
                  .Where(x => x.Key < row).Sum(x => GetRowHeight(x.Key));
        }
        else if (Parent is IRowHeaders rowHeaders)
        {
            defaultRowHeight = rowHeaders.WorkSheet.DefaultRowHeight;
        }
        else if (Parent is IColumnHeaders columnHeaders)
        {
            defaultRowHeight = columnHeaders.DefaultRowHeight;
        }

        var location = yLocation + (count * defaultRowHeight) + deltaHeight - sum;

        if (!_locationMap.TryAdd(row, location))
            _locationMap[row] = location;

        return location;
    }

    internal void UpdateRowsLocation(int fromRow, double offset)
    {
        for (var index = fromRow; index < Count; index++)
        {
            if (_locationMap.ContainsKey(index))
                _locationMap[index] += offset;
        }
    }

    protected override Row CreateItem(int index)
    {
        var row = new Row(this)
        {
            Index = index
        };
        return row;
    }

    public int GetRowHeight(int row)
    {
        var sheetRow = GetItem(row, false);

        return sheetRow == null ? GetDefaultRowHeight() : sheetRow.Height;
    }

    private int GetDefaultRowHeight()
    {
        if (Parent is IWorkSheet workSheet)
        {
            return workSheet.DefaultRowHeight;
        }
        else if (Parent is IRowHeaders rowHeaders)
        {
            return rowHeaders.WorkSheet.DefaultRowHeight;
        }
        else if (Parent is IColumnHeaders columnHeaders)
        {
            return columnHeaders.DefaultRowHeight;
        }

        return 0;
    }

    private int GetCount()
    {
        if (Parent is IWorkSheet workSheet)
        {
            return workSheet.RowCount;
        }
        else if (Parent is IRowHeaders rowHeaders)
        {
            return rowHeaders.WorkSheet.RowCount;
        }
        else if (Parent is IColumnHeaders columnHeaders)
        {
            return columnHeaders.RowCount;
        }

        return 0;
    }

    public void Dispose()
    {
        _locationMap.Clear();
        InternalCollection.Clear();
        InternalCollection = null;
        _locationMap = null;
    }

    public override void InsertBelow(int index) => InsertBelow(index, 1);

    public override void Add() => Add(1);

    public override void Remove(int index) => Remove(index, 1);

    public override void InsertBelow(int index, int count)
    {
        if (Parent is WorkSheet workSheet)
        {
            var items = InternalCollection.ToList();

            for (var itemIndex = items.Count - 1; itemIndex >= 0; itemIndex--)
            {
                var item = items[itemIndex];

                if (item.Key >= index)
                {
                    InternalCollection.Remove(item.Key);
                    InternalCollection.Add(item.Key + count, item.Value);
                }
            }

            workSheet.Cells.InsertRows(index, count);
            workSheet.RowCount += count;
            workSheet.OnRowsChanged(new RowChangedEventArgs()
            {
                Index = index,
                Count = count,
                Action = SheetAction.Added,
                ChangeType = ChangeType.Count
            });
        }
    }

    public override void Add(int count)
    {
        if (Parent is WorkSheet workSheet)
        {
            workSheet.RowCount += count;
        }
    }

    public override void Remove(int index, int count)
    {
        if (Parent is WorkSheet workSheet)
        {
            var items = InternalCollection.ToList();

            for (var itemIndex = 0; itemIndex < items.Count - 1; itemIndex++)
            {
                var item = items[itemIndex];

                if (item.Key >= index && item.Key < index + count)
                {
                    InternalCollection.Remove(item.Key);
                }
                else if (item.Key >= index + count)
                {
                    InternalCollection.Remove(item.Key);
                    InternalCollection.Add(item.Key - count, item.Value);
                }
            }

            workSheet.Cells.RemoveRows(index, count);
            workSheet.RowCount -= count;
            workSheet.OnRowsChanged(new RowChangedEventArgs()
            {
                Index = index,
                Count = count,
                Action = SheetAction.Removed,
                ChangeType = ChangeType.Count
            });
        }
    }
}
