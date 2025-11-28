using System.Collections;

namespace AlphaX.Sheets.Model;

public class WorkSheets : IWorkSheets
{
    private Dictionary<string, WorkSheet> _sheets;
    private WorkSheet _activeSheet;

    public int Count => _sheets.Count;
    public WorkBook WorkBook { get; private set; }

    public WorkSheet this[string sheetName] => GetSheet(sheetName);

    public WorkSheet this[int index] => GetSheet(index);

    public WorkSheet ActiveSheet
    {
        get => _activeSheet; set => SetActiveSheet(value);
    }

    public int ActiveSheetIndex
    {
        get
        {
            var index = 0;
            foreach (var sheet in _sheets.Values)
            {
                if (sheet == _activeSheet)
                    return index;
                index++;
            }
            return -1;
        }
        set
        {
            var sheet = GetSheet(value);
            SetActiveSheet(sheet);
        }
    }

    public event EventHandler<SheetEventArgs> SheetAdded;
    public event EventHandler<SheetEventArgs> SheetRemoved;
    public event EventHandler<SheetEventArgs> ActiveSheetChanged;

    internal WorkSheets(WorkBook workBook)
    {
        WorkBook = workBook;
        _sheets = [];
    }

    public WorkSheet AddSheet(string name)
    {
        VerifySheetName(name);
        var workSheet = new WorkSheet(WorkBook, name);
        _sheets.Add(name.ToLowerInvariant(), workSheet);
        SheetAdded?.Invoke(this, new SheetEventArgs(workSheet));
        return workSheet;
    }

    /// <summary>
    /// Verifies if a sheet is already present with the same name.
    /// </summary>
    /// <param name="name"></param>
    /// <exception cref="ArgumentException"></exception>
    private void VerifySheetName(string name)
    {
        if (_sheets.ContainsKey(name.ToLowerInvariant()))
            throw new ArgumentException($"Sheet with name '{name}' already present.");
    }

    public WorkSheet GetSheet(string sheetName)
    {
        sheetName = sheetName.ToLowerInvariant();

        return !_sheets.TryGetValue(sheetName, out var sheet)
            ? throw new ArgumentException($"Sheet with name '{sheetName}' not present.")
            : sheet;
    }

    public WorkSheet GetSheet(int index)
    {
        if (_sheets.Count <= index)
            throw new IndexOutOfRangeException("Sheet index is out of range.");

        var sheet = _sheets.Values.ElementAt(index);
        return sheet;
    }

    private void SetActiveSheet(WorkSheet sheet)
    {
        _activeSheet = sheet;
        OnActiveSheetChanged(sheet);
    }

    public void RemoveSheet(string name)
    {
        var sheet = GetSheet(name);
        _sheets.Remove(sheet.Name.ToLowerInvariant());
        SheetRemoved?.Invoke(this, new SheetEventArgs(sheet));
    }

    public void RemoveSheet(int index)
    {
        var sheet = GetSheet(index);
        _sheets.Remove(sheet.Name);
        sheet.Dispose();
        SheetRemoved?.Invoke(this, new SheetEventArgs(sheet));
    }

    public void Clear()
    {
        foreach (var sheet in _sheets.ToList())
        {
            RemoveSheet(sheet.Key);
        }
        _activeSheet = null;
    }

    protected virtual void OnActiveSheetChanged(WorkSheet sheet) => ActiveSheetChanged?.Invoke(this, new SheetEventArgs(sheet));

    public void Dispose()
    {
        GC.SuppressFinalize(this);
        Clear();
        _sheets = null;
        WorkBook = null;
        _activeSheet = null;
    }

    public IEnumerator<WorkSheet> GetEnumerator() => _sheets.Values.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
