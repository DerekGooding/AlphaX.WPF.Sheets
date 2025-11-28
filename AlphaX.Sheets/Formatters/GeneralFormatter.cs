using System.Globalization;

namespace AlphaX.Sheets.Formatters;

public class GeneralFormatter : IFormatter
{
    private readonly CultureInfo _culture;

    public GeneralFormatter() => _culture = CultureInfo.CurrentCulture;

    public string? Format(object? value)
        => value == null ? null
        : value is string v ? v
        : value.IsNumber() ? double.Parse(value.ToString(), _culture).ToString()
        : value is DateTime ? DateTime.Parse(value.ToString(), _culture).ToShortDateString() : value.ToString();
}
