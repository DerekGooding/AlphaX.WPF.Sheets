namespace AlphaX.WPF.Sheets.Data;

public class PropertyDataMap(string propertyName) : DataMap
{
    public string PropertyName { get; } = propertyName;
}

public class DataColumnDataMap(string columnName) : DataMap
{
    public string ColumnName { get; } = columnName;
}

public abstract class DataMap : IDataMap
{
}
