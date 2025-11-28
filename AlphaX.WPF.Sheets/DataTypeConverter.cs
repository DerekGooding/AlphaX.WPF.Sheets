namespace AlphaX.WPF.Sheets;

internal static class DataTypeConverter
{
    public static object? ConvertType(string value)
        => string.IsNullOrEmpty(value) ? null
            : int.TryParse(value, out var integer) ? integer
            : double.TryParse(value, out var doubleResult) ? doubleResult
            : decimal.TryParse(value, out var decimalResult) ? decimalResult
            : DateTime.TryParse(value, out var date) ? date : value;
}
