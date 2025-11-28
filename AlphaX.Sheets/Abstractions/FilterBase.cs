namespace AlphaX.Sheets.Abstractions;

public abstract class FilterBase
{
    internal bool PassesFilter(object value) => Filter(value);

    /// <summary>
    /// Evaluates whether the value passes the filter or not.
    /// </summary>
    /// <returns></returns>
    protected abstract bool Filter(object value);
}
