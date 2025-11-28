using AlphaX.CalcEngine.Parsers.Calc;
using AlphaX.Sheets.Data;
using AlphaX.Sheets.Enums;
using AlphaX.Sheets.Formatters;
using System.Diagnostics;

namespace AlphaX.Sheets.Model;

public class Cells : ICell
{
    static Cells() => _sortComparer = new NaturalSortComparer();

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private static readonly NaturalSortComparer _sortComparer;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private SortedDictionary<int, SortedDictionary<int, Cell>> _cellStore;

    public Cells this[string name]
    {
        get
        {
            if (name.Contains(':'))
            {
                var rangeRef = new CellRangeRef(name);
                return GetRange(rangeRef.TopRow, rangeRef.LeftColumn, rangeRef.RowCount, rangeRef.ColumnCount);
            }
            else
            {
                var cell = new CellRef(name);
                return GetRange(cell.Row, cell.Column, 1, 1);
            }
        }
    }

    public Cell this[int row, int column] => GetCell(row, column, true);

    public Cells this[int row, int column, int rowCount, int columnCount] => GetRange(row, column, rowCount, columnCount);

    public int Row { get; }

    public int Column { get; }

    public int RowCount
    {
        get
        {
            if (field >= 0)
                return field;

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
    }

    public int ColumnCount
    {
        get
        {
            if (field >= 0)
                return field;

            if (Parent is IWorkSheet workSheet)
            {
                return workSheet.ColumnCount;
            }
            else if (Parent is IRowHeaders rowHeaders)
            {
                return rowHeaders.ColumnCount;
            }
            else if (Parent is IColumnHeaders columnHeaders)
            {
                return columnHeaders.WorkSheet.ColumnCount;
            }

            return 0;
        }
    }

    public object Value
    {
        get => GetCell(Row, Column, true).Value; set => ApplyToRange(x => x.Value = value);
    }

    public string? Formula
    {
        get
        {
            var cell = GetCell(Row, Column, false);
            return cell?.Formula;
        }

        set => ApplyToRange(x => x.Formula = value);
    }

    public IFormatter? Formatter
    {
        get
        {
            var cell = GetCell(Row, Column, false);
            return cell?.Formatter;
        }

        set => ApplyToRange(x => x.Formatter = value);
    }

    public string StyleName
    {
        get => GetCell(Row, Column, true).StyleName; set => ApplyToRange(x => x.StyleName = value);
    }

    public object Parent { get; }

    public DataMap DataMap
    {
        get => GetCell(Row, Column, true).DataMap; set => ApplyToRange(x => x.DataMap = value);
    }

    public ICellType CellType
    {
        get => GetCell(Row, Column, true).CellType; set => ApplyToRange(x => x.CellType = value);
    }

    Cells ICell.Parent { get; }
    public bool HasFormula => false;

    public bool Locked
    {
        get => GetCell(Row, Column, true).Locked; set => ApplyToRange(x => x.Locked = value);
    }

    public bool IsVisible
    {
        get => GetCell(Row, Column, true).IsVisible; set => ApplyToRange(x => x.IsVisible = value);
    }

    public int RowSpan
    {
        get => GetCell(Row, Column, true).RowSpan; set => ApplyToRange(x => x.RowSpan = value);
    }

    public int ColumnSpan
    {
        get => GetCell(Row, Column, true).ColumnSpan; set => ApplyToRange(x => x.ColumnSpan = value);
    }

    internal Cells(object parent)
    {
        Parent = parent;
        Row = Column = 0;
        RowCount = ColumnCount = -1;
        _cellStore = [];
    }

    internal Cells(Cells parentRange, int row, int column, int rowCount, int columnCount)
    {
        Parent = parentRange.Parent;
        Row = row;
        Column = column;
        RowCount = rowCount;
        ColumnCount = columnCount;
        _cellStore = parentRange._cellStore;
    }

    /// <summary>
    /// Gets the cell present at row/column index.
    /// </summary>
    /// <param name="row">
    /// Row index.
    /// </param>
    /// <param name="column">
    /// Column index.
    /// </param>
    /// <param name="createIfNotExists">
    /// Whether to create the cell if not present in cell store.
    /// </param>
    /// <returns></returns>
    internal Cell GetCell(int row, int column, bool createIfNotExists)
    {
        ValidateIndexes(row, column, 1, 1);
        Cell cell = null;
        if (ContainsCell(row, column))
        {
            cell = _cellStore[row][column];
            cell.Row = row;
            cell.Column = column;
        }
        else if (createIfNotExists)
        {
            cell = CreateCell(row, column);
            cell.Row = row;
            cell.Column = column;
        }

        return cell;
    }

    /// <summary>
    /// Gets the column cells present in cell store.
    /// </summary>
    /// <param name="column"></param>
    internal IEnumerable<KeyValuePair<int, object>> GetCellValues(int column)
    {
        foreach (var rowCells in _cellStore)
        {
            if (rowCells.Value.TryGetValue(column, out var value) && value.Value != null)
            {
                yield return new KeyValuePair<int, object>(rowCells.Key, value.Value);
            }
        }
    }

    internal void ClearColumnCells(int column)
    {
        foreach (var rowCells in _cellStore)
        {
            if (rowCells.Value.ContainsKey(column))
            {
                rowCells.Value[column] = null;
                rowCells.Value.Remove(column);
            }
        }
    }

    public async void Sort(bool ascending)
    {
        await Task.Factory.StartNew(() =>
        {
            for (var col = Column; col < Column + ColumnCount; col++)
            {
                var cells = new Dictionary<int, Cell>();

                for (var row = Row; row < Row + RowCount; row++)
                {
                    cells.Add(row, GetCell(row, col, false));
                }

                var sortedCells = cells.ToList();
                sortedCells.Sort(_sortComparer);

                if (!ascending)
                    sortedCells.Reverse();

                var enumerator = cells.GetEnumerator();

                foreach (var cell in sortedCells)
                {
                    enumerator.MoveNext();
                    var current = enumerator.Current;
                    MoveCell(cell.Value, current.Key, col);
                }
            }
        });

        if (Parent is WorkSheet workSheet)
        {
            workSheet.OnRangeChanged(new RangeChangedEventArgs()
            {
                Action = SheetAction.Sort,
                ChangeType = ChangeType.None,
                SortState = ascending ? SortState.Ascending : SortState.Descending,
                CellRange = new CellRange(Row, Column, RowCount, ColumnCount)
            });
        }
    }

    public void Merge(string mergeStyle = null)
    {
        for (var col = Column; col < Column + ColumnCount; col++)
        {
            for (var row = Row + RowCount - 2; row >= Row; row--)
            {
                var cell = GetCell(row, col, true);
                var previousCell = GetCell(row + 1, col, true);

                if (cell.Value?.ToString() == previousCell.Value?.ToString())
                {
                    cell.RowSpan = previousCell.RowSpan < 2 ? 2 : previousCell.RowSpan + 1;
                    cell.StyleName = mergeStyle;
                    previousCell.IsVisible = false;
                }
            }
        }

        if (Parent is WorkSheet workSheet)
        {
            workSheet.OnRangeChanged(new RangeChangedEventArgs()
            {
                ChangeType = ChangeType.None,
                Action = SheetAction.Merge,
                CellRange = new CellRange(Row, Column, RowCount, ColumnCount)
            });
        }
    }

    internal void ClearCellStore()
    {
        foreach (var item in _cellStore)
        {
            foreach (var value in item.Value)
            {
                value.Value.Value = null;
            }
            item.Value.Clear();
        }

        _cellStore.Clear();
    }

    internal void InsertRows(int index, int count)
    {
        if (Parent is WorkSheet)
        {
            var items = _cellStore.ToList();

            for (var itemIndex = items.Count - 1; itemIndex >= 0; itemIndex--)
            {
                var item = items[itemIndex];

                if (item.Key >= index)
                {
                    _cellStore.Remove(item.Key);
                    _cellStore.Add(item.Key + count, item.Value);
                }
            }
        }
    }

    internal void RemoveRows(int index, int count)
    {
        if (Parent is WorkSheet workSheet)
        {
            var items = _cellStore.ToList();

            for (var itemIndex = 0; itemIndex < items.Count; itemIndex++)
            {
                var item = items[itemIndex];

                if (item.Key >= index && item.Key < index + count)
                {
                    var rowCells = _cellStore[item.Key];

                    foreach (var cell in rowCells)
                    {
                        workSheet.DataStore.SetValue(item.Key, cell.Key, null);
                    }

                    rowCells.Clear();
                    _cellStore.Remove(item.Key);
                }
                else if (item.Key >= index + count)
                {
                    _cellStore.Remove(item.Key);
                    _cellStore.Add(item.Key - count, item.Value);
                }
            }
        }
    }

    private void MoveCell(Cell cell, int toRow, int toColumn)
    {
        if (cell?.Row == toRow && cell?.Column == toColumn)
            return;

        if (_cellStore.ContainsKey(toRow))
        {
            if (cell == null)
            {
                _cellStore[toRow].Remove(toColumn);
            }
            else
            {
                _cellStore[toRow].Remove(toColumn);

                _cellStore[toRow].Add(toColumn, cell);
            }
            return;
        }

        if (cell != null)
        {
            _cellStore.Add(toRow, []);
            _cellStore[toRow].Add(toColumn, cell);
        }
    }

    /// <summary>
    /// Gets the cell range.
    /// </summary>
    /// <param name="row"></param>
    /// <param name="column"></param>
    /// <param name="rowCount"></param>
    /// <param name="columnCount"></param>
    /// <returns></returns>
    private Cells GetRange(int row, int column, int rowCount, int columnCount)
    {
        ValidateIndexes(row, column, rowCount, columnCount);
        return new Cells(this, row, column, rowCount, columnCount);
    }

    /// <summary>
    /// Creates a new cell.
    /// </summary>
    /// <param name="row"></param>
    /// <param name="column"></param>
    /// <returns></returns>
    private Cell CreateCell(int row, int column)
    {
        var cell = new Cell(this);

        if (!_cellStore.ContainsKey(row))
            _cellStore.Add(row, []);

        _cellStore[row].Add(column, cell);
        return cell;
    }

    /// <summary>
    /// Gets whether the cell is present in the cell store or not.
    /// </summary>
    /// <param name="row"></param>
    /// <param name="column"></param>
    /// <returns></returns>
    private bool ContainsCell(int row, int column) => _cellStore.ContainsKey(row) && _cellStore[row].ContainsKey(column);

    /// <summary>
    /// Validates whether the indexes are out of range or not.
    /// </summary>
    /// <param name="row"></param>
    /// <param name="column"></param>
    /// <param name="rowCount"></param>
    /// <param name="columnCount"></param>
    /// <exception cref="IndexOutOfRangeException"></exception>
    private void ValidateIndexes(int row, int column, int rowCount, int columnCount)
    {
        //if (row < Row || row >= Row + RowCount || column <  Column || column >= Column + ColumnCount
        //    || RowCount < rowCount || ColumnCount < columnCount)
        //    throw new IndexOutOfRangeException("Provided indexes doesn't belong to this cell range.");
    }

    /// <summary>
    /// Executes action for each cell present in range.
    /// </summary>
    /// <param name="action"></param>
    private void ApplyToRange(Action<ICell> action)
    {
        for (var row = Row; row < Row + RowCount; row++)
        {
            for (var column = Column; column < Column + ColumnCount; column++)
            {
                var cell = GetCell(row, column, true);
                action(cell);
            }
        }
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
        ClearCellStore();
        _cellStore = null;
    }
}
