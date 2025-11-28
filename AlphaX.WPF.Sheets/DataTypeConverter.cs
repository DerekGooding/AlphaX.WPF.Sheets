namespace AlphaX.WPF.Sheets;

internal class DataTypeConverter
{
    public static object ConvertType(string value)
    {
        if (string.IsNullOrEmpty(value))
            return null;

        if (int.TryParse(value, out int integer))
            return integer;

        if (double.TryParse(value, out double doubleResult))
            return doubleResult;

        return decimal.TryParse(value, out decimal decimalResult)
            ? decimalResult
            : DateTime.TryParse(value, out DateTime date) ? date : value;
    }
}
