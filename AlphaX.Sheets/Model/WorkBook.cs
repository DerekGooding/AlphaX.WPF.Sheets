using AlphaX.CalcEngine;
using AlphaX.CalcEngine.Interfaces;

namespace AlphaX.Sheets.Model;

public class WorkBook : IWorkBook
{
    private Dictionary<string, NamedStyle> _namedStyles;

    public string Name { get; set; }
    public WorkSheets WorkSheets { get; private set; }
    public ICalcEngine CalcEngine { get; private set; }
    public IUpdateProvider UpdateProvider { get; }
    internal WorkBookDataProvider DataProvider { get; private set; }

    public WorkBook(string name)
    {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentNullException(nameof(name));

        Name = name;
        WorkSheets = new WorkSheets(this);
        _namedStyles = [];
        DataProvider = new WorkBookDataProvider(this);
        CalcEngine = new AlphaXCalcEngine(DataProvider);
    }

    public WorkBook(string name, IUpdateProvider updateProvider) : this(name)
    {
        ArgumentNullException.ThrowIfNull(updateProvider);

        UpdateProvider = updateProvider;
    }

    public void AddNamedStyle(string styleName, NamedStyle style)
    {
        if (_namedStyles.ContainsKey(styleName))
            throw new ArgumentException($"A style is already registered with the name '{styleName}'");

        _namedStyles.Add(styleName, style);
    }

    public NamedStyle GetNamedStyle(string styleName)
        => !_namedStyles.TryGetValue(styleName, out var value)
            ? throw new ArgumentException($"Style with name '{styleName}' not found.")
            : value;

    public void Dispose()
    {
        GC.SuppressFinalize(this);
        WorkSheets.Dispose();
        _namedStyles.Clear();
        WorkSheets = null;
        CalcEngine = null;
        _namedStyles = null;
        DataProvider = null;
    }
}
